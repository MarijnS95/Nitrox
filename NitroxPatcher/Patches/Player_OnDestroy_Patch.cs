using System;
using System.Reflection;
using Harmony;
using NitroxClient.Communication.Abstract;
using NitroxModel.Core;

namespace NitroxPatcher.Patches
{
    public class Player_OnDestroy_Patch : NitroxPatch
    {
        public static readonly Type TARGET_CLASS = typeof(Player);
        public static readonly MethodInfo TARGET_METHOD = TARGET_CLASS.GetMethod("OnDestroy", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void Prefix()
        {
            IMultiplayerSession multiplayerSession = NitroxServiceLocator.LocateService<IMultiplayerSession>();
            multiplayerSession.Disconnect();
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPrefix(harmony, TARGET_METHOD);
        }
    }
}
