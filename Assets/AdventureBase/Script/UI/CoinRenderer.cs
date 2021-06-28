using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class CoinRenderer : MonoBehaviour {
        public TextMeshPro CoinText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (CombatControl.Main.SelectintGroup)
            {
                AIControl_Friendly AF = (AIControl_Friendly)CombatControl.Main.SelectintGroup.GetAIControl();
                CoinText.text = ((int)AF.Coin).ToString();
            }
            if (!CombatControl.Main.SelectintGroup)
                CoinText.text = ((int)CombatControl.Main.Coin).ToString();
        }
    }
}