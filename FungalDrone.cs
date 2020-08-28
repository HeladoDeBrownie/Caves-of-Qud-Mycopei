namespace XRL.World.Parts
{
    using XRL.World.Effects;

    public class helado_Mycopei_FungalDrone : IPart
    {
        public override bool WantEvent(int id, int cascade)
        {
            return
                id == EndTurnEvent.ID ||
                base.WantEvent(id, cascade);
        }

        public override bool HandleEvent(EndTurnEvent @event)
        {
            var brain = ParentObject.pBrain;

            if (brain != null)
            {
                if (!ParentObject.HasEffect(typeof(helado_Mycopei_FungalDroneDormant)))
                {
                    ParentObject.ApplyEffect(new helado_Mycopei_FungalDroneDormant());
                }
            }

            return true;
        }
    }
}

namespace XRL.World.Effects
{
    public class helado_Mycopei_FungalDroneDormant : Effect
    {
        public helado_Mycopei_FungalDroneDormant()
        {
            Duration = DURATION_INDEFINITE;
        }

        public override void Register(GameObject go)
        {
            go.RegisterEffectEvent(this, "BeforeAITakingAction");
            base.Register(go);
        }

        public override bool FireEvent(Event @event)
        {
            switch (@event.ID)
            {
                case "BeforeAITakingAction":
                    return false;

                default:
                    return base.FireEvent(@event);
            }
        }

        public override bool Apply(GameObject go)
        {
            if (ThePlayer.HasLOSTo(Object: go))
            {
                XDidY(
                    what: go,
                    verb: "fall",
                    extra: "dormant",
                    terminalPunctuation: "!"
                );
            }

            return true;
        }

        public override void Remove(GameObject go)
        {
            if (ThePlayer.HasLOSTo(Object: go))
            {
                XDidY(
                    what: go,
                    verb: "reactivate",
                    terminalPunctuation: "!"
                );
            }
        }
    }
}
