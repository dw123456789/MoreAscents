using BepInEx;
using BepInEx.Logging;
using System.Reflection;
using BepInEx.Configuration;
using UnityEngine;
using HarmonyLib;
using MoreAscents.Patches;
using Zorro.Core;
using Logger = UnityEngine.Logger;

namespace MoreAscents
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        internal static Plugin Instance;

        // fix an issue that causes people to break when the mod is uninstalled after beating an ascent higher than 7
        internal static ConfigEntry<int> ascentsUnlocked;
        internal static ConfigEntry<bool> ascent13Disabler;
        
        private void Awake() {
            Logger = base.Logger;
            Instance = this;

            ascentsUnlocked = Config.Bind("General",      // The section under which the option is shown
                "MaxAscent",  // The key of the configuration option in the configuration file
                0, // The default value
                ""); // Description of the option to show in the config file
            ascent13Disabler = Config.Bind("General", "Ascent 13 Disabler", false, "Disables ascent 13.");

            AscentGimmickHandler.GetBaseAscentCount();
            
            // custom ones
            AscentGimmickHandler.RegisterAscent<FallDamageGimmick>();
            AscentGimmickHandler.RegisterAscent<AfflictionGimmick>();
            AscentGimmickHandler.RegisterAscent<LuggageGimmick>();
            AscentGimmickHandler.RegisterAscent<HelpingIsBadGimmick>();
            AscentGimmickHandler.RegisterAscent<OccultStatueGimmick>();
            
            // chaos ones
            AscentGimmickHandler.RegisterAscent<SkeletonGimmick>();
            AscentGimmickHandler.RegisterAscent<CampfireGimmick>();
            AscentGimmickHandler.RegisterAscent<SunHotGimmick>();
            AscentGimmickHandler.RegisterAscent<BingBongGimmick>();
            
            AscentGimmickHandler.Initialize();
            
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            
            Harmony harmony = new(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }

        private static bool Initted = false;
        internal static void Init() {
            if (Initted) {
                return;
            }
            Initted = true;
            if (Singleton<AchievementManager>.Instance.GetSteamStatInt(STEAMSTATTYPE.MaxAscent, out int maxAscents)) {
                if (maxAscents > AscentGimmickHandler.BaseAscents) {
                    Singleton<AchievementManager>.Instance.SetSteamStat(STEAMSTATTYPE.MaxAscent,AscentGimmickHandler.BaseAscents);
                }
                ascentsUnlocked.Value = ascentsUnlocked.Value < maxAscents ? maxAscents : ascentsUnlocked.Value;
            }
            
            Logger.LogInfo($"Initted");
        }

        private void Update() {
            GUIManagerPatches.Grasp.SinceLastGrab += Time.deltaTime;
            
            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active) {
                    continue;
                }
                gimmick.Update();
            }
        }
    }

    [HarmonyPatch(typeof(BoardingPass), "UpdateAscent")]
    internal static class boarding_initilize_patch
    {
        internal static void Prefix(BoardingPass __instance)
        {
            if (Input.GetKey(KeyCode.L)) {
                Singleton<AchievementManager>.Instance.SetSteamStat(STEAMSTATTYPE.MaxAscent, 155);
            }
            
            FieldInfo info = __instance.GetType().GetField("maxAscent",BindingFlags.Instance | BindingFlags.NonPublic);
            info.SetValue(__instance, AscentData.Instance.ascents.Count - 3);
            Plugin.Logger.LogInfo($"Ascents capped {info.GetValue(__instance)}");
        }
    }
}
