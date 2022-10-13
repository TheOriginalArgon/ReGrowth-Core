using System.Collections.Generic;
using Verse;
using VFECore;

namespace ReGrowthCore
{
    public class CompProperties_TreeAmbientSound : CompProperties_AmbientSound
    {
        public float minWindSpeed;
        public CompProperties_TreeAmbientSound()
        {
            compClass = typeof(CompTreeAmbientSound);
        }
    }
    public class CompTreeAmbientSound : CompAmbientSound
    {
        public static HashSet<CompTreeAmbientSound> comps = new();
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            _ = comps.Add(this);
        }

        public override void PostDeSpawn(Map map)
        {
            _ = comps.Remove(this);
            base.PostDeSpawn(map);
        }

        public new CompProperties_TreeAmbientSound Props => base.props as CompProperties_TreeAmbientSound;
        public void CompTreeAmbientSoundTick()
        {
            if (CanStartSustainer())
            {
                StartSustainer();
            }
            else
            {
                EndSustainer();
            }
        }

        protected override bool CanStartSustainer()
        {
            return parent.Map.windManager.WindSpeed >= Props.minWindSpeed;
        }
    }
}
