[h1]Quasimorph Context Menu Hotkeys[/h1]


[h1]Info[/h1]

Adds hotkeys to the context menus.

Supports both command binding (Q = Unequip) and positional binding (second item is 2).

By default the command binding is enabled.

The key binds and the modes can be changed in the configuration file.

[h1]Update For Existing Users[/h1]

The latest update defaults to "command bind" mode instead of "both".
The configuration has been simplified and now includes all context menu commands in the configuration file.
The mod will automatically upgrade the existing configuration file to the latest version.

Use the new EnableNumberedMode and EnableCommandMode value to toggle each mode.  It is no longer required to modify the key binds to disable a mode.

[h1]Positional Mode Modifier Keys[/h1]

Any command with a '+' requires holding down the number key and the alt or shift key to activate.
This is to safeguard against accidentally invoking disassembly commands.

The modifier keys (shift, alt, etc.) and the list of commands that require a modifier key can be configured.

[h1]Configuration[/h1]

The configuration file is located at [i]%UserProfile%\AppData\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_ContextMenuHotkeys\QM_ContextMenuHotkeys.json[/i].
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
[td]ConfigVersion
[/td]
[td]
[/td]
[td]Used internally
[/td]
[/tr]
[tr]
[td]EnableNumberedMode
[/td]
[td]false
[/td]
[td]Set to true to enable the positional hotkeys.  Ex: 2 = second item
[/td]
[/tr]
[tr]
[td]EnableCommandMode
[/td]
[td]true
[/td]
[td]Set to true to enable command binding hotkeys.  Ex:  D = Drop
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
[tr]
[td]CommandN
[/td]
[td]0-9
[/td]
[td]Key bindings for the positional mode. Numbered from top to bottom.
[/td]
[/tr]
[tr]
[td]ModifierKeys
[/td]
[td]Shift, Alt
[/td]
[td]Positional Mode only. The list of keys that count as a modifier.
[/td]
[/tr]
[/table]

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

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM-ContextMenuHotkeys

[h1]Change Log[/h1]

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
