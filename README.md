<a id="readme-top"></a>

# TelegramBot.NET
Ready-to-use library for convenient development of Telegram bots.

# Purposes
Many people know the ASP.NET Core platform and its convenience for developing web API applications.

I came up with the idea to implement a similar message processing pattern for telegram bots.

# Getting Started
Start by importing the library into your project

`dotnet add package TelegramBot.NET --version 1.0.1`

1. Implement simple handler in your `Program.cs`

```CSharp
static void Main(string[] args)
{
    BotBuilder builder = new BotBuilder(args)
        .UseApiKey(x => x.FromConfiguration());

    var app = builder.Build();
    app.MapControllers();
    app.Run();
}
```

2. Add your API key from [BotFather](https://t.me/BotFather) to `appsettings.json` file, key is `TelegramBotToken`:

```JSON
{
  "TelegramBotToken": "YOUR_API_TOKEN"
}
```

or use command line arguments:

```Bash
./TelegramBot.Console TelegramBotToken=YOUR_API_TOKEN
```

3. Implement controller, in this sample - for handling `/start` command:

```CSharp
public class CommandController(ILogger<CommandController> _logger) : BotControllerBase
{
    [BotCommand("/start")]
    public async Task<IActionResult> HandleStartAsync()
    {
        _logger.LogInformation("Start command received.");
        await Task.Delay(1000);
        return Text("Hello!");
    }
}
```

4. Run application - and see result:

```Bash
info: TelegramBot.BotApp[0]
      Bot started - receiving updates.
info: TelegramBot.ConsoleTest.Controllers.HomeController[0]
      Start command received.
```

<p align="right"><a href="#readme-top">back to top</a></p>

## Roadmap

- [x] Add command handlers
- [ ] Add command query params
- [ ] Add response types:
    - [X] Text
    - [ ] Inline

See the [open issues](https://github.com/BigMakCode/TelegramBot.NET/issues) for a full list of proposed features (and known issues).

<p align="right"><a href="#readme-top">back to top</a></p>

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right"><a href="#readme-top">back to top</a></p>

# License

Distributed under the MIT License. See LICENSE.md for more information.

# Contact

[E-Mail](mailto:github-telegram-bot-net@belov.us)