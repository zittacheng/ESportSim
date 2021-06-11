using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ESP
{
    public class Temp_CostText : MonoBehaviour {
        public TextMeshPro EnergyText;
        public TextMeshPro CoinText;
        public GameObject EnergySprite;
        public GameObject CoinSprite;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ThreadControl.Main.GetCost(out float TC, out float EC, out float CC);
            if (EC > 0)
            {
                EnergySprite.SetActive(true);
                EnergyText.text = "-" + EC;
            }
            else
            {
                EnergySprite.SetActive(false);
                EnergyText.text = "";
            }
            
            if (CC > 0)
            {
                CoinSprite.SetActive(true);
                CoinText.text = "-" + CC;
            }
            else
            {
                CoinSprite.SetActive(false);
                CoinText.text = "";
            }
        }
    }
}