using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ConversationContentRenderer : MonoBehaviour {
        public static ConversationContentRenderer Main;
        public Conversation LastConversation;
        public GameObject SentenceBase;
        public float RendererDistance;
        public float LeftPosition;
        public float RightPosition;
        public GameObject LeftRenderer;
        public GameObject RightRenderer;
        public List<SentenceRenderer> SRenderers;
        [Space]
        public float CurrentHeight;
        public float MaxHeight;
        public float Sensitivity;
        public float EndHeight;
        public bool MouseOn;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (LastConversation != ConversationControl.Main.CurrentConversation)
                RenderUpdate();

            if (Input.GetKey(KeyCode.Space))
                RenderUpdate();

            HeightUpdate();
        }

        public void RenderUpdate()
        {
            LastConversation = ConversationControl.Main.CurrentConversation;
            RenderSentences(LastConversation);
            CurrentHeight = MaxHeight;
        }

        public void HeightUpdate()
        {
            if (MouseOn)
            {
                float Change = Input.GetAxisRaw("Mouse ScrollWheel") * Sensitivity;
                CurrentHeight -= Change;
            }

            if (CurrentHeight <= 0)
                CurrentHeight = 0;
            else if (CurrentHeight > MaxHeight)
            {
                CurrentHeight = MaxHeight;
                if (CurrentHeight <= 0)
                    CurrentHeight = 0;
            }

            SentenceBase.transform.localPosition = new Vector3(SentenceBase.transform.localPosition.x, CurrentHeight, SentenceBase.transform.localPosition.z);
        }

        public void RenderSentences(Conversation Target)
        {
            RemoveRenderers();
            List<Sentence> Sentences;
            if (!Target)
                Sentences = new List<Sentence>();
            else
                Sentences = Target.GetSentences();
            float Next = -RendererDistance;
            for (int i = 0; i < Sentences.Count; i++)
            {
                GameObject G = null;
                if (Sentences[i].RenderType == SentenceRenderType.Left)
                {
                    G = Instantiate(LeftRenderer, SentenceBase.transform);
                    G.transform.localPosition = new Vector3(LeftPosition, G.transform.localPosition.y, G.transform.localPosition.z);
                }
                else if (Sentences[i].RenderType == SentenceRenderType.Right)
                {
                    G = Instantiate(RightRenderer, SentenceBase.transform);
                    G.transform.localPosition = new Vector3(RightPosition, G.transform.localPosition.y, G.transform.localPosition.z);
                }
                if (!G)
                    continue;

                SentenceRenderer SR = G.GetComponent<SentenceRenderer>();
                SR.Render(Sentences[i].GetContent(), Next, out float TempNext);
                SRenderers.Add(SR);
                Next = TempNext;
                Next -= RendererDistance;
            }
            MaxHeight = -Next - EndHeight;
            CurrentHeight = 0;
        }

        public void RemoveRenderers()
        {
            for (int i = SRenderers.Count - 1; i >= 0; i--)
            {
                Destroy(SRenderers[i].gameObject);
                SRenderers.RemoveAt(i);
            }
        }
    }
}