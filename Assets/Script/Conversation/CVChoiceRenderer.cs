using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;
using TMPro;

namespace ESP
{
    public class CVChoiceRenderer : MonoBehaviour {
        public int Index;
        public GameObject ActiveBase;
        public GameObject SelectionBase;
        public GameObject EmptyBase;
        public TextMeshPro ContentText;
        public bool MouseOn;

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
                EmptyBase.SetActive(true);
                ActiveBase.SetActive(false);
                SelectionBase.SetActive(false);
                ContentText.text = "";
                return;
            }

            EmptyBase.SetActive(false);
            ActiveBase.SetActive(!MouseOn);
            SelectionBase.SetActive(!ActiveBase.activeSelf);
            ContentText.text = GetTarget().GetContent();
        }

        public ConversationChoice GetTarget()
        {
            if (!ConversationControl.Main.GetCurrentConversation() || ConversationControl.Main.GetCurrentConversation().Choices.Count <= Index)
                return null;
            return ConversationControl.Main.GetCurrentConversation().Choices[Index];
        }

        public void Select()
        {
            if (!GetTarget())
                return;
            GetTarget().Effect(ConversationControl.Main.GetCurrentConversation());
        }
    }
}