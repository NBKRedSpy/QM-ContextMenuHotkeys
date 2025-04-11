[h1]Quasimorph Context Menu Hotkeys[/h1]


[h1]Info[/h1]

Adds hotkeys to the context menus.
The hotkeys can be configured in the config file noted below.

[h1]Configuration[/h1]

The configuration file is located at [i]%UserProfile%\AppData\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_ContextMenuHotkeys\QM_ContextMenuHotkeys.json[/i].
The file will be created the first time the game is run.

[h2]Settings[/h2]
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
[td]ConfigVersion
[/td]
[td]
[/td]
[td]Used internally
[/td]
[/tr]
[tr]
[td]CommandBinds
[/td]
[td]Key and Command (See config file)
[/td]
[td]The list of commands and their shortcut keys to invoke the command.  For example, D for Disassemble.  To not bind a command, set the Key to "None"
[/td]
[/tr]
[/table]

[h3]Important - Divide Stacks[/h3]

As of 0.8.6, the divide stacks command (aka split stacks internally) is now the value of 99999.
This is due to an internal change in the game.  Existing configs will have the SplitStacks value automatically updated to the number version.

[h3]Command Binding Duplicate Note[/h3]

The Command Binding mode can use the same key for multiple entries.  For example, Disassembly and DiassemblyAll will not show up on the same menu and is safe to reuse the same key.

If the context menu has more than item with the same key bind, the first entry will be chosen.

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
UnlockDatadisk
SpillOnTheFloorX1
SpillOnTheFloorX5
99999
ApplySkull
RemoveSkull
RemoveFire
Take_To_Drag
Augment
RemoveAugmentation
Install
[/code]

[h1]Support[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM-ContextMenuHotkeys

[h1]Change Log[/h1]

[h2]3.5.0[/h2]
[list]
[*]v0.8.6 compatibility
[*]Removed positional mode.
[*]Configs will automatically be changed to write any missing settings from older files.
[/list]

[h2]3.3.0[/h2]
[list]
[*]v0.8.5 compatible.
[/list]

[h2]3.3.0[/h2]
[list]
[*]Moved config file directory.
[/list]

[h2]3.2.0[/h2]
[list]
[*].8 compatible.
[/list]

[h2]3.1.0[/h2]
[list]
[*]Simplified enabling modes with a single setting.
[*]Supports upgrading the configuration schema.
[*]Added every context menu command in the config with unbound items set to KeyCode.None.
[*]Highlights the hotkey on the menu items.
[/list]

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
