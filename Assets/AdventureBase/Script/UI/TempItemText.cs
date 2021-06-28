using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class TempItemText : MonoBehaviour {
        public TextMeshPro TEXT;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (CombatControl.Main.SelectingItem)
                TEXT.text = CombatControl.Main.SelectingItem.GetName();
            else
                TEXT.text = "";
        }
    }
}