using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UndoUnit : MonoBehaviour {
        public GameObject AddItem;
        public GameObject RemoveItem;
        public float CoinChange;
        public string SwitchHero;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Execute()
        {
            if (AddItem)
                CombatControl.Main.AddItem(AddItem, CoinChange, CombatControl.Main.MCGroup);
            if (RemoveItem)
                CombatControl.Main.RemoveItem(RemoveItem, CoinChange, CombatControl.Main.MCGroup);
            if (SwitchHero != "")
                CombatControl.Main.MCGroup.SwitchCard(SwitchHero);
        }
    }
}