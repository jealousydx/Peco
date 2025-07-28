# Peco

A simple GUI wrapper for the CLI of [sing-box](https://github.com/SagerNet/sing-box) by SagerNet.

## Features

- Load and edit configuration files

- Start and stop the core

- `Validation` of the config file before starting `sing-box.exe`

- Switch between `Tun` and `System Proxy` modes

- View real-time logs of the core

- Automatically save settings on application exit to `C:\Users\<Username>\AppData\Local\Peco\`

## Supported Platforms

- Windows 10 (version 1607 or later) or Windows 11

## Requirements

- [.NET 8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.18-windows-x64-installer), `unless you are using the Self-Contained version`.

- Your `own` config.json file (There is no built-in config generator `yet`. You can create it yourself using the documentation on [sing-box.sagernet.org](https://sing-box.sagernet.org/configuration/))

## Usage

- Run `Peco.exe`

- Load your config file via `Config → Load` before starting the core.

- Click `On`

## License

This project is licensed under [MIT LICENSE](./LICENSE).
