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

The divide stacks and confirm commands are special in that they use a number.  Divide is 99999 and confirm is 100000.

[h3]Command Binding Duplicate Note[/h3]

The Command Binding mode can use the same key for multiple entries.  For example, Disassembly and DiassemblyAll will not show up on the same menu and is safe to reuse the same key.

If the context menu has more than item with the same key bind, the first entry will be chosen.

[h2]Key List[/h2]

The list of valid keyboard keys can be found  at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

[h1]Command List[/h1]

The game currently supports the following commands for the Context Menu.
[code]
    Drop,
    Take,
    Equip,
    Unequip,
    Use,
    Reload,
    UnloadAmmo,
    Eat,
    FixWound,
    Amputate,
    Disassemble,
    DisassembleAll,
    DisassembleX1,
    Repair,
    UnlockDatadisk,
    SpillOnTheFloorX1,
    SpillOnTheFloorX5,
    ApplySkull,
    RemoveSkull,
    RemoveFire,
    Take_To_Drag,
    Augment,
    RemoveAugmentation,
    Implant,
    RemoveImplants,
    99999,
    100000
[/code]

Note: Commands 99,999 and 100,000 are "split stacks" and "split stacks confirm"

[h1]Support[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM-ContextMenuHotkeys

[h1]Change Log[/h1]

https://github.com/NBKRedSpy/QM-ContextMenuHotkeys/blob/master/CHANGELOG.md
