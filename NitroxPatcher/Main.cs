﻿using System;
using System.Collections.Generic;
using Harmony;
using System.Reflection;
using UnityEngine;
using NitroxPatcher.Patches;
using NitroxClient.MonoBehaviours;

namespace NitroxPatcher
{
    class Main
    {
        private static readonly List<NitroxPatch> patches = new List<NitroxPatch>()
        {
            new ArmsController_Update_Patch(),
            new ArmsController_Start_Patch(),
            new BuilderPatch(),
            new ClipMapManager_HideEntities_Patch(),
            new ClipMapManager_ShowEntities_Patch(),
            new Constructable_Construct_Patch(),
            new Pickupable_Pickup_Patch(),
            new Pickupable_Drop_Patch(),
            new SpawnConsoleCommand_Patch(),
            new ConstructorInput_Craft_Patch(),
            new ConstructorInput_OnCraftingBegin_Patch(),
            new Constructable_Construct_Patch(),
            new BaseGhost_Finish_Patch()
        };

        public static void Execute()
        {
            Console.WriteLine("Patching subnautica for nitrox");
            // Enabling this creates a log file on your desktop (why there?), showing the emitted IL instructions.
            HarmonyInstance.DEBUG = false;

            UnityEngine.Object.FindObjectOfType<SystemsSpawner>().gameObject.AddComponent<DebugManager>();
            // TODO: DebugManager component is awakened in a later update cycle, so log messages below are not captured by it.
            HarmonyInstance harmony = HarmonyInstance.Create("com.nitroxmod.harmony");
            foreach (NitroxPatch patch in patches)
            {
                Debug.Log("[NITROX] Applying " + patch.GetType());
                patch.Patch(harmony);
            }

            Console.WriteLine("Completed patching for nitrox using " + Assembly.GetExecutingAssembly().FullName);
        }
    }
}
