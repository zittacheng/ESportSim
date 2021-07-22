using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Esuna : Mark_Status {
        public List<string> RequiredKeys;
        public List<string> InheritKeys;
        public List<GameObject> MainSignals;
        public List<GameObject> BreakSignals;

        public override void TimePassed(float Value)
        {
            for (int i = Source.Status.Count - 1; i >= 0; i--)
            {
                if (i < Source.Status.Count && Source.Status[i] && PassStatus(Source.Status[i]))
                {
                    OnTrigger(Source, new List<string>());
                    Source.RemoveStatus(this);
                }
            }
            base.TimePassed(Value);
        }

        public virtual void OnTrigger(Card Target, List<string> Keys)
        {
            List<string> AddKeys = new List<string>();
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                {
                    if ((s == "Damage" || s == "Heal") && GetKey("BaseScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, GetKey(s) * Source.GetBaseDamage()));
                    else
                        AddKeys.Add(KeyBase.Compose(s, GetKey(s)));
                }
            }
            if (Target)
            {
                AddKeys.Add(KeyBase.Compose("TargetPositionX", Target.GetPosition().x));
                AddKeys.Add(KeyBase.Compose("TargetPositionY", Target.GetPosition().y));
            }
            if (HasKey("ItemCount"))
                AddKeys.Add(KeyBase.Compose("ItemCount", GetKey("ItemCount")));
            for (int i = 0; i < Keys.Count; i++)
                AddKeys.Add(Keys[i]);
            foreach (GameObject G in MainSignals)
                Source.SendSignal(G, AddKeys, Target);
        }

        public override void CombatRemove()
        {
            OnBreak(Source, new List<string>());
            base.CombatRemove();
        }

        public void OnBreak(Card Target, List<string> Keys)
        {
            List<string> AddKeys = new List<string>();
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                {
                    if ((s == "Damage" || s == "Heal") && GetKey("BaseScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, GetKey(s) * Source.GetBaseDamage()));
                    else
                        AddKeys.Add(KeyBase.Compose(s, GetKey(s)));
                }
            }
            if (Target)
            {
                AddKeys.Add(KeyBase.Compose("TargetPositionX", Target.GetPosition().x));
                AddKeys.Add(KeyBase.Compose("TargetPositionY", Target.GetPosition().y));
            }
            if (HasKey("ItemCount"))
                AddKeys.Add(KeyBase.Compose("ItemCount", GetKey("ItemCount")));
            for (int i = 0; i < Keys.Count; i++)
                AddKeys.Add(Keys[i]);
            foreach (GameObject G in BreakSignals)
                Source.SendSignal(G, AddKeys, Target);
        }

        public bool PassStatus(Mark M)
        {
            bool T = true;
            foreach (string s in RequiredKeys)
                if (!M.HasKey(s) || M.GetKey(s) == 0)
                    T = false;
            return T;
        }
    }
}