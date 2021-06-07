using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ESP
{
    public class Temp_CostText : MonoBehaviour {
        public TextMeshPro TimeText;
        public TextMeshPro EnergyText;
        public TextMeshPro CoinText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ThreadControl.Main.GetCost(out float TC, out float EC, out float CC);
            TimeText.text = "Time Cost: " + TC;
            EnergyText.text = "Stamina Cost: " + EC;
            CoinText.text = "Coin Cost: " + CC;
        }
    }
}