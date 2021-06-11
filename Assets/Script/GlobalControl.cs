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

        }

        // Update is called once per frame
        public void Update()
        {
            
        }

        public void ChangeEnergy(float Value)
        {
            KeyBase.Main.ChangeKey("Energy", Value);
        }

        public void ChangeCoin(float Value)
        {
            KeyBase.Main.ChangeKey("Coin", Value);
        }

        public void ChangeStreamPoint(float Value)
        {
            KeyBase.Main.ChangeKey("StreamPoint", Value);
        }

        public void ChangeGamePoint(float Value)
        {
            KeyBase.Main.ChangeKey("GamePoint", Value);
        }

        public void ChangeExp(float Value)
        {
            KeyBase.Main.ChangeKey("Exp", Value);
            KeyBase.Main.SetKey("Level", ValueBase.GetLevel(KeyBase.Main.GetKey("Exp")));
        }
    }
}