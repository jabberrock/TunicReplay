state("TUNIC") {}
state("Secret Legend") {}

startup
{
	// Adapted from Ero's implementation at https://github.com/just-ero/asl/tree/main/TUNIC

	Assembly.Load(File.ReadAllBytes("Components/asl-help")).CreateInstance("Unity");
	vars.Helper.LoadSceneManager = true;
	
	string[,] _settings =
	{
		{ null, "Key Items", "Key Items" },
			{ "Key Items", "inventory quantity Wand : 1",                                          "Magic Orb" },
			{ "Key Items", "inventory quantity Techbow : 1",                                       "Fire Wand" },
			{ "Key Items", "Achievement_2 : 1",                                                    "Sword" },
			{ "Key Items", "Achievement_12 : 1",                                                   "Hyperdash (Hero's Laurels)" },
			{ "Key Items", "inventory quantity SlowmoItem : 1",                                    "Hourglass" },
			{ "Key Items", "inventory quantity Shield : 1",                                        "Shield" },
			{ "Key Items", "inventory quantity Lantern : 1",                                       "Lantern" },
			{ "Key Items", "inventory quantity Stick : 1",                                         "Stick" },
			{ "Key Items", "inventory quantity Mask : 1",                                          "Mask" },
			{ "Key Items", "inventory quantity Shotgun : 1",                                       "Gun" },
			{ "Key Items", "inventory quantity Stundagger : 1",                                    "Ice Dagger" },
			{ "Key Items", "inventory quantity Hexagon Green : 1",                                 "Hexagon Green" },
			{ "Key Items", "inventory quantity Hexagon Red : 1",                                   "Hexagon Red" },
			{ "Key Items", "inventory quantity Hexagon Blue : 1",                                  "Hexagon Blue" },
			// Hero Relics - I'm honestly not sure which is which for Water/Crown/Sword
			{ "Key Items", "inventory quantity Relic - Hero Sword : 1",                            "Hero Relic Attack" },
			{ "Key Items", "inventory quantity Relic - Hero Crown : 1",                            "Hero Relic Defense" },
			{ "Key Items", "inventory quantity Relic - Hero Water : 1",                            "Hero Relic Potion" },
			{ "Key Items", "inventory quantity Relic - Hero Pendant HP : 1",                       "Hero Relic HP" },
			{ "Key Items", "inventory quantity Relic - Hero Pendant SP : 1",                       "Hero Relic SP" },
			{ "Key Items", "inventory quantity Relic - Hero Pendant MP : 1",                       "Hero Relic MP" },

		{ null, "Major Events", "Major Events" },
			{ "Major Events", "Achievement_4 : 1",                        "Rung Bell 1 (East)" },
			{ "Major Events", "Achievement_5 : 1",                        "Rung Bell 2 (West)" },
			{ "Major Events", "SV_Fortress Arena_Spidertank Is Dead : 1", "Killed Siege Engine" },
			{ "Major Events", "Librarian Dead Forever : 1",               "Killed Librarian" },
			{ "Major Events", "SV_ScavengerBossesDead : 1",               "Killed Boss Scavenger" },
			{ "Major Events", "Has Died To God : 1",                      "Died to Heir" },
			{ "Major Events", "Placed Hexagon 1 Red : 1",                 "Placed Hexagon 1 Red" },
			{ "Major Events", "Placed Hexagon 2 Green : 1",               "Placed Hexagon 2 Green" },
			{ "Major Events", "Placed Hexagon 3 Blue : 1",                "Placed Hexagon 3 Blue" },
			{ "Major Events", "Placed Hexagons ALL : 1",                  "Placed Hexagons ALL" },
			{ "Major Events", "Achievement_13 : 1",                       "Restored Corporeal Form" },

		{ null, "Cards", "Cards" },
			{ "Cards", "inventory quantity Trinket - MP Flasks : 1",              "Inverted Ash" },
			{ "Cards", "inventory quantity Trinket - Glass Cannon : 1",           "Glass Cannon" },
			{ "Cards", "inventory quantity Trinket - BTSR : 1",                   "Cyan Peril Ring" },
			{ "Cards", "inventory quantity Trinket - Heartdrops : 1",             "Lucky Cup" },
			{ "Cards", "inventory quantity Trinket - Bloodstain Plus : 1",        "Louder Echo" },
			{ "Cards", "inventory quantity Trinket - RTSR : 1",                   "Orange Peril Ring" },
			{ "Cards", "inventory quantity Trinket - Attack Up Defense Down : 1", "Tincture" },
			{ "Cards", "inventory quantity Trinket - Block Plus : 1",             "Bracer" },
			{ "Cards", "inventory quantity Trinket - Bloodstain MP : 1",          "Magic Echo" },
			{ "Cards", "inventory quantity Trinket - Fast Icedagger : 1",         "Dagger Strap" },
			{ "Cards", "inventory quantity Trinket - Stamina Recharge : 1",       "Perfume" },
			{ "Cards", "inventory quantity Trinket - Sneaky : 1",                 "Muffling Bell" },
			{ "Cards", "inventory quantity Trinket - Walk Speed Plus : 1",        "Anklet" },
			{ "Cards", "inventory quantity Trinket - Parry Window : 1",           "Aura's Gem" },
			{ "Cards", "inventory quantity Trinket - IFrames : 1",                "Bone" },

		{ null, "Fairies", "Fairies" },
			{ "Fairies", "SV_Fairy_1_Overworld_Flowers_Upper_Opened : 1", "1 - Flowers 1 (Upper)" },
			{ "Fairies", "SV_Fairy_2_Overworld_Flowers_Lower_Opened : 1", "2 - Flowers 2 (Lower)" },
			{ "Fairies", "SV_Fairy_3_Overworld_Moss_Opened : 1",          "3 - Moss" },
			{ "Fairies", "SV_Fairy_4_Caustics_Opened : 1",                "4 - Casting Light" },
			{ "Fairies", "SV_Fairy_5_Waterfall_Opened : 1",               "5 - Secret Gathering Place" },
			{ "Fairies", "SV_Fairy_6_Temple_Opened : 1",                  "6 - Sealed Temple" },
			{ "Fairies", "SV_Fairy_7_Quarry_Opened : 1",                  "7 - Quarry" },
			{ "Fairies", "SV_Fairy_8_Dancer_Opened : 1",                  "8 - East Forest (Dancer)" },
			{ "Fairies", "SV_Fairy_9_Library_Rug_Opened : 1",             "9 - The Great Library" },
			{ "Fairies", "SV_Fairy_10_3DPillar_Opened : 1",               "10 - Maze (Column)" },
			{ "Fairies", "SV_Fairy_11_WeatherVane_Opened : 1",            "11 - Vane" },
			{ "Fairies", "SV_Fairy_12_House_Opened : 1",                  "12 - House" },
			{ "Fairies", "SV_Fairy_13_Patrol_Opened : 1",                 "13 - Patrol" },
			{ "Fairies", "SV_Fairy_14_Cube_Opened : 1",                   "14 - Cube" },
			{ "Fairies", "SV_Fairy_15_Maze_Opened : 1",                   "15 - Maze (Invisible)" },
			{ "Fairies", "SV_Fairy_16_Fountain_Opened : 1",               "16 - Fountain" },
			{ "Fairies", "SV_Fairy_17_GardenTree_Opened : 1",             "17 - West Garden (Tree)" },
			{ "Fairies", "SV_Fairy_18_GardenCourtyard_Opened : 1",        "18 - West Garden (Courtyard)" },
			{ "Fairies", "SV_Fairy_19_FortressCandles_Opened : 1",        "19 - Fortress of the Eastern Vault" },
			{ "Fairies", "SV_Fairy_20_ForestMonolith_Opened : 1",         "20 - East Forest (Obelisk)" },
			{ "Fairies", "SV_Fairy_00_Enough Fairies Found : 1",          "Found 10 fairies" },
			{ "Fairies", "SV_Fairy_00_All Fairies Found : 1",             "Found 20 fairies" },

		{ null, "Golden Trophies", "Golden Trophies" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_1 : 1", "1 - Mr Mayor" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_2 : 1", "2 - A Secret Legend" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_3 : 1", "3 - Sacred Geometry" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_4 : 1", "4 - Vintage" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_5 : 1", "5 - Just Some Pals" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_6 : 1", "6 - Regal Weasel" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_7 : 1", "7 - Sprinng Falls" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_8 : 1", "8 - Power Up" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_9 : 1", "9 - Back to Work" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_10 : 1", "10 - Phonomath" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_11 : 1", "11 - Dusty" },
			{ "Golden Trophies", "inventory quantity GoldenTrophy_12 : 1", "12 - Forever Friend" },

		{ null, "Pages", "Pages" },
			{ "Pages", "unlocked page 0 : 1", "Page 0-1" },
			{ "Pages", "unlocked page 1 : 1", "Page 2-3" },
			{ "Pages", "unlocked page 2 : 1", "Page 4-5" },
			{ "Pages", "unlocked page 3 : 1", "Page 6-7" },
			{ "Pages", "unlocked page 4 : 1", "Page 8-9" },
			{ "Pages", "unlocked page 5 : 1", "Page 10-11" },
			{ "Pages", "unlocked page 6 : 1", "Page 12-13" },
			{ "Pages", "unlocked page 7 : 1", "Page 14-15" },
			{ "Pages", "unlocked page 8 : 1", "Page 16-17" },
			{ "Pages", "unlocked page 9 : 1", "Page 18-19" },
			{ "Pages", "unlocked page 10 : 1", "Page 20-21" },
			{ "Pages", "unlocked page 11 : 1", "Page 22-23" },
			{ "Pages", "unlocked page 12 : 1", "Page 24-25" },
			{ "Pages", "unlocked page 13 : 1", "Page 26-27" },
			{ "Pages", "unlocked page 14 : 1", "Page 28-29" },
			{ "Pages", "unlocked page 15 : 1", "Page 30-31" },
			{ "Pages", "unlocked page 16 : 1", "Page 32-33" },
			{ "Pages", "unlocked page 17 : 1", "Page 34-35" },
			{ "Pages", "unlocked page 18 : 1", "Page 36-37" },
			{ "Pages", "unlocked page 19 : 1", "Page 38-39" },
			{ "Pages", "unlocked page 20 : 1", "Page 40-41" },
			{ "Pages", "unlocked page 21 : 1", "Page 42-43" },
			{ "Pages", "unlocked page 22 : 1", "Page 44-45" },
			{ "Pages", "unlocked page 23 : 1", "Page 46-47" },
			{ "Pages", "unlocked page 24 : 1", "Page 48-49" },
			{ "Pages", "unlocked page 25 : 1", "Page 50-51" },
			{ "Pages", "unlocked page 26 : 1", "Page 52-53" },
			{ "Pages", "unlocked page 27 : 1", "Page 54-55" },

		{ null, "Areas", "Areas" },
			{ "Areas", "scEvery", "Split every single time the selected areas are visited" },
			{ "Areas", "scOnce", "Split only the first time the selected areas are visited"},

			{ "Areas", "scGeneral", "General Areas" },
				{ "scGeneral", "sc25", "Overworld" },
				{ "scGeneral", "sc39", "Teleport Area" },
				{ "scGeneral", "sc24", "Temple" },
				{ "scGeneral", "sc26", "Shield Area" },
				{ "scGeneral", "sc27", "Well" },
				{ "scGeneral", "sc64", "Dark Tomb" },

			{ "Areas", "scMountain", "Mountain" },
				{ "scMountain", "sc09", "Mountain" },
				{ "scMountain", "sc10", "Mountain Top" },

			{ "Areas", "scEast", "East" },
				{ "scEast", "sc53", "East Forest" },
				{ "scEast", "sc12", "East Forest Sword" },
				{ "scEast", "sc55", "East Forest Guardhouse" },
				{ "scEast", "sc11", "East Forest Boss Room" },
				{ "scEast", "sc36", "East Beltower" },

			{ "Areas", "scWest", "West" },
				{ "scWest", "sc31", "West Garden" },

			{ "Areas", "sc32", "Atoll" },

			{ "Areas", "scFrog", "Frog Cave" },
				{ "scFrog", "sc33", "Way to Frog Cave" },
				{ "scFrog", "sc52", "Frog Cave" },

			{ "Areas", "scFortress", "Fortress" },
				{ "scFortress", "sc13", "Fortress" },
				{ "scFortress", "sc15", "Fortress Courtyard" },
				{ "scFortress", "sc14", "Fortress Basement" },
				{ "scFortress", "sc16", "Siege Arena" },

			{ "Areas", "scLibrary", "Library" },
				{ "scLibrary", "sc34", "Library Exterior" },
				{ "scLibrary", "sc19", "Library Hall" },
				{ "scLibrary", "sc20", "Library Rotunda" },
				{ "scLibrary", "sc18", "Library Lab" },
				{ "scLibrary", "sc28", "Librarian Arena" },

			{ "Areas", "scQuarry", "Quarry" },
				{ "scQuarry", "sc23", "Way to Quarry" },
				{ "scQuarry", "sc60", "Quarry" },
				{ "scQuarry", "sc22", "Monastery" },

			{ "Areas", "scZiggurat", "Ziggurat" },
				{ "scZiggurat", "sc45", "Ziggurat Entrance" },
				{ "scZiggurat", "sc43", "Ziggurat" },
				{ "scZiggurat", "sc42", "Ziggurat Transition" },
				{ "scZiggurat", "sc44", "Ziggurat 2" },

			{ "Areas", "scSwamp", "Swamp" },
				{ "scSwamp", "sc59", "Swamp" },
				{ "scSwamp", "sc69", "Cathedral" },
				{ "scSwamp", "sc61", "Gauntlet Arena" },

			{ "Areas", "sc63", "Heir Arena" },
	};

	for (int i = 0; i < _settings.GetLength(0); i++)
	{
		var parent = _settings[i, 0];
		var id     = _settings[i, 1];
		var desc   = _settings[i, 2];

		settings.Add(id, parent == null, desc, parent);
	}

	if (timer.CurrentTimingMethod == TimingMethod.RealTime)
	{
		var mbox = MessageBox.Show(
			"TUNIC uses in-game time.\nWould you like to switch to it?",
			"LiveSplit | TUNIC",
			MessageBoxButtons.YesNo);

		if (mbox == DialogResult.Yes)
			timer.CurrentTimingMethod = TimingMethod.GameTime;
	}
}

init
{
	current.IGT = 0.0f;
	current.TimerRunning = false;
	current.GameComplete = false;
	current.Event = "";
	current.SceneIndex = -1;

	// In-game time when the run started, to support loading from an NG+ save
	// with a non-zero start time. The actual game time recorded by LiveSplit is
	// (in-game time - start time).
	vars.StartTime = 0.0f;

	// Whether we can start the run when the scene loads in and the in-game
	// timer starts. This is primed when we transition from the TitleScreen to
	// the Overworld scene.
	vars.CanStart = false;

	// Tracks the scenes and events which have been completed. Prevents
	// splitting multiple times on the same event. If the settings only allow
	// splitting once on entering a scene, prevents splitting multiple times on
	// the same scene.
	vars.CompletedSplits = new HashSet<string>();

	// Tracks the player's position for replay
	vars.replayFile = null;
	vars.replaySceneIndex = null;

	vars.Helper.TryLoad = (Func<dynamic, bool>)(mono =>
	{
		var srd = mono.GetClass("SpeedrunData");
		vars.Helper["inGameTime"] = srd.Make<float>("inGameTime");
		vars.Helper["timerRunning"] = srd.Make<bool>("timerRunning");
		vars.Helper["gameComplete"] = srd.Make<bool>("gameComplete");
		vars.Helper["lastEvent"] = srd.MakeString("LastEvent");

		// Do not use SpeedrunData.lastLoadedSceneIndex because it does not
		// update when transitioning to the main menu.  Instead, query the
		// SceneManager scene (vars.Helper.Scenes.Active) directly.
		// vars.Helper["lastLoadedSceneIndex"] = srd.Make<int>("lastLoadedSceneIndex");

		var pc = mono.GetClass("PlayerCharacter");
		var lastPositionOffset = pc["lastPosition"];
		vars.Helper["lastPositionX"] = pc.Make<float>("_instance", lastPositionOffset);
		vars.Helper["lastPositionY"] = pc.Make<float>("_instance", lastPositionOffset + 4);
		vars.Helper["lastPositionZ"] = pc.Make<float>("_instance", lastPositionOffset + 8);
		var lastRotationOffset = pc["lastRotation"];
		vars.Helper["lastRotationX"] = pc.Make<float>("_instance", lastRotationOffset);
		vars.Helper["lastRotationY"] = pc.Make<float>("_instance", lastRotationOffset + 4);
		vars.Helper["lastRotationZ"] = pc.Make<float>("_instance", lastRotationOffset + 8);
		vars.Helper["lastRotationW"] = pc.Make<float>("_instance", lastRotationOffset + 12);

        return true;
    });
}

update
{
	current.IGT = vars.Helper["inGameTime"].Current;
	current.TimerRunning = vars.Helper["timerRunning"].Current;
	current.GameComplete = vars.Helper["gameComplete"].Current;

	var lastEvent = vars.Helper["lastEvent"].Current ?? "";
	if (!lastEvent.StartsWith("playtime")
		&& !lastEvent.StartsWith("permanentlyDead")
		&& !lastEvent.StartsWith("GrassCount"))
	{
		current.Event = lastEvent;
	}
	else
	{
		current.Event = old.Event;
	}

	var scene = vars.Helper.Scenes.Active;
	// Ignore the Loading scene (81), which happens in between area transitions
	// or rests, so that we can make decisions about transitioning between two
	// "real" scenes.
	if (scene.IsValid && scene.Index > 0 && scene.Index != 81)
	{
		current.SceneIndex = scene.Index;
	}
	else
	{
		current.SceneIndex = old.SceneIndex;
	}

	if (vars.replayFile != null && scene.IsValid && scene.Index > 0)
	{
		// Write a new section header if we transitioned to a new scene
		if (vars.replaySceneIndex != scene.Index)
		{
			// But don't write a transition to the TitleScreen (3) or Loading
			// scene (8) since those scenes aren't actually part of the run
			if (scene.Index != 3 && scene.Index != 81)
			{
				vars.Log("[REPLAY] Wrote scene header " + scene.Index);

				vars.replayFile.Write(0xDEADBEEF);
				vars.replayFile.Write(scene.Index);
				vars.replayFile.Write(current.IGT);
			}

			vars.replaySceneIndex = scene.Index;
		}

		// Prevent logging when the timer is running, but the in-game time is
		// not updating game because we are in menu or the game is in the
		// background.
		if (current.TimerRunning && current.IGT != old.IGT)
		{
			vars.replayFile.Write(0xCAFED00D);
			vars.replayFile.Write(current.IGT);
			vars.replayFile.Write(vars.Helper["lastPositionX"].Current);
			vars.replayFile.Write(vars.Helper["lastPositionY"].Current);
			vars.replayFile.Write(vars.Helper["lastPositionZ"].Current);
			vars.replayFile.Write(vars.Helper["lastRotationX"].Current);
			vars.replayFile.Write(vars.Helper["lastRotationY"].Current);
			vars.replayFile.Write(vars.Helper["lastRotationZ"].Current);
			vars.replayFile.Write(vars.Helper["lastRotationW"].Current);
		}
	}
}

start
{
	// If we transition from TitleScreen (scene 3) to the Overworld (scene 25),
	// assume that we can start the run. This happens when the player clicks
	// "New Game", or loads a save file that starts on the Overworld. If the
	// player loads a save file, assume that the save file represents a clean
	// NG+ file.
	if (old.SceneIndex == 3 && current.SceneIndex == 25)
	{
		vars.CanStart = true;
	}

	// The timer only starts some time after the scene changes, so wait
	return vars.CanStart && !old.TimerRunning && current.TimerRunning;
}

onStart
{
	// onStart is called after the in-game timer has started. If the in-game
	// time is very close to 0, then assume that we started from 0. Some NG+
	// files have a non-zero start time, so if the in-game time is much larger,
	// then assume that we started from that time. For these files, the
	// LiveSplit time may be a few frames faster than the true in-game time.
	var igt = current.IGT;
	vars.StartTime = igt < 1.0f ? 0.0f : igt;
	vars.CanStart = false;
	vars.CompletedSplits.Clear();

	if (vars.replayFile != null)
	{
		vars.replayFile.Close();
		vars.replayFile = null;
	}

	var replaysPath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Desktop\TunicReplays\");
	Directory.CreateDirectory(replaysPath);
	
	var replayTs = DateTime.UtcNow.ToString("yyyy-MM-ddTHH-mm-ssZ", System.Globalization.CultureInfo.InvariantCulture);
	vars.replayFile = new BinaryWriter(File.Create(Path.Combine(replaysPath, "tunic-replay-" + replayTs + ".trp")));
	vars.replayFile.Write(0x00001000); // Version header
	vars.replaySceneIndex = -1;
}

split
{
	if (old.SceneIndex != current.SceneIndex)
	{
		vars.Log("Scene changed: " + old.SceneIndex + " -> " + current.SceneIndex);

		var cS = "sc" + current.SceneIndex;
		if ((settings["scOnce"] && !vars.CompletedSplits.Contains(cS)) || settings["scEvery"])
		{
			vars.CompletedSplits.Add(cS);

			if (settings.ContainsKey(cS) && settings[cS])
			{
				return true;
			}
		}
	}

	var cE = current.Event;
	if (old.Event != cE && !vars.CompletedSplits.Contains(cE))
	{
		vars.Log("Latest event: " + cE);
		
		vars.CompletedSplits.Add(cE);

		if (settings.ContainsKey(cE) && settings[cE])
		{
			return true;
		}
	}

	return !old.GameComplete && current.GameComplete;
}

onSplit
{
	if (vars.replayFile != null)
	{
		vars.replayFile.Flush();

		if (current.GameComplete)
		{
			vars.replayFile.Close();
			vars.replayFile = null;
		}
	}
}

reset
{
	// TODO: There's no good way to auto reset based on a New Game or loading an
	// NG+ game yet. The first frame that LiveSplit processes may not have
	// exactly a game time of 0.0s (New Game) or 1.0h (NG+).
}

onReset
{
	if (vars.replayFile != null)
	{
		vars.replayFile.Close();
		vars.replayFile = null;
	}
}

gameTime
{
	return TimeSpan.FromSeconds(current.IGT - vars.StartTime);
}

isLoading
{
	// Lie to LiveSplit that we are always loading, which prevents LiveSplit
	// from automatically advancing the game time. Otherwise the LiveSplit game
	// time will advance then jump back on update.
	return true;
}
