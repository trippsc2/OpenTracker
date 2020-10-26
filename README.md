# OpenTracker
An open-source cross-platform tracking app for A Link to the Past Randomizer.

This project is intended to provide a tracker with the following features:
- Mystery seed friendly
- Support for most game modes (No Glitches logic only and no Door shuffle yet)
- Autotracking
- Support for Windows, Linux, and MacOS
- Customizable from the GUI

The following is on my roadmap for future updates:
- Stream Capture view
  - Provide a Window view that is friendly to capture with OBS.
  - Consider NDI support for remote streaming.
  - Allow Stream Capture window to be customized from the GUI.
- Improved package manager support
  - Linux packages available in a repository for as many distros as possible
  - Add software to the Apple App Store, if possible
  - Create Chocolatey package in the community repository for Windows
- Add glitched logic options
  - Add Overworld/Major Glitches logic to the tracker
  - This feature will not be taken on until the v32 graph-based logic is made public, as I will be converting my logic to follow Veetorp's lead.

## Getting Started

### Prerequisites

OpenTracker is a .NET Core 3.1 application.  You will be required to install a .NET Core runtime version 3.1 or greater.  You can find it at this link: https://dotnet.microsoft.com/download/dotnet-core

### Windows

- Download the latest .msi release.
- Follow the install wizard to install the application.
- To run, click the shortcut placed on your desktop or in your Start menu.

NOTE: The Windows MSI has been known to not install properly over an existing install some of the time.  If you run into odd issues after an update, try uninstalling and reinstalling the program to see if that resolves it and report the issue here.

### Linux

If you are running a Debian-based (Ubuntu, Mint, PopOS, etc.) distribution, run the following commands:

```
wget https://github.com/trippsc2/OpenTracker/releases/download/<version>/OpenTracker.<version>.deb
sudo apt install ./OpenTracker.<version>.deb
```

If you are running a RHEL-based (Fedora, etc.) distribution, run the following commands:

```
wget https://github.com/trippsc2/OpenTracker/releases/download/<version>/OpenTracker.<version>.rpm
sudo rpm -i ./OpenTracker.<version>.rpm
```

If you are running a different distribution, run the following commands:

```
wget https://github.com/trippsc2/OpenTracker/releases/download/<version>/OpenTracker.<version>.tar.gz
tar xvzf ./OpenTracker.<version>.tar.gz
```

To run the application, run the OpenTracker binary file.  It will be located in the extracted folder, if you used the tarball.  It will be located in your distribution's X application folder (usually /usr/share), if you installed the package.

### MacOS

Download the OpenTracker.\<version\>.macOS.zip file from the Releases page.  This contains an .app bundle that can be moved to your Applications folder or run directly.

## How it Works

### General

Some notable differences from other map trackers:

- To set the dungeon medallion requirements, right click on the medallion that is required until it has the correct text overlayed (MM, TR, or BOTH).
- You can bypass the logic by holding CTRL while clearing or collecting a location.  This will allow you to clear inaccessible locations.
- Game mode settings can be changed by clicking the Gear icon on the top right of the Items panel and modifying the settings listed.
- The Dungeon Item Placement settings change the number of available checks in a dungeon to the number of non-dungeon items applicible for that mode.  (e.g. If Standard Dungeon Item Placement is set, Palace of Darkness has 5 items.  If Maps/Compasses is set, PoD has 7 items.)
- To allow for mystery seed friendliness, all mode settings can be changed without affecting the tracker state.  Modifying Dungeon Item Placement adds or subtracts remaining items from relevant dungeons when the mode is changed.
- With Generic Keys enabled, you can use the Small Key icon in the Items panel to indicate the number of keys in inventory.  Any keys used in a dungeon can be tracked using the dungeon key icons.  The dungeon logic will take the total of the generic keys and dungeon keys, so to give you a better sense of what you can currently do.
- The Rupee quiver is assumed to be had in Retro mode, but is trackable by right clicking the Bow icon in the Items panel.

### Autotracking

Autotracking support is available currently.  QUSB2SNES or USB2SNES are required to connect the tracker to your game currently.  I don't plan to add support for direct Lua connectors at this time, as QUSB2SNES can facilitate connections to those emulators already.  If there is enough demand, this may change.

To start Autotracking, have QUSB2SNES or USB2SNES open and connected to your game, then select "Autotracker..." from the Tracker menu.  Click Start on the Autotracker window that pops up.

Some notes about Autotracking:

- Please note the rules for competitions regarding autotracking.  I will do my best to provide options to disable forbidden functionality, when reported.
- All inventory items are autotracked.
- Small keys are autotracked for ROMs that support it.
  - Berserker's Multiworld 3.2.0 or higher.
  - alttpr.com 31.0.7 or higher will support this.
- Big Keys are autotracked in all versions.
- The dungeon prize type is not autotracked, but whether or not the dungeon prize has been acquired from the dungeon is autotracked.
- All non-dungeon item locations are autotracked.
- OpenTracker v1.5 or newer supports an option for Race Illegal Tracking.  This enables tracking of dungeon item locations and should not be used in competitive races, unless it is allowed.
- All mode settings (including crystal requirements, swordless, etc.) are not autotracked.  This is to prevent providing information not available to the player in mystery seeds.
- Entrance locations in Entrance Shuffle mode are not autotracked.
- Take Any locations are not autotracked.

### Entrance Shuffle

Enabling the Entrance Shuffle mode will do the following.

- Scales down the size of all map locations to allow for the higher density.
- Stops displaying in-door overworld item locations.
- Starts displaying entrances that can be shuffled.

Each entrance can be marked with a number of images.  The images are of all inventory items, Ganon, Agahnim, and text representing each of the dungeon entrances.  When an image is selected, it will be displayed on the map instead of the square.  This allows for you to use your own system for marking entrance locations for future use and be able to see the markings you made at a glance.

The standard colors still display on each location to indicate whether the location is accessible as an entrance given the current items and exits available.  However, all entrance locations (including those colored as inaccessible) can be cleared by right clicking the location, indicating that it is available as an exit.  This will allow you to determine what locations a new exit provides.

### UI Color Themes

In version 1.1.0, UI color themes were introduced.  Themes are encoded in a XAML file stored in the Themes subfolder of the program folder.  Any custom themes that you create and place in that folder will be available to choose in the menu.

The themes are extended from the Avalonia themes generated by this project: https://github.com/wieslawsoltes/ThemeEditor

The following lines need to be added and customized to the themes generated for storing the color of the Items panel background and the Locations panel background (see existing themes for file structure):

```
    <Color x:Key="ItemsPanelColor">#FF2D4848</Color>
    <Color x:Key="LocationsPanelColor">#FF233939</Color>

    <SolidColorBrush x:Key="ItemsPanelBrush" Color="{DynamicResource ItemsPanelColor}"></SolidColorBrush>
    <SolidColorBrush x:Key="LocationsPanelBrush" Color="{DynamicResource LocationsPanelColor}"></SolidColorBrush>
```

If you'd like to see a theme you created be included in the official release, please send it to me via an Issue or Pull Request on here or by a DM on Discord (Tripp#6120) or Reddit (trippsc2).

## Development

OpenTracker is developed in the Avalonia framework.  In order to develop using Visual Studio, you'll need to install the Avalonia extensions in the Extensions menu.

OpenTracker follows the Model-View-ViewModel (MVVM) pattern as closely as possible.  The solution is separated into 3 projects.

- OpenTracker - This project contains all GUI-specific code.  All View and ViewModel classes, GUI utility classes/types, and GUI-specific Model classes/types will be a part of this project.
- OpenTracker.Models - This contains all GUI non-specific Model classes/types.  This project can be used to port the project to another UI framework without reliance on any Avalonia libraries.
- OpenTracker.Setup - This is a Visual Studio Setup project used for creating the .MSI for Windows users.

## License

This project is licensed under the MIT license.

## Acknowledgments

- My wife - Thank you for your help testing and providing feedback.  Thank you for putting up with me pouring free time into this project and this old game.
- Serafina Bui - Thank you for your help with the visual presentation.
- Derian Meyer - Thank you for allowing me to bounce ideas off you and for your help with testing and feedback.
- Sara Meyer - Thank you for your help with testing and providing new Rando player feedback.
- Kyle (Kerigyl) - Thank you for your help with testing and providing feedback.
- KatDevsGames - Thank you for developing ConnectorLib.  It was a great reference for my Autotracker connector code.
- EmoSaru - Thank you for creating EmoTracker and providing inspiration for this app.
- Avalonia team - Thank you for delivering a cross-platform desktop app framework for .NET programmers.
- Wiesław Šoltés - Thank you for creating all of the various Avalonia extension libraries.  Your work has been extremely helpful to this project!
- codemann8 - Thank you for allowing me to use your assets for marking entrances!  It is much appreciated!
