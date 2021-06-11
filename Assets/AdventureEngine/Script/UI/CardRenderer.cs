using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ADV
{
    public class CardRenderer : UIElement {
        public Card Target;
        public GameObject AnimBase;
        public TextMeshPro NameText;
        public TextMeshPro LifeBar;
        public int LifeRenderDirection;
        public List<StatusRenderer> StatusRenderers;

        public override void Render()
        {
            GetTarget();
            MainRender();
            foreach (StatusRenderer SR in StatusRenderers)
                SR.RealUpdate(Target);
            base.Render();
        }

        public void GetTarget()
        {
            //Target = CombatControl.Main.SelectingCard;
            if (Target)
                SetActive(true);
            else
                SetActive(false);
        }

        public void MainRender()
        {
            if (!Target)
                return;

            // Temp
            if (Target.CombatActive())
                AnimBase.SetActive(true);
            else
                AnimBase.SetActive(false);

            if (LifeRenderDirection == 1)
                NameText.text = Target.GetName() + " " + Mathf.RoundToInt(Target.GetLife()) + "/" + Mathf.RoundToInt(Target.GetMaxLife());
            else if (LifeRenderDirection == -1)
                NameText.text = Mathf.RoundToInt(Target.GetLife()) + "/" + Mathf.RoundToInt(Target.GetMaxLife()) + " " + Target.GetName();
            else
                NameText.text = Target.GetName();
            int a = Mathf.RoundToInt(Target.GetLife() / Target.GetMaxLife() * 25);
            string s = "";
            for (int i = 0; i < a; i++)
                s += "-";
            LifeBar.text = s;
        }

        public void SetActive(bool Value)
        {
            AnimBase.SetActive(Value);
        }
    }
}