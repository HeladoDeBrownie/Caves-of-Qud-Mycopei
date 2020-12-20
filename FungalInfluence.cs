namespace XRL.World.Parts
{
    public class helado_Mycopei_FungalInfluence : IPart
    {
        public static MinEvent FungalInfluenceEvent = new helado_Mycopei_FungalInfluenceEvent();

        public override bool WantEvent(int id, int _)
        {
            return
                id == EndTurnEvent.ID ||
                base.WantEvent(id, _);
        }

        public override bool HandleEvent(EndTurnEvent @event)
        {
            foreach (var go in ParentObject.CurrentZone.GetObjects())
            {
                go.HandleEvent(FungalInfluenceEvent);
            }

            return true;
        }
    }
}

namespace XRL.World
{
    public class helado_Mycopei_FungalInfluenceEvent : MinEvent
    {
        public static new readonly int ID = MinEvent.AllocateID();

        public helado_Mycopei_FungalInfluenceEvent()
        {
            base.ID = ID;
        }

        public override bool WantInvokeDispatch()
        {
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
