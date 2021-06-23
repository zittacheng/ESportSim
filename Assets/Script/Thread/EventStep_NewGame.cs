using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class EventStep_NewGame : EventStep {
        public int EnemyLevel;
        public bool RankScaling;
        public bool RankGame;
        public List<GameObject> Groups;

        public override void OnEffect()
        {
            KeyBase.Main.SetKey("LastLevel", LevelControl.Main.Levels.IndexOf(LevelControl.Main.CurrentLevel));
            KeyBase.Main.SetKey("EnemyLevel", EnemyLevel);
            if (RankGame)
                KeyBase.Main.SetKey("RankGameActive", 1);
            else
                KeyBase.Main.SetKey("RankGameActive", 0);
            InfoBase.Main.Groups = Groups;
            UnityEngine.SceneManagement.SceneManager.LoadScene("CombatScene");
        }
    }
}