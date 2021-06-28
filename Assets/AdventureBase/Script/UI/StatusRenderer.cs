using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ADV
{
    public class StatusRenderer : MonoBehaviour {
        public GameObject AnimBase;
        public TextMeshPro Text;
        public int Index;
        public Mark_Status Target;

        public void RealUpdate(Card T)
        {
            if (T)
                Target = T.GetStatus(Index);
            else
                Target = null;
            Render();
        }

        public void Render()
        {
            if (Target)
            {
                AnimBase.SetActive(true);
                Text.text = Target.GetName();
                if (Target.HasKey("Energy"))
                    Text.text += " (" + Target.GetKey("Energy") + ")";
                if (Target.HasKey("Shield"))
                    Text.text += " (" + (int)Target.GetKey("Shield") + ")";
                if (Target.HasKey("Stack"))
                    Text.text += " (x" + Target.GetKey("Stack") + ")";
                if (Target.HasKey("Duration"))
                    Text.text += " [" + (int)Target.GetKey("Duration") + "]";
                if (Target.HasKey("Mana") && Target.HasKey("MaxMana"))
                    Text.text += " " + (int)(Target.PassValue("Mana", 1) * 100) + "%";
            }
            else
            {
                AnimBase.SetActive(false);
                Text.text = "";
            }
        }
    }
}