# OpenTracker
An open-source cross-platform tracking app for A Link to the Past Randomizer.

This project is intended to provide a tracker with the following features:
- Mystery seed friendly
- Support for most game modes (no Door shuffle yet)
- Autotracking
- Support for Windows, Linux, and MacOS
- Customizable from the GUI

The following is on my roadmap for future updates:
- Improvements to AutoTracking (stability, detect whether ALttP is running, better logging, etc.)
- Stream Capture view (a view of the tracker that can be captured more easily on stream and edited in GUI)
- Color Themes (preset color sets to choose from)
- Improvements to package manager support (APT/YUM repo, package requirements added to .deb and .rpm, AUR inclusion, App Store inclusion, and Chocolatey package for Win)

## Getting Started

### Prerequisites

OpenTracker is a .NET Core 3.1 application.  You will be required to install a .NET Core runtime version 3.1 or greater.  You can find it at this link: https://dotnet.microsoft.com/download/dotnet-core

### Windows

- Download the latest .msi release.
- Follow the install wizard to install the application.
- To run, click the shortcut placed on your desktop or in your Start menu.

### Linux

If you are running a Debian-based (Ubuntu, Mint, PopOS, etc.) distribution, run the following commands:

```
wget https://github.com/trippsc2/OpenTracker/releases/download/<version>-beta/OpenTracker.<version>.deb
sudo apt install ./OpenTracker.<version>.deb
```

If you are running a RHEL-based (Fedora, etc.) distribution, run the following commands:

```
wget https://github.com/trippsc2/OpenTracker/releases/download/<version>-beta/OpenTracker.<version>.rpm
sudo rpm -i ./OpenTracker.<version>.rpm
```

If you are running a different distribution, run the following commands:

```
wget https://github.com/trippsc2/OpenTracker/releases/download/<version>-beta/OpenTracker.<version>.tar.gz
tar xvzf ./OpenTracker.<version>.tar.gz
```

To run the application, run the OpenTracker binary file.  It will be located in the extracted folder, if you used the tarball.  It will be located in your distribution's X application folder (usually /usr/local/share), if you installed the package.

### MacOS

Coming soon...

## Development

OpenTracker is developed in the Avalonia framework.  In order to develop using Visual Studio, you'll need to install the Avalonia extensions in the Extensions menu of Visual Studio.

## License

This project is licensed under the MIT License

## Acknowledgments

- EmoSaru - Thank you for creating EmoTracker and providing inspiration for this app
