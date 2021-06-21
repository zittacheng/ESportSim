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
            if (KeyBase.Main.GetKey("LastResult") == 1)
            {
                ThreadControl.Main.AddEvent(TempVictoryEvent);
                ThreadControl.Main.StartProcess();
            }
            else if (KeyBase.Main.GetKey("LastResult") == -1)
            {
                ThreadControl.Main.AddEvent(TempDefeatEvent);
                ThreadControl.Main.StartProcess();
            }
        }
    }
}