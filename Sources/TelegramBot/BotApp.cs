﻿using System;
using Telegram.Bot;
using System.Threading;
using System.Reflection;
using Telegram.Bot.Types;
using TelegramBot.Handlers;
using System.Threading.Tasks;
using TelegramBot.Controllers;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace TelegramBot
{
    /// <summary>
    /// Telegram bot application.
    /// </summary>
    public class BotApp : IBot
    {
        private readonly ILogger<BotApp> _logger;
        private readonly TelegramBotClient _client;
        private readonly ServiceProvider _serviceProvider;
        private IReadOnlyCollection<MethodInfo> _controllerMethods;

        /// <summary>
        /// Creates a new instance of <see cref="BotApp"/>.
        /// </summary>
        /// <param name="client">Telegram bot client.</param>
        /// <param name="serviceProvider">Service provider.</param>
        public BotApp(TelegramBotClient client, ServiceProvider serviceProvider)
        {
            _client = client;
            _serviceProvider = serviceProvider;
            _controllerMethods = new List<MethodInfo>();
            _logger = serviceProvider.GetRequiredService<ILogger<BotApp>>();
        }

        /// <summary>
        /// Maps controllers inherited from <see cref="BotControllerBase"/>.
        /// </summary>
        public IBot MapControllers()
        {
            var types = Assembly.GetCallingAssembly().GetTypes();
            List<Type> result = new List<Type>();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(BotControllerBase)))
                {
                    result.Add(type);
                }
            }
            _controllerMethods = result
                .SelectMany(t => t.GetMethods())
                .ToList();
            return this;
        }

        /// <summary>
        /// Runs the bot.
        /// </summary>
        /// <param name="token">Cancellation token (optional).</param>
        public void Run(CancellationToken token = default)
        {
            var botUser = _client.GetMeAsync().Result;
            _client.StartReceiving(UpdateHandler, ErrorHandler, cancellationToken: token);
            _logger.LogInformation("Bot '{botUser}' started - receiving updates.", botUser.Username);
            Task.Delay(-1, token).Wait(token);
        }

        private Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            _logger.LogError(exception, "Error occurred while receiving updates.");
            return Task.CompletedTask;
        }

        private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Message != null && !string.IsNullOrWhiteSpace(update.Message.Text) && update.Message.Text.StartsWith('/'))
            {
                _logger.LogInformation("Received text message: {Text}.", update.Message.Text);
                await HandleTextMessageAsync(update);
            }
            else if (update.CallbackQuery != null && update.CallbackQuery.Data != null)
            {
                _logger.LogInformation("Received inline query: {Data}.", update.CallbackQuery.Data);
                await HandleInlineQueryAsync(update);
            }
            else
            {
                _logger.LogWarning("Unsupported update type: {UpdateType}.", update.Type);
            }
        }

        private async Task HandleInlineQueryAsync(Update update)
        {
            InlineQueryHandler handler = new InlineQueryHandler(_controllerMethods, _serviceProvider);
            await handler.HandleAsync(update);
        }

        private async Task HandleTextMessageAsync(Update update)
        {
            TextMessageHandler handler = new TextMessageHandler(_controllerMethods, _serviceProvider);
            await handler.HandleAsync(update);
        }
    }
}