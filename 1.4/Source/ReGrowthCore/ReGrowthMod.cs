using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;

namespace ReGrowthCore
{
    public class ReGrowthMod : Mod
    {
        public static ReGrowthSettings settings;
        public ReGrowthMod(ModContentPack pack) : base(pack)
        {
            Harmony harmony = new("Helixien.ReGrowthCore");
            harmony.PatchAll();
            settings = GetSettings<ReGrowthSettings>();
            List<MethodInfo> hooks = new()
            {
                AccessTools.Method(typeof(Game), "InitNewGame"),
                AccessTools.Method(typeof(Game), "LoadGame"),
                AccessTools.Method(typeof(SavedGameLoaderNow), "LoadGameFromSaveFileNow")
            };

            foreach (MethodInfo hook in hooks)
            {
                harmony.Patch(hook, new HarmonyMethod(typeof(ReGrowthUtils), nameof(ReGrowthUtils.ResetStaticData)));
            }
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            Listing_Standard list = new();
            list.Begin(inRect);
            Text.Font = GameFont.Small;
            list.CheckboxLabeled("Enable all leaves spawners", ref settings.enableLeaveSpawners);
            list.Gap(5);
            list.CheckboxLabeled("Enable all autumn leaves spawners", ref settings.enableAutumnLeaveSpawners);
            list.End();
        }

        public override string SettingsCategory()
        {
            return Content.Name;
        }
    }
}
