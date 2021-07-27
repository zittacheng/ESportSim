using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ADV
{
    public class StatusRenderer : MonoBehaviour {
        public GameObject AnimBase;
        public SpriteRenderer SR;
        public TextMeshPro Text;
        public Image DurationMask;
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
                string Name = Target.GetName();
                if (Target.HasKey("Energy"))
                    Name += " (" + Target.GetKey("Energy") + ")";
                if (Target.HasKey("Shield"))
                    Name += " (" + (int)Target.GetKey("Shield") + ")";
                if (Target.HasKey("Stack"))
                    Name += " (x" + Target.GetKey("Stack") + ")";
                if (Target.HasKey("Duration"))
                    Name += " [" + (int)Target.GetKey("Duration") + "]";
                if (Target.HasKey("Mana") && Target.HasKey("MaxMana"))
                    Name += " " + (int)(Target.PassValue("Mana", 1) * 100) + "%";

                if (Text)
                    Text.text = Name;

                if (DurationMask)
                {
                    if (!Target.HasKey("OriDuration") || Target.GetKey("OriDuration") <= 0)
                        DurationMask.gameObject.SetActive(false);
                    else
                    {
                        DurationMask.gameObject.SetActive(true);
                        DurationMask.fillAmount = (Target.GetKey("Duration") / Target.GetKey("OriDuration"));
                    }
                }

                if (SR)
                    SR.sprite = Target.GetIcon();
            }
            else
            {
                AnimBase.SetActive(false);
                if (Text)
                    Text.text = "";
                if (DurationMask)
                    DurationMask.gameObject.SetActive(false);
                if (SR)
                    SR.sprite = null;
            }
        }
    }
}