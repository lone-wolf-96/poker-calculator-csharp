# [Poker Calculator in C#](https://github.com/lone-wolf-96/poker-calculator-csharp/)

Demo that 'calculates' the winning poker hand between 2 players through algorithms.
This is the C# version of:

* [Poker Calculator in Java](https://github.com/lone-wolf-96/poker-calculator-java/)
* [Poker Calculator in Python](https://github.com/lone-wolf-96/poker-calculator-python/)
* [Poker Calculator in JavaScript](https://github.com/lone-wolf-96/poker-calculator-js/)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.
Probably needed: [Using .NET Core in Visual Studio Code](https://code.visualstudio.com/docs/languages/dotnet/).

### Installation

It requires no other installation than having .NET Core SDK latest stable release (2.2.300 by this date).

### Built with

* [Visual Studio Code](https://code.visualstudio.com/) was used for this, it requires extensions from [C# for Visual Studio Code](https://code.visualstudio.com/docs/languages/csharp/) - <https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp> or [C# Extensions](https://marketplace.visualstudio.com/items?itemName=jchannon.csharpextensions).

* For testing, [xUnit](https://xunit.net/docs/getting-started/netcore/cmdline/) was used. This may help: [Unit testing C# in .NET Core using dotnet test and xUnit](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test/).

### Usage

The process is simple, once is run, it will ask for the pokerdata.txt source directory and then, for the poker_results.txt target directory (pokerdata.txt was added as an example for file format).

Unit tests are included.

### Bear-in-mind

About generating launch.json file: [Get started with C# and Visual Studio Code](https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code#faq).

As Visual Studio Code Output Console cannot receive inputs, it's necessary you add this to the generated launch.json file to use the Integrated Terminal:

```json
"console": "integratedTerminal"
```

.Net Core CLI can only run .csproj files if it's in the same folder, or with the --project param indicating the location. For example:

`dotnet run --project ./[solution-name]/[project-name]/[file-name].csproj`

Otherwise it'll raise an error. By the way, this project requires to be run from the root directory. This may be a problem while debugging.

This extension could help for testing: [.NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer). This should be added to settings.json file.

```json
"dotnet-test-explorer.testProjectPath": "[solution-name]/[test-project-name]/[file-name].csproj"
```

## Authors

* **LoneWolf96** - *Final work* - [lone-wolf-96](https://github.com/lone-wolf-96/)

## Contributing

All kind of suggestions are welcome. This has academic purposes only.

## License

This project is licensed under the [GNU General Public License v3.0](https://choosealicense.com/licenses/gpl-3.0/).
