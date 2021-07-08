using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ADV;

namespace ESP
{
    public class UpgradeRenderer : MonoBehaviour {
        public UIWindow_Shop Shop;
        public Upgrade Target;
        public GameObject ActiveBase;
        public GameObject SelectionBase;
        public GameObject UnlockedBase;
        public GameObject EmptyBase;
        public TextMeshPro NameText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!GetTarget())
            {
                NameText.text = "";
                EmptyBase.SetActive(true);
                ActiveBase.SetActive(false);
                SelectionBase.SetActive(false);
                UnlockedBase.SetActive(false);
                return;
            }

            EmptyBase.SetActive(false);
            NameText.text = GetTarget().GetName();

            if (GetTarget().Unlocked())
            {
                ActiveBase.SetActive(false);
                SelectionBase.SetActive(false);
                UnlockedBase.SetActive(true);
            }
            else
            {
                SelectionBase.SetActive(Shop.SelectingUpgrade == GetTarget());
                ActiveBase.SetActive(!SelectionBase.activeSelf);
                UnlockedBase.SetActive(false);
            }
        }

        public void Interact()
        {
            if (!GetTarget())
                return;
            Shop.SelectingUpgrade = GetTarget();
        }

        public void DoubleInteract()
        {
            if (!GetTarget() || !GetTarget().CanUnlock())
                return;
            GetTarget().OnUnlock();
        }

        public Upgrade GetTarget()
        {
            return Target;
        }
    }
}