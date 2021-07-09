using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Skill : Mark {
        public List<GameObject> MainSignals;
        public List<GameObject> Targetings;
        public List<GameObject> Conditions;
        [HideInInspector] public List<Targeting> Tars;
        [HideInInspector] public List<Condition> Cons;

        public virtual bool TryUse()
        {
            if (!CanUse())
                return false;
            if (!AddTryCondition())
                return false;
            if (Source.GetGCD() > 0 && GetKey("IgnoreGCD") <= 0)
                return false;
            if (Source.GetCast() && GetKey("IgnoreCast") <= 0)
                return false;
            if (Tars.Count <= 0)
            {
                foreach (GameObject G in Targetings)
                {
                    Targeting Tar = G.GetComponent<Targeting>();
                    Tars.Add(Tar);
                }
            }
            if (GetKey("Positional") > 0)
            {
                Vector2 P = new Vector2(Mathf.Infinity, Mathf.Infinity);
                foreach (Targeting Tar in Tars)
                {
                    P = Tar.FindPosition(Source);
                    if (P.x != Mathf.Infinity && P.y != Mathf.Infinity)
                        break;
                }
                if (P.x == Mathf.Infinity || P.y == Mathf.Infinity)
                    return false;
                if (GetKey("CastTime") <= 0)
                    Source.UseSkill(this, P);
                else
                    Source.CastSkill(this, P);
            }
            else
            {
                Card T = null;
                foreach (Targeting Tar in Tars)
                {
                    T = Tar.FindTarget(Source);
                    if (T)
                        break;
                }
                if (!T)
                    return false;
                if (GetKey("CastTime") <= 0)
                    Source.UseSkill(this, T);
                else
                    Source.CastSkill(this, T);
            }
            return true;
        }

        public virtual bool TryUse(Card Target, float CastTime)
        {
            if (!CanUse())
                return false;
            if (!Target || (!Target.CardActive() && GetKey("IgnoreDeath") == 0))
                return false;
            if (!AddTryCondition())
                return false;
            if (GetKey("CastTime") <= 0)
                return false;
            else if (GetKey("CastTime") <= CastTime)
            {
                Source.UseSkill(this, Target);
                return true;
            }
            else
                return false;
        }

        public virtual bool TryUse(Vector2 Position, float CastTime)
        {
            if (!CanUse())
                return false;
            if (!AddTryCondition())
                return false;
            if (GetKey("CastTime") <= 0)
                return false;
            else if (GetKey("CastTime") <= CastTime)
            {
                Source.UseSkill(this, Position);
                return true;
            }
            else
                return false;
        }

        public virtual bool AddTryCondition()
        {
            return !HasKey("EnergyCost") || Source.PassValue("Energy") < Source.PassValue("MaxEnergy");
        }

        public virtual void Use(Card Target)
        {
            List<string> AddKeys = new List<string>();
            if (Target)
            {
                AddKeys.Add(KeyBase.Compose("TargetPositionX", Target.GetPosition().x));
                AddKeys.Add(KeyBase.Compose("TargetPositionY", Target.GetPosition().y));
                if (HasKey("Count"))
                    AddKeys.Add(KeyBase.Compose("ItemCount", GetKey("Count")));
            }
            foreach (GameObject G in MainSignals)
                Source.SendSignal(G, AddKeys, Target);
            OnUse();
        }

        public virtual void Use(Vector2 Position)
        {
            List<string> AddKeys = new List<string>();
            AddKeys.Add(KeyBase.Compose("TargetPositionX", Position.x));
            AddKeys.Add(KeyBase.Compose("TargetPositionY", Position.y));
            if (HasKey("Count"))
                AddKeys.Add(KeyBase.Compose("ItemCount", GetKey("Count")));
            foreach (GameObject G in MainSignals)
                Source.SendSignal(G, AddKeys, null);
            OnUse();
        }

        public virtual void OnUse()
        {
            if (HasKey("CoolDown") && GetKey("CDMR") == 0)
            {
                if (HasKey("RCD"))
                    SetKey("CCD", GetKey("CoolDown") + Random.Range(-GetKey("RCD"), GetKey("RCD")));
                else
                    SetKey("CCD", GetKey("CoolDown"));
            }
            if (HasKey("Count") && GetKey("Consumed") == 1)
            {
                ChangeKey("Count", -1);
                if (Source.GetGroup())
                    CombatControl.Main.ConsumeItem(gameObject, Source.GetGroup(), Source, 1);
                Source.UpdateRenderSkills();
                /*if (GetKey("Count") <= 0)
                    Source.RemoveSkill(this);*/
            }
            if (HasKey("GCD"))
                Source.SetGCD(GetKey("GCD"));
        }

        public override void StartOfCombat()
        {
            if (HasKey("CoolDown"))
                SetKey("CCD", 0);
            base.StartOfCombat();
        }

        public override void EndOfCombat()
        {
            if (HasKey("CoolDown"))
                SetKey("CCD", 0);
            base.EndOfCombat();
        }

        public override void Death()
        {
            if (HasKey("CoolDown"))
                SetKey("CCD", 0);
            base.Death();
        }

        public virtual bool CanUse()
        {
            return (!HasKey("CoolDown") || GetKey("CCD") <= 0) && (!HasKey("Count") || GetKey("Count") > 0 || GetKey("Consumed") == 0) && GetKey("Passive") == 0
                && Source && Source.CardActive() && ConditionPass() && (!HasKey("ManaCost") || Source.PassValue("Mana") >= GetKey("ManaCost"))
                && (GetKey("IgnoreStun") == 1 || Source.PassValue("Stunned", 0) == 0);
        }

        public virtual bool CanUse(Card Target)
        {
            if (!CanUse())
                return false;
            if (!Target || !Target.CardActive())
                return false;
            return true;
        }

        public virtual bool CanUse(Vector2 Position)
        {
            if (!CanUse())
                return false;
            if (Position.x == Mathf.Infinity || Position.y == Mathf.Infinity)
                return false;
            return true;
        }

        public bool ConditionPass()
        {
            if (Cons.Count <= 0)
            {
                foreach (GameObject G in Conditions)
                {
                    Condition C = G.GetComponent<Condition>();
                    Cons.Add(C);
                }
            }
            foreach (Condition C in Cons)
            {
                if (!C.Pass(Source))
                    return false;
            }
            return true;
        }

        public bool CanItemStack(Mark_Skill S)
        {
            return HasKey("Count") && S.HasKey("Count");
        }

        public void ItemStack(Mark_Skill S)
        {
            if (!CanItemStack(S))
                return;
            ChangeKey("Count", S.GetKey("Count"));
        }

        public override void TimePassed(float Value)
        {
            if (HasKey("CoolDown"))
            {
                if (GetKey("ASCD") != 0)
                    ChangeKey("CCD", -Value * Source.GetAttackSpeedScale());
                else
                    ChangeKey("CCD", -Value);
            }
            base.TimePassed(Value);
        }

        public override float PassValue(string Key, float Value)
        {
            if (GetKey("BasicSkill") == 1 && Key == "AttackSpeed" && GetKey("CoolDown") > 0)
                return Value * (1 / GetKey("CoolDown"));
            return base.PassValue(Key, Value);
        }

        public override bool Active()
        {
            return CanUse();
        }

        public override void CommonKeys()
        {
            // "CoolDown": Original cool down
            // "CCD": Current cool down
            // "RCD": Random cool down range
            // "CDMR": Whether cool down should be resetted by external signal
            // "ASCD": Whether cool down should be affected by attack speed
            // "Count": Current count
            // "Consumed": Whether the item count is consumed on use
            // "Cost": Coin cost of the item
            // "IgnoreStack": Whether the item should ignore stacking
            // "Passive": Whether the skill is passive
            // "Permanent": Whether the skill cannot be replaced
            // "Hidden": Whether not to display when inactive
            // "HiddenII": Whether not to put into RenderSkills list when inactive
            // "Auto": Whether the skill will automatically be used
            // "AutoPriority": The priority for use
            // "Stunned": Whether the source is stunned
            // "IgnoreStun": Whether the skill can be used while stunned
            // "Positional": Whether the skill use positional targeting
            // "CastTime": Max cast time
            // "HoldCast": Whether the skill should not be used instantly after cast
            // "IgnoreCast": Whether the skill can be used while casting
            // "Selected": Whether the skill should be selected instead of used
            // "GCD": GCD lock duration
            // "IgnoreGCD": Whether the skill can be used when GCD is not ready
            // "ManaCost": Mana cost (0 ~ 1)
            // "StartOfCombat": Whether the skill should be used at the start of combat
            // "EndOfCombat": Whether the skill should be used at the end of combat
            // "IgnoreDeath": Whether the skill can be used on dead target
            // "BasicSkill": Whether the skill is the auto attack skill
            base.CommonKeys();
        }
    }

    public enum SkillType
    {
        Skill,
        Item,
        System
    }
}