using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ADV
{
    public class SkillRenderer : UIElement {
        public Mark_Skill Target;
        public SkillType TargetType;
        public int Index = -1;
        [HideInInspector] public bool HaveUpgrade;
        [Space]
        public Animator Anim;
        public GameObject AnimBase;
        public SpriteRenderer Icon;
        public TextMeshPro CoolDownText;
        public TextMeshPro CountText;
        public TextMeshPro CountTextII;
        public SpriteRenderer CountIcon;
        [Space]
        public Sprite DefaultIcon;
        public Color ActiveColor;
        public Color DisableColor;
        public Color TextActiveColor;
        public Color TextDisableColor;

        public override void Render()
        {
            GetTarget();
            SetActive(GetActive());
            UpdateRender();
        }

        public virtual void UpdateRender()
        {
            if (!Target || (!Target.CanUse() && Target.GetKey("Passive") <= 0))
                SetEnable(false);
            else
                SetEnable(true);

            if (!Target || (!Target.CanUse() && Target.GetKey("Hidden") > 0))
            {
                Icon.sprite = DefaultIcon;
                CountText.text = "";
                CountTextII.text = "";
                CoolDownText.gameObject.SetActive(false);
                CoolDownText.text = "";
            }
            else
            {
                if (Target.GetIcon())
                    Icon.sprite = Target.GetIcon();
                else
                    Icon.sprite = DefaultIcon;

                if (Target.HasKey("Count"))
                    CountText.text = Target.GetKey("Count").ToString();
                else if (Target.HasKey("Upgrade"))
                    CountText.text = "+";
                else if (Target.HasKey("Cost"))
                    CountText.text = Target.GetKey("Cost").ToString();
                else if (Target.HasKey("CostII"))
                    CountText.text = Target.GetKey("CostII").ToString() + "xp";
                else if (Target.HasKey("CostIII"))
                    CountTextII.text = "   " + Target.GetKey("CostIII").ToString();
                else
                    CountText.text = "";

                if (Target.HasKey("CountII"))
                    CountTextII.text = Target.GetKey("CountII").ToString();
                else
                    CountTextII.text = "";

                if (Target.HasKey("CostIII"))
                    CountIcon.gameObject.SetActive(true);
                else
                    CountIcon.gameObject.SetActive(false);

                if (!Target.HasKey("CoolDown") || Target.GetKey("CCD") <= 0)
                {
                    CoolDownText.gameObject.SetActive(false);
                    CoolDownText.text = "";
                }
                else
                {
                    CoolDownText.text = ((int)Target.GetKey("CCD")).ToString();
                    CoolDownText.gameObject.SetActive(true);
                }
            }
        }

        public virtual void GetTarget()
        {
            if (!CombatControl.Main.SelectingCard)
            {
                Target = null;
                return;
            }
            Target = CombatControl.Main.SelectingCard.GetRenderSkill(Index);
        }

        public void SetActive(bool Value)
        {
            AnimBase.SetActive(Value);
        }

        public virtual bool GetActive()
        {
            return CombatControl.Main.SelectingCard;
        }

        public virtual void SetEnable(bool Value)
        {
            if (Value)
            {
                Icon.color = ActiveColor;
                CountText.color = TextActiveColor;
                CountTextII.color = TextActiveColor;
                CountIcon.color = ActiveColor;
            }
            else
            {
                Icon.color = DisableColor;
                CountText.color = TextDisableColor;
                CountTextII.color = TextActiveColor;
                CountIcon.color = DisableColor;
            }
        }
    }
}