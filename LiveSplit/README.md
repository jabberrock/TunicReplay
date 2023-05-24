# TUNIC Replay Auto-Splitter for LiveSplit

This auto-splitter script will capture the positions of the player throughout a
run and save the replay as a `.trp` file in `Desktop\TunicReplays`.

## Install

1. Start LiveSplit
2. If you are using the TUNIC auto-splitter that is automatically downloaded by
   LiveSplit, you need to first disable it:
   1. Right click -> Edit Splits
   2. Clear out the "Game Name" field so that it is emtpy
   3. Press "OK"
3. Download the files needed for the TUNIC Replay auto-splitter:
   1. Download `TUNIC_Replay.asl` from https://github.com/jabberrock/TunicReplay
   2. Download `asl-help` from https://github.com/just-ero/asl-help/raw/main/lib/asl-help
   3. Copy both files into the `Components` directory of your LiveSplit directory
4. Load the TUNIC Replay auto-splitter script into LiveSplit:
   1. Right click -> Edit Layout
   2. Add "Scriptable Auto Splitter", and double click on it
   3. Click "Browse" and select `TUNIC_REPLAY.asl` so that it shows up in the "Script Path" field
   4. Select any events you normally use

## Uninstall

1. Remove the TUNIC Replay auto-splitter script from LiveSplit:
   1. Right click -> Edit Layout
   2. Remove the "Scriptable Auto Splitter"
   3. Add back the default TUNIC auto-splitter:
      1. Right click -> Edit Splits
      2. Type "TUNIC" into the "Game Name" field
      3. Select any events you normally use

## Create a Replay file

To create a replay file, just use LiveSplit like you would on any normal run.
You can use either a vanilla TUNIC instance (valid runs), or a TUNIC instance
that is modded (invalid runs).

1. Whenever you start a run in LiveSplit, a new `.trp` file will be created in
   `Desktop\TunicReplays`.
2. When you complete or reset the run, LiveSplit will finish writing the replay,
   close the file. If you try to access the file before then, Windows will say
   that the file is in use.
3. Just compress the file (right click -> Compress to ZIP file) and share it!

## Using a Replay file

1. Create the `Desktop\TunicReplays` directory if it doesn't exist.
2. Uncompress the replay into the directory.
3. Start TUNIC with the Replay BepInEx plugin (follow instruction on the plugin).
4. When you start a New Game or load a NG+ Game, you should see a "ghost"
   following the path of the replay.

It is not necessary to have LiveSplit running.
