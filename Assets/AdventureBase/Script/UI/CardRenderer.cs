using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ADV
{
    public class CardRenderer : UIElement {
        public int Side;
        public int Index;
        public Card DefaultTarget;
        public bool Follow;
        public GameObject AnimBase;
        public TextMeshPro NameText;
        public int LifeRenderDirection;
        public EnergyBar LifeBar;
        public EnergyBarScale LifeBarScale;
        [Space]
        public EnergyBar DamageBar;
        public float DamageBarDelay;
        [Space]
        public EnergyBar ShieldBar;
        public EnergyBar ManaBar;
        [Space]
        public TextMeshPro LifeText;
        public TextMeshPro LifeTextII;
        public TextMeshPro ManaText;
        public TextMeshPro ManaTextII;
        public TextMeshPro DamageText;
        public TextMeshPro AttackSpeedText;
        public TextMeshPro RecoverySpeedText;
        public GameObject MainSprite;
        public GameObject MCSprite;
        public List<StatusRenderer> StatusRenderers;

        public override void Render()
        {
            if (GetTarget())
            {
                SetActive(true);
                if (Follow)
                    transform.position = new Vector3(GetTarget().GetPosition().x, GetTarget().GetPosition().y, transform.position.z);
            }
            else
                SetActive(false);
            MainRender();
            DamageBarUpdate();
            foreach (StatusRenderer SR in StatusRenderers)
                SR.RealUpdate(GetTarget());
            base.Render();
        }

        public Card GetTarget()
        {
            if (DefaultTarget)
                return DefaultTarget;
            if (Side == 0 && CombatControl.Main.FriendlyCards.Count > Index)
                return CombatControl.Main.FriendlyCards[Index];
            else if (Side == 1 && CombatControl.Main.EnemyCards.Count > Index)
                return CombatControl.Main.EnemyCards[Index];
            return null;
        }

        public void MainRender()
        {
            if (!GetTarget())
                return;

            // Temp
            if (GetTarget().CombatActive())
                SetActive(true);
            else
                SetActive(false);

            if (MCSprite && MainSprite)
            {
                if (GetTarget() == CombatControl.Main.GetCurrentMC())
                {
                    MCSprite.SetActive(true);
                    MainSprite.SetActive(false);
                }
                else
                {
                    MCSprite.SetActive(false);
                    MainSprite.SetActive(true);
                }
            }

            if (NameText)
            {
                if (LifeRenderDirection == 1)
                    NameText.text = GetTarget().GetName() + " " + Mathf.RoundToInt(GetTarget().GetLife()) + "/" + Mathf.RoundToInt(GetTarget().GetMaxLife());
                else if (LifeRenderDirection == -1)
                    NameText.text = Mathf.RoundToInt(GetTarget().GetLife()) + "/" + Mathf.RoundToInt(GetTarget().GetMaxLife()) + " " + GetTarget().GetName();
                else
                    NameText.text = GetTarget().GetName();
            }

            if (GetTarget().GetLife() <= 0)
            {
                if (LifeText)
                    LifeText.text = "";
                if (LifeTextII)
                    LifeTextII.text = "";
            }
            else if (GetTarget().GetLife() / GetTarget().GetMaxLife() >= 0.05f)
            {
                if (LifeText)
                    LifeText.text = ((int)GetTarget().GetLife()).ToString();
                if (LifeTextII)
                    LifeTextII.text = "";
            }
            else
            {
                if (LifeText)
                    LifeText.text = "";
                if (LifeTextII)
                    LifeTextII.text = ((int)GetTarget().GetLife()).ToString();
            }

            float m = GetTarget().PassValue("Mana");
            if (m <= 0)
            {
                if (ManaText)
                    ManaText.text = "";
                if (ManaTextII)
                    ManaTextII.text = "";
            }
            else if (m >= 0.05f)
            {
                if (ManaText)
                    ManaText.text = (int)(m * 100) + "%";
                if (ManaTextII)
                    ManaTextII.text = "";
            }
            else
            {
                if (ManaText)
                    ManaText.text = "";
                if (ManaTextII)
                    ManaTextII.text = (int)(m * 100) + "%";
            }

            int a = Mathf.RoundToInt(GetTarget().GetLife() / GetTarget().GetMaxLife() * 25);
            string s = "";
            for (int i = 0; i < a; i++)
                s += "-";

            float next = GetTarget().GetLife() / GetTarget().GetMaxLife();
            float current = 0;
            if (LifeBar)
                current = LifeBar.CurrentValue;

            float h = 0;
            if (ShieldBar)
            {
                h = GetTarget().PassValue("Shield");
                if (h > 0)
                {
                    float h2 = (GetTarget().GetLife() + h) / GetTarget().GetMaxLife();
                    if (h2 > 1)
                    {
                        ShieldBar.Render(1);
                        next = GetTarget().GetLife() / (GetTarget().GetLife() + h);
                    }
                    else
                        ShieldBar.Render(h2);
                }
                else
                    ShieldBar.Render(0);
            }

            if (LifeBar)
                LifeBar.Render(next);

            if (LifeBarScale)
            {
                float max = GetTarget().GetMaxLife();
                if (GetTarget().GetLife() + h > max)
                    max = GetTarget().GetLife() + h;
                max = (((int)(max / 10)) / 5) * 5 - 1;
                LifeBarScale.Render((int)max);
            }
            if (DamageBar && next < current && /*DamageBar.CurrentValue <= LifeBar.CurrentValue*/ DamageBarDelay <= 0)
            {
                DamageBar.Render(current);
                DamageBarDelay = 0.1f;
            }

            if (ManaBar)
                ManaBar.Render(GetTarget().PassValue("Mana"));

            if (DamageText)
                DamageText.text = StatToText(GetTarget().GetBaseDamage());

            if (AttackSpeedText)
                AttackSpeedText.text = StatToText(GetTarget().PassValue("AttackSpeed", 1));

            if (RecoverySpeedText)
                RecoverySpeedText.text = StatToText(GetTarget().PassValue("ManaRecovery", 1) * (1 / GetTarget().PassValue("MaxMana", 0)));
        }

        public void DamageBarUpdate()
        {
            if (!DamageBar)
                return;

            if (DamageBarDelay > 0)
            {
                DamageBarDelay -= Time.deltaTime;
                return;
            }

            if (DamageBar.CurrentValue <= LifeBar.CurrentValue)
            {
                DamageBar.Render(0);
                return;
            }

            DamageBar.Render(DamageBar.CurrentValue - 0.5f * Time.deltaTime);
        }

        public string StatToText(float Stat)
        {
            if (Stat == 0)
                return "0.0";
            float Temp = ((int)(Stat / 0.1f)) / 10f + 0.1f;
            if (Temp < 10 && Temp % 1 == 0)
                return Temp + ".0";
            return Temp.ToString();
        }

        public void SetActive(bool Value)
        {
            if (AnimBase)
                AnimBase.SetActive(Value);
        }
    }
}