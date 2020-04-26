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

NOTE: The Windows MSI has been known to not install properly over an existing install some of the time.  If you run into odd issues, try uninstalling and reinstalling the program to see if that resolves it and report the issue here.

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

To run the application, run the OpenTracker binary file.  It will be located in the extracted folder, if you used the tarball.  It will be located in your distribution's X application folder (usually /usr/share), if you installed the package.

### MacOS

Coming soon...

## How it Works

### General

Some notable differences from other map trackers:

- The tracker assumes that fake flippers is always possible (except for entry to Swamp Palace).  This is because there are so many Splash Deletion setups that the logic would be very complicated.  There is room for development on this.
- Game mode settings can be changed by clicking the Gear icon on the top right of the Items panel and modifying the settings listed.
- The Dungeon Item Placement settings change the number of available checks in a dungeon to the number of non-dungeon items applicible for that mode.  (e.g. If Standard Dungeon Item Placement is set, Palace of Darkness has 5 items.  If Maps/Compasses is set, PoD has 7 items.)
- To allow for mystery seed friendliness, all mode settings can be changed without affecting the tracker state.  Modifying Dungeon Item Placement adds or subtracts remaining items from relevant dungeons when the mode is changed.
- More complete Retro support is planned for the future.  The current design intent is to add a generic key to the Items panel to represent keys in inventory and to use the Small Key shuffle icons for each dungeon to represent keys used in the dungeon.  This is coming soon.
- Currently, you are assumed to always have the Rupee quiver in Retro mode.  This will likely change when more complete Retro support is added.

### Autotracking

Autotracking support is available currently.  QUSB2SNES or USB2SNES are required to connect the tracker to your game currently.  I don't plan to add support for direct Lua connectors at this time, as QUSB2SNES can facilitate connections to those emulators already.  If there is enough demand, this may change.

To start Autotracking, have QUSB2SNES or USB2SNES open and connected to your game, then select "Autotracker..." from the Tracker menu.  Click Start on the Autotracker window that pops up.

Some notes about Autotracking:

- Autotracking will track most inventory items.
- The Retro mode rupee quiver is not autotracked.
- Small keys are not autotracked in any way.  The game only stored currently held keys and this doesn't work for our purposes.
- Big keys are autotracked.
- Autotracking will not track the prize of a dungeon, this needs to be manually tracked.
- Autotracking will track all non-dungeon item locations.
- Dungeons (including Hyrule Castle, Agahnim's Tower, and Ganon's Tower) will need to be manually tracked.
- An issue has been occurring that causes the Items panel UI to not show the correct image when an item is added via Autotracking.  If this occurs, you can manipulate the item manually to correct this.

## Development

OpenTracker is developed in the Avalonia framework.  In order to develop using Visual Studio, you'll need to install the Avalonia extensions in the Extensions menu of Visual Studio.

OpenTracker follows the Model-View-ViewModel (MVVM) pattern as closely as possible.  The solution is separated into 3 projects.

- OpenTracker - This contains the View and ViewModel code and is responsible for presenting the data to the user in the GUI.
- OpenTracker.Models - This contains all the Model classes and is responsible for all of the tracking state data.
- OpenTracker.Setup - This is a Visual Studio Setup project used for creating the .MSI for Windows users.

## License

This project is licensed under the MIT License

## Acknowledgments

- My wife - Thank you for your help testing and providing feedback.  Thank you for putting up with me pouring free time into this project and this old game.
- Serafina Bui - Thank you for your help with the visual presentation.
- KatDevsGames - Thank you for developing ConnectorLib.  It was a great reference for my Autotracker connector code.
- EmoSaru - Thank you for creating EmoTracker and providing inspiration for this app.
