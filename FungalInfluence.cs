namespace XRL.World.Parts
{
    public class helado_Mycopei_FungalInfluence : IPart
    {
        public override bool WantEvent(int id, int _)
        {
            return
                id == EndTurnEvent.ID ||
                base.WantEvent(id, _);
        }

        public override bool HandleEvent(EndTurnEvent @event)
        {
            if (ParentObject.InSameZone(ThePlayer))
            {
                AddPlayerMessage("DEBUG: fungal influence");
            }

            return true;
        }
    }
}

namespace XRL.World.ZoneBuilders
{
    public class helado_Mycopei_FungalInfluence
    {
        public bool BuildZone(Zone zone)
        {
            zone.GetCell(x: 0, y: 0).AddObject(Blueprint:
                "helado_Mycopei_Fungal Influence Widget"
            );

            return true;
        }
    }
}
