using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ESP
{
    public class ConversationRenderer : MonoBehaviour {
        public int Index;
        public GameObject ActiveBase;
        public GameObject SelectionBase;
        public GameObject EmptyBase;
        public TextMeshPro ContentText;
        public float MaxRange = -25;

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
            ActiveBase.SetActive(ConversationControl.Main.GetCurrentConversation() != GetTarget());
            SelectionBase.SetActive(!ActiveBase.activeSelf);
            ContentText.text = GetTarget().GetLastContent();
        }

        public Conversation GetTarget()
        {
            if (ConversationControl.Main.CVOrder.Count <= Index)
                return null;
            return ConversationControl.Main.CVOrder[Index];
        }

        public bool HeightCheck()
        {
            float h = transform.localPosition.y + transform.parent.transform.localPosition.y;
            return h >= -25.2f && h <= 0.2f;
        }

        public void Select()
        {
            if (!GetTarget() || !HeightCheck())
                return;
            ConversationControl.Main.RenderConversation(GetTarget());
        }
    }
}