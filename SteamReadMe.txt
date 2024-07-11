[h1]Quasimorph Context Menu Hotkeys[/h1]


[h1]Info[/h1]

Adds hotkeys to the context menus.

Ex: Pressing the 1 key for "1. Unequip" will unequip the item.
Ex: Pressing Alt+3 for the "3+. Disassemble" will disassemble the item.

The keys default to 0-9, and modifiers to Shift or Alt.  Can be customized in the configuration.

[h2]Modifier Keys[/h2]

Any command with a '+' requires holding down an alt or shift key to activate.
This is to safeguard against accidentally invoking disassembly commands.

The modifier keys (shift, alt, etc.) and the list of commands that require a modifier key can be configured.

[h1]Configuration[/h1]

The configuration file is located at [i]%UserProfile%\AppData\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_ContextMenuHotkeys.json[/i].
The file will be created the first time the game is run.

[h2]Settings[/h2]

Note: if the config file does not have all of the settings below, delete the file.  When the game is next run, a new config file with all options will be created.
[table]
[tr]
[td]Name
[/td]
[td]Default
[/td]
[td]Description
[/td]
[/tr]
[tr]
[td]CommandX
[/td]
[td]0-9
[/td]
[td]The activation key that matches the number.  Ex: 1. Disassemble uses the 1 key.
[/td]
[/tr]
[tr]
[td]ModifierCommands
[/td]
[td]Disassemble*, UnlockDataDisk
[/td]
[td]The list of commands to require a modifier key to be held.  Ex: Alt + 1.  See the Command List section below
[/td]
[/tr]
[tr]
[td]ModifierKeys
[/td]
[td]Shift, Alt
[/td]
[td]The modifier keys for the ModifierCommands
[/td]
[/tr]
[/table]

[h2]Key List[/h2]

The list of valid keyboard keys can be found  at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

[h1]Command List[/h1]

The game currently supports the following commands for the Context Menu.
[code]
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
[/code]

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM-ContextMenuHotkeys

[h1]Change Log[/h1]

[h2]2.0.0[/h2]
[list]
[*]
Added list for commands that must have a modifier.  Defaults to destructive items.
[*]
Improved the command parsing.  Now avoids invisible cached commands.
[/list]
