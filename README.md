# OpenTracker
An open-source cross-platform tracking app for A Link to the Past Randomizer.

By request, I created a Discord server for the project.  https://discord.gg/VGjNVmKXgJ

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
- Add door shuffle support

## Getting Started

### Prerequisites

OpenTracker is a .NET 8.0 application.  You will be required to install a .NET runtime version 8.0 or greater.  You can find it at this link: https://dotnet.microsoft.com/download/dotnet

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

To start Autotracking, perform the following steps:

- Have QUSB2SNES or USB2SNES running and connected to your emulator or cartridge.
- Select "Auto-Tracker..." from the Tracker menu.
- Change the USB2SNES URI, if needed.  The default value will work for most.
- Click the "Connect" button.
- Click the "Get Devices" button.
- Select your device from the dropdown menu.
- If allowed, you can check the "Race Illegal Tracking" box.  DO NOT DO THIS IN COMPETITION UNLESS EXPLICITLY PERMITTED!
- Click the "Start Auto-Tracking" button.

Some notes about Autotracking:

- Please note the rules for competitions regarding auto-tracking.  I will do my best to provide options to disable forbidden functionality, when reported.  The developers of OpenTracker assume no responsibility for how the tool is used in competition.  In short, don't be a cheater!
- All Y-button inventory items are auto-tracked.
- All swords (unless swordless, see below), shields, and tunic upgrades are auto-tracked.
- Boots, Moon Pearl, Gloves, Flippers, and Half Magic Upgrade.
- Whether the Flute has been activated is auto-tracked.
- Small keys are autotracked for ROMs that support it.
  - Berserker's Multi-world/Archipelago 3.2.0 or higher.
  - alttpr.com 31.0.7 or higher.
- Big Keys are auto-tracked in all versions.
- The dungeon prize type is not auto-tracked, but whether or not the dungeon prize has been acquired from the dungeon is auto-tracked.
- All non-dungeon item locations are autotracked.
- OpenTracker v1.5 or newer supports an option for Race Illegal Tracking.  This enables tracking of dungeon item locations and should not be used in competitive races, unless it is explicitly permitted.
- All mode settings (including crystal requirements, swordless, etc.) are not autotracked.  This is to prevent providing information not available to the player in mystery seeds.
- Entrance locations in Entrance Shuffle mode are not auto-tracked.
- Take Any locations are not auto-tracked.
- Shop locations are not auto-tracked.

### Entrance Shuffle

Enabling the Entrance Shuffle mode will do the following.

- Scales down the size of all map locations to allow for the higher density.
- Stops displaying in-door overworld item locations.
- Starts displaying entrances that can be shuffled.

Each entrance can be marked with a number of images.  These images include all standard items, Agahnim, Ganon, text representing each dungeon entrance, and pictures representing most of the connectors.  When an image is selected, it will be displayed on the map attached to the entrance.  You can use this to mark points of interest for future use.

The standard colors still display on each location to indicate whether the location is accessible as an entrance given the current items and exits available.  However, all entrance locations (including those colored as inaccessible) can be cleared by right clicking the location, indicating that it is available as an exit.  This will allow you to determine what locations a new exit provides.

You can click and drag entrances together to connect them with a line.  This will allow you to mark connectors more clearly.

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
