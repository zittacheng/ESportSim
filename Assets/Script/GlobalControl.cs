using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class GlobalControl : MonoBehaviour {
        public static GlobalControl Main;

        // Start is called before the first frame update
        public void Start()
        {
            IniProcess();
        }

        public void IniProcess()
        {
            if (KeyBase.Main.GetKey("LastResult") == 0)
                ThreadControl.Main.ProcessAdvance();
            else
                LevelControl.Main.ResultProcess();
        }

        // Update is called once per frame
        public void Update()
        {
            
        }

        public void NewDay()
        {
            KeyBase.Main.SetKey("Energy", KeyBase.Main.GetKey("MaxEnergy"));
            KeyBase.Main.ChangeKey("Day", 1);
        }

        public void ChangeEnergy(float Value)
        {
            KeyBase.Main.ChangeKey("Energy", Value);
        }

        public void ChangeCoin(float Value)
        {
            KeyBase.Main.ChangeKey("Coin", Value);
        }

        public void ChangeExp(float Value)
        {
            KeyBase.Main.ChangeKey("Exp", Value);
            KeyBase.Main.SetKey("Level", ValueBase.GetLevel(KeyBase.Main.GetKey("Exp")));
        }

        public void ChangePopulation(float Value)
        {
            KeyBase.Main.ChangeKey("Population", Value);
        }

        public void ChangeRank(float Value)
        {
            KeyBase.Main.ChangeKey("Rank", Value);
        }
    }
}