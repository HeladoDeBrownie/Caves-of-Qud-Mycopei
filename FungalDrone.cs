namespace XRL.World.Parts
{
    using XRL.World.Effects;

    public class helado_Mycopei_FungalDrone : IPart
    {
        public bool ReceivedFungalInfluenceRecently = false;

        public override bool WantEvent(int id, int cascade)
        {
            return
                id == BeginTakeActionEvent.ID ||
                id == helado_Mycopei_FungalInfluenceEvent.ID ||
                base.WantEvent(id, cascade);
        }

        public override bool HandleEvent(BeginTakeActionEvent @event)
        {
            var brain = ParentObject.pBrain;

            if (brain != null)
            {
                if (!ReceivedFungalInfluenceRecently && !ParentObject.HasEffect(typeof(helado_Mycopei_FungalDroneDormant)))
                {
                    ParentObject.ApplyEffect(new helado_Mycopei_FungalDroneDormant());
                }
            }

            ReceivedFungalInfluenceRecently = false;
            return true;
        }

        public bool HandleEvent(helado_Mycopei_FungalInfluenceEvent @event)
        {
            ReceivedFungalInfluenceRecently = true;
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
            DisplayName = "{{K|dormant}}";
            Duration = DURATION_INDEFINITE;
        }

        public override bool WantEvent(int id, int cascade)
        {
            return
                id == helado_Mycopei_FungalInfluenceEvent.ID ||
                base.WantEvent(id, cascade);
        }

        public bool HandleEvent(helado_Mycopei_FungalInfluenceEvent @event)
        {
            Object.RemoveEffect(this);
            return true;
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

        public override void Unregister(GameObject go)
        {
            go.UnregisterEffectEvent(this, "BeforeAITakingAction");
            base.Unregister(go);
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
