# Tunic Replay Plugin

This is a BepInEx plugin that replays a Tunic replay file (.trp).

## Install

1. Install BepInEx
    - Download the correct version of BepInEx from here: https://builds.bepinex.dev/projects/bepinex_be/572/BepInEx_UnityIL2CPP_x64_9c2b17f_6.0.0-be.572.zip
    - Extract the zip folder you downloaded from the previous step into your game's install directory
        - For example: `C:\Program Files (x86)\Steam\steamapps\common\TUNIC`
    - Launch Tunic and close it. This will finalize the BepInEx installation.

2. Install the Tunic Replay plugin
    - Download the `TunicReplayPlugin.dll` file from the latest release
    - Copy it into `BepInEx/plugins` under your game's install directory
    - Launch Tunic again and you should see a small options window on the title screen!

## Add Tunic Replay files

1. Create a `TunicReplays` folder on your Desktop
2. Copy one or more `.trp` file into the `TunicReplays` folder
    - Example files can be found at https://github.com/jabberrock/TunicReplayUploads. Click the "Download raw file" button to download a file.

## Play against a Replay

1. Start Tunic
2. Select how you would like to race against the replay:
    - Game Time - the ghost follows the original recorded run
    - Segment Time - the ghost waits for you at every save/load point
    - Best Segment Time - the ghost picks the best segment across all replay files

## Uninstall

1. To uninstall, either:
    - Delete `TunicReplayPlugin.dll` from `BepInEx/plugins`, or
    - Rename `winhttp.dll` file located in the game's root directory (this will disable all installed mods).
