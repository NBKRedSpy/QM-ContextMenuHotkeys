[h1]Quasimorph Context Menu Hotkeys[/h1]


[h1]Info[/h1]

Adds hotkeys to the context menus.  Both positional (0-9) as well as well as by command (E = equip).
The keys for both positional and by command can be changed in the config.
Either can be disabled.  See the configuration section for more info.

Given the top item in image, which has both modes enabled:
[list]
[*]Pressing 1 will equip.
[*]Pressing E will also equip.
[*]Pressing 3 + Shift or Alt will Disassemble the item.
[list]
[*]The shift is for protection against accidental destructive commands and can be disabled.
[/list]
[/list]

[h1]Configuration Upgrade Note[/h1]

This section will only affect users of the previous version of the mod and want to modify the settings in the new version.

The new version of the mod will not upgrade the previous version of the configuration.

To get all the new options, delete or rename the config file.  The next time the game is run, a fresh config file will be created.

[h2]Modifier Keys[/h2]

Any command with a '+' requires holding down an alt or shift key to activate.
This is to safeguard against accidentally invoking disassembly commands.

The modifier keys (shift, alt, etc.) and the list of commands that require a modifier key can be configured.

[h1]Configuration[/h1]

The configuration file is located at [i]%UserProfile%\AppData\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_ContextMenuHotkeys.json[/i].
The file will be created the first time the game is run.

[h1]Disabling Modes[/h1]

To disable the positional options (0-9), set all CommandN values to [i]"None"[/i] .
to disable all Command Binds (E = Equip), set the CommandBinds to [i][][/i] .

Deleting the entries instead of making the modifications above will cause the mod to still use the defaults.

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
[td]CommandN
[/td]
[td]0-9
[/td]
[td]The key to activate a menu item.  Numbered from top to bottom.
[/td]
[/tr]
[tr]
[td]ModifierCommands
[/td]
[td]Disassemble*, UnlockDataDisk
[/td]
[td]The list of commands to require a modifier key to be held.  Ex: Alt + 1. This only affects the CommandN items.  See the Command List section below
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
[tr]
[td]CommandBinds
[/td]
[td](See config file)
[/td]
[td]The list of commands and their shortcut key to invoke the command.  For example, D for Disassemble.
[/td]
[/tr]
[/table]

[h3]Command Binding Duplicate Note[/h3]

The Command Binding (E = Equip) can use the same key for multiple entries.  For example, Disassembly and DiassemblyAll will not show up on the same menu.

If the context menu has items with duplicate keys, the first entry will be chosen.

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

These command identifiers are used for both the Command binds as well as the commands that require a <number> + Modifier Key.

[h1]Support[/h1]

If you enjoy my mods and want to leave a tip, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM-ContextMenuHotkeys

[h1]Change Log[/h1]

[h2]3.0.0[/h2]
[list]
[*]Supports Command Binding.
[/list]

[h2]2.0.0[/h2]
[list]
[*]
Added list for commands that must have a modifier.  Defaults to destructive items.
[*]
Improved the command parsing.  Now avoids invisible cached commands.
[/list]
