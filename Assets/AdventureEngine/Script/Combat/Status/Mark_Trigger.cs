using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger : Mark_Status {
        public List<string> RequiredKeys;
        public List<string> AvoidedKeys;
        public List<GameObject> SourceConditions;
        public List<GameObject> TargetConditions;
        public List<GameObject> MainSignals;

        public override void TimePassed(float Value)
        {
            if (HasKey("CoolDown"))
                ChangeKey("CCD", -Value);
            base.TimePassed(Value);
        }

        public virtual void TryTrigger(Card Target, float Chance, List<string> Keys)
        {
            float a = Random.Range(0.001f, 0.999f);
            if (a <= Chance)
                OnTrigger(Target, Keys);
        }

        public virtual void OnTrigger(Card Target, List<string> Keys)
        {
            List<string> AddKeys = new List<string>();
            if (Target)
            {
                AddKeys.Add(KeyBase.Compose("TargetPositionX", Target.GetPosition().x));
                AddKeys.Add(KeyBase.Compose("TargetPositionY", Target.GetPosition().y));
            }
            if (HasKey("ItemCount"))
                AddKeys.Add(KeyBase.Compose("ItemCount", GetKey("ItemCount")));
            foreach (GameObject G in MainSignals)
                Source.SendSignal(G, AddKeys, Target);

            if (HasKey("TriggerCount"))
            {
                ChangeKey("TriggerCount", -1);
                if (GetKey("TriggerCount") <= 0)
                {
                    CombatRemove();
                    Source.RemoveStatus(this);
                }
            }

            if (HasKey("CoolDown"))
                SetKey("CCD", GetKey("CoolDown"));
        }

        public override void EndOfCombat()
        {
            if (HasKey("MaxTriggerCount"))
                SetKey("TriggerCount", GetKey("MaxTriggerCount"));
            base.EndOfCombat();
        }

        public virtual bool CanTrigger()
        {
            if (HasKey("TriggerCount") && GetKey("TriggerCount") <= 0)
                return false;
            if (HasKey("CoolDown") && GetKey("CCD") > 0)
                return false;
            return true;
        }

        public virtual bool Pass(Signal S)
        {
            bool T = true;
            foreach (string s in RequiredKeys)
                if (!S.HasKey(s) || S.GetKey(s) == 0)
                    T = false;
            foreach (string s in AvoidedKeys)
                if (S.HasKey(s) || S.GetKey(s) > 0)
                    T = false;
            foreach (GameObject G in SourceConditions)
                if (!G.GetComponent<Condition>().Pass(Source))
                    T = false;
            foreach (GameObject G in TargetConditions)
                if (!G.GetComponent<Condition>().Pass(S.Target))
                    T = false;
            return T;
        }

        public override void CommonKeys()
        {
            // "CoolDown": Original cool down
            // "CCD": Current cool down
            // "TriggerCount": Remaining trigger count
            // "MaxTriggerCount": Original trigger count
            base.CommonKeys();
        }
    }
}