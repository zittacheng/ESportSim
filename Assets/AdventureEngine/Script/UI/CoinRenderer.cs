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
            CoinText.text = CombatControl.Main.Coin.ToString();
        }
    }
}