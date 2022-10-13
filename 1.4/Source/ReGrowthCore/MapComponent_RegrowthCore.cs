using Verse;

namespace ReGrowthCore
{
    public class MapComponent_RegrowthCore : MapComponent
    {
        public MapComponent_RegrowthCore(Map map) : base(map)
        {
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();
            foreach (CompTreeAmbientSound comp in CompTreeAmbientSound.comps)
            {
                if (comp.parent.Map == map)
                {
                    comp.CompTreeAmbientSoundTick();
                }
            }
        }
    }
}
