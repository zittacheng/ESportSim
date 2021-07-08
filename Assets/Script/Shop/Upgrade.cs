using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class Upgrade : MonoBehaviour {
        public string Key;
        public string Name;
        [TextArea] public string Description;
        public float Cost;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool Unlocked()
        {
            return KeyBase.Main.GetKey(Key + "Unlocked") == 1;
        }

        public bool CanUnlock()
        {
            return !Unlocked() && KeyBase.Main.GetKey("Coin") >= Cost;
        }

        public void OnUnlock()
        {
            GlobalControl.Main.ChangeCoin(-Cost);
            KeyBase.Main.SetKey(Key + "Unlocked", 1);
        }

        public string GetName()
        {
            return Name;
        }
    }
}