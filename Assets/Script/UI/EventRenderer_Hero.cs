using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;
using TMPro;

namespace ESP
{
    public class EventRenderer_Hero : EventRenderer {
        public string HeroKey;
        public TextMeshPro LevelText;

        public override void Render()
        {
            base.Render();
            LevelText.text = KeyBase.Main.GetKey(HeroKey + "Level").ToString();
        }
    }
}