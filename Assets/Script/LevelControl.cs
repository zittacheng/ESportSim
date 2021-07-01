using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class LevelControl : MonoBehaviour {
        public static LevelControl Main;
        public Event TempVictoryEvent;
        public Event TempDefeatEvent;
        public Event RankVictoryEvent;
        public Event RankDefeatEvent;
        public List<Event> Levels;
        public Event CurrentLevel;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ResultProcess()
        {
            if (KeyBase.Main.HasKey("LastLevelIndex"))
                CurrentLevel = Levels[(int)KeyBase.Main.GetKey("LastLevelIndex")];
            if (KeyBase.Main.GetKey("RankGameActive") == 0)
            {
                if (KeyBase.Main.GetKey("LastResult") == 1)
                {
                    ThreadControl.Main.ForceEvent(TempVictoryEvent);
                    ThreadControl.Main.StartProcess();
                }
                else if (KeyBase.Main.GetKey("LastResult") == -1)
                {
                    ThreadControl.Main.ForceEvent(TempDefeatEvent);
                    ThreadControl.Main.StartProcess();
                }
            }
            else
            {
                if (KeyBase.Main.GetKey("LastResult") == 1)
                {
                    ThreadControl.Main.ForceEvent(RankVictoryEvent);
                    ThreadControl.Main.StartProcess();
                }
                else if (KeyBase.Main.GetKey("LastResult") == -1)
                {
                    ThreadControl.Main.ForceEvent(RankDefeatEvent);
                    ThreadControl.Main.StartProcess();
                }
            }
        }

        public void NextLevel()
        {
            int Index = Levels.IndexOf(CurrentLevel) + 1;
            if (Index < 0 || Index >= Levels.Count)
                return;
            CurrentLevel = Levels[Index];
        }
    }
}