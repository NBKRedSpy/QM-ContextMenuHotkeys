
# Quasimorph Context Menu Hotkeys

![All Menu Versions](media/All%20Modes.png)

# Info
Adds hotkeys to the context menus.  Both positional (0-9) as well as well as by command (E = equip).
The keys for both positional and by command can be changed in the config.
Either can be disabled.  See the configuration section for more info.

Given the top item in image, which has both modes enabled:
* Pressing 1 will equip.
* Pressing E will also equip.
* Pressing 3 + Shift or Alt will Disassemble the item.
  * The shift is for protection against accidental destructive commands and can be disabled.

# Configuration Upgrade Note

This section will only affect users of the previous version of the mod and want to modify the settings in the new version.

The new version of the mod will not upgrade the previous version of the configuration.  

To get all the new options, delete or rename the config file.  The next time the game is run, a fresh config file will be created.

## Modifier Keys
Any command with a '+' requires holding down an alt or shift key to activate.
This is to safeguard against accidentally invoking disassembly commands.

The modifier keys (shift, alt, etc.) and the list of commands that require a modifier key can be configured.

# Configuration

The configuration file is located at ```%UserProfile%\AppData\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_ContextMenuHotkeys.json```.
The file will be created the first time the game is run.

# Disabling Modes

To disable the positional options (0-9), set all CommandN values to ```"None"``` .
to disable all Command Binds (E = Equip), set the CommandBinds to ```[]``` .

Deleting the entries instead of making the modifications above will cause the mod to still use the defaults.

## Settings
Note: if the config file does not have all of the settings below, delete the file.  When the game is next run, a new config file with all options will be created.


|Name|Default|Description|
|--|--|--|
|CommandN|0-9|The key to activate a menu item.  Numbered from top to bottom.|
|ModifierCommands|Disassemble*, UnlockDataDisk|The list of commands to require a modifier key to be held.  Ex: Alt + 1. This only affects the CommandN items.  See the Command List section below |
|ModifierKeys|Shift, Alt|The modifier keys for the ModifierCommands|
|CommandBinds|(See config file)|The list of commands and their shortcut key to invoke the command.  For example, D for Disassemble.|

### Command Binding Duplicate Note
The Command Binding (E = Equip) can use the same key for multiple entries.  For example, Disassembly and DiassemblyAll will not show up on the same menu.

If the context menu has items with duplicate keys, the first entry will be chosen.

## Key List
The list of valid keyboard keys can be found  at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

# Command List
The game currently supports the following commands for the Context Menu.

```
Drop
Take
Equip
Unequip
Use
Reload
UnloadAmmo
Eat
FixWound
Amputate
Disassemble
DisassembleAll
DisassembleX1
Repair
SplitStacks
SplitStacksConfirm
UnlockDatadisk
SpillOnTheFloorX1
SpillOnTheFloorX5
ApplySkull
RemoveSkull
RemoveFire
```

These command identifiers are used for both the Command binds as well as the commands that require a <number> + Modifier Key.


# Source Code
Source code is available on GitHub https://github.com/NBKRedSpy/QM-ContextMenuHotkeys

# Change Log

## 3.0.0
* Supports Command Binding.

## 2.0.0

* Added list for commands that must have a modifier.  Defaults to destructive items.

* Improved the command parsing.  Now avoids invisible cached commands.



