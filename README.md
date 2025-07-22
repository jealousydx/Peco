# Peco

A simple GUI wrapper for the CLI of [sing-box](https://github.com/SagerNet/sing-box) by SagerNet.  
Currently in **alpha stage**, it provides basic functionality to manage sing-box with ease.

## Features

- Load and edit configuration files
- Start and stop the core
- Automatically save settings on application exit to `C:\Users\<Username>\AppData\Local\Peco\`

## Supported Platforms

- Windows 10 (version 1607 or later) or Windows 11

## Requirements

- [.NET 8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.18-windows-x64-installer)


## Usage

- Make sure [.NET 8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.18-windows-x64-installer) is installed (unless you use the Self-Contained version), then run `Peco.exe`.

- Place `sing-box.exe` in the `core` folder (not required if you use version `with Core`).

- Load your configuration via **Config → Load** before starting the core.

---

## License

This project is licensed under [MIT LICENSE](./LICENSE).