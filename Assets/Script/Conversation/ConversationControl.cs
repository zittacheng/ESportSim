using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ConversationControl : MonoBehaviour {
        public static ConversationControl Main;
        public List<Conversation> Conversations;
        public Conversation TempConversation;
        public Conversation CurrentConversation;
        [HideInInspector] public List<Conversation> Changes;
        public List<Conversation> CVOrder;

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                RenderConversation(TempConversation);
        }

        public void LateUpdate()
        {
            if (Changes.Count > 0)
            {
                UpdateCVOrder();
                Changes.Clear();
            }
        }

        public void RenderConversation(Conversation Target)
        {
            CurrentConversation = Target;
            if (ConversationContentRenderer.Main)
                ConversationContentRenderer.Main.RenderUpdate();
        }

        public void TimePassed()
        {
            RenderConversation(null);
            for (int i = 0; i < Conversations.Count; i++)
                Conversations[i].TimePassed();
        }

        public void OnCVChange(Conversation Target)
        {
            Changes.Add(Target);
            if (ConversationContentRenderer.Main)
                ConversationContentRenderer.Main.RenderUpdate();
        }

        public void UpdateCVOrder()
        {
            CVOrder = new List<Conversation>();
            List<Conversation> Temp = new List<Conversation>();
            foreach (Conversation CV in Conversations)
                Temp.Add(CV);
            while (Temp.Count > 0)
            {
                float Lowest = Mathf.Infinity;
                Conversation Target = null;
                for (int i = Temp.Count - 1; i >= 0; i--)
                {
                    if (Temp[i].GetKey("UpdateTime") <= Lowest)
                    {
                        Lowest = Temp[i].GetKey("UpdateTime");
                        Target = Temp[i];
                    }
                }
                if (!Target)
                    break;
                Temp.Remove(Target);
                CVOrder.Add(Target);
            }
        }

        public Conversation GetCurrentConversation()
        {
            return CurrentConversation;
        }
    }
}