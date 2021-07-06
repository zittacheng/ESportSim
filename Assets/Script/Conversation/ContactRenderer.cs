using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;
using TMPro;

namespace ESP
{
    public class ContactRenderer : MonoBehaviour {
        public GameObject RenderBase;
        public TextMeshPro NameText;
        public TextMeshPro InfoText;
        public EnergyBar ValueBar;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Render();
        }

        public void Render()
        {
            if (!GetTarget())
            {
                RenderBase.SetActive(false);
                return;
            }
            RenderBase.SetActive(true);

            if (NameText)
                NameText.text = GetTarget().GetName();

            if (InfoText)
                InfoText.text = GetTarget().GetInfo();

            if (ValueBar)
                ValueBar.Render(KeyBase.Main.GetKey(GetTarget().GetKey() + "Value"));
        }

        public ConversationInfo GetTarget()
        {
            if (!ConversationControl.Main.GetCurrentConversation())
                return null;
            return ConversationControl.Main.GetCurrentConversation().GetInfo();
        }
    }
}