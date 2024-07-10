﻿using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace TelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Update"/>.
    /// </summary>
    public static class TelegramUpdateExtensions
    {
        /// <summary>
        /// Determines whether the update is a text message.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <returns>If the update is a text message, returns true; otherwise, false.</returns>
        public static bool IsTextMessage(this Update update)
        {
            return update.Message?.Text != null;
        }

        /// <summary>
        /// Determines whether the update is an inline query.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <returns>If the update is an inline query, returns true; otherwise, false.</returns>
        public static bool IsInlineQuery(this Update update)
        {
            return update.InlineQuery != null;
        }

        /// <summary>
        /// Tries to get the user who sent the update.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <param name="user">The user who sent the update.</param>
        /// <returns>If the user is not null, returns true; otherwise, false.</returns>
        public static bool TryGetUser(this Update update, out User user)
        {
            if (update.Message != null && update.Message.From != null)
            {
                user = update.Message.From;
                return true;
            }

            user = null!;
            return false;
        }
    }
}
