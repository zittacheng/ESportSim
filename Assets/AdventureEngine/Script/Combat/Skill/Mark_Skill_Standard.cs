using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Skill_Standard : Mark_Skill {
        public List<Signal> ReturnSignals;
        public List<Condition> MainConditions;
        public List<GameObject> StartOfTurnSignals;
        public List<GameObject> EndOfTurnSignals;
        public List<GameObject> StartOfCombatSignals;
        public List<GameObject> EndOfCombatSignals;
        public List<GameObject> UpgradeSignals;

        public override void Use(Card Target)
        {
            List<string> LS = new List<string>();
            for (int i = 0; i < MainSignals.Count; i++)
                SendSignal(MainSignals[i], LS);
            base.Use(null);
            if (GetKey("Upgrade") > 0)
                ChangeKey("Upgrade", -1);
            if (HasKey("Upgrade") && GetKey("Upgrade") <= 0)
            {
                for (int i = 0; i < UpgradeSignals.Count; i++)
                    SendSignal(UpgradeSignals[i]);
            }
        }

        public void StartOfTurn()
        {
            for (int i = 0; i < StartOfTurnSignals.Count; i++)
                SendSignal(StartOfTurnSignals[i]);
            //base.StartOfTurn();
        }

        public void EndOfTurn()
        {
            for (int i = 0; i < EndOfTurnSignals.Count; i++)
                SendSignal(EndOfTurnSignals[i]);
            //base.EndOfTurn();
        }

        public override void StartOfCombat()
        {
            for (int i = 0; i < StartOfCombatSignals.Count; i++)
                SendSignal(StartOfCombatSignals[i]);
            base.StartOfCombat();
        }

        public override void EndOfCombat()
        {
            for (int i = 0; i < EndOfCombatSignals.Count; i++)
                SendSignal(EndOfCombatSignals[i]);
            base.EndOfCombat();
        }

        public override bool CanUse()
        {
            return base.CanUse() && PassMainConditions();
        }

        public bool PassMainConditions()
        {
            foreach (Condition C in MainConditions)
                if (!C.Pass(GKB()))
                    return false;
            return true;
        }

        public override void CommonKeys()
        {
            // "Upgrade": Remaining usage till upgrade
            base.CommonKeys();
        }
    }
}