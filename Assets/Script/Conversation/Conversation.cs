using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class Conversation : MonoBehaviour {
        [HideInInspector] public KeyBase KB;
        [HideInInspector] public string LastContent;
        [HideInInspector] public int LastSentenceCount;
        [HideInInspector] public bool Active;
        public List<Sentence> Sentences;
        public List<ConversationChoice> Choices;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TimePassed()
        {
            ChangeKey("UpdateTime", 1);
            for (int i = 0; i < Sentences.Count; i++)
                Sentences[i].TimePassed();
            GetSentences();
        }

        public void AddSentence(Sentence Target)
        {
            Sentences.Add(Target);
            GetSentences();
        }

        public List<Sentence> GetSentences()
        {
            List<Sentence> Temp = new List<Sentence>();
            for (int i = 0; i < Sentences.Count; i++)
            {
                if (!Sentences[i].Active(this))
                    break;
                Temp.Add(Sentences[i]);
            }

            if (Temp.Count != LastSentenceCount)
            {
                int a = Temp.Count - LastSentenceCount;
                LastSentenceCount = Temp.Count;
                GetChoices(Temp[Temp.Count - 1]);
                Active = Temp.Count > 0;
                OnChange(a);
            }

            return Temp;
        }

        public List<ConversationChoice> GetChoices(Sentence LastSentence)
        {
            List<ConversationChoice> Cs = new List<ConversationChoice>();
            for (int i = 0; i < LastSentence.Choices.Count; i++)
            {
                if (LastSentence.Choices[i].Active(this))
                    Cs.Add(LastSentence.Choices[i]);
            }
            Choices = Cs;
            return Cs;
        }

        public void UpdateLastContent()
        {
            List<Sentence> Temp = GetSentences();
            if (Temp.Count <= 0)
                LastContent = "";
            else
                LastContent = Temp[Temp.Count - 1].GetContent();
        }

        public void OnChange(int CountChange)
        {
            SetKey("UpdateTime", 0);
            UpdateLastContent();
            ConversationControl.Main.OnCVChange(this);
        }

        public string GetLastContent()
        {
            return LastContent;
        }

        //----------------------------------------------------- KeyBase -----------------------------------------------------

        public KeyBase GetKeyBase()
        {
            if (!KB)
                KB = GetComponent<KeyBase>();
            return KB;
        }

        public void AddKey(string Key)
        {
            GetKeyBase().AddKey(Key);
        }

        public bool HasKey(string Key)
        {
            return GetKeyBase().HasKey(Key);
        }

        public float GetKey(string Key)
        {
            return GetKeyBase().GetKey(Key);
        }

        public float ChangeKey(string Key, float Value)
        {
            return GetKeyBase().ChangeKey(Key, Value);
        }

        public void SetKey(string Key, float Value)
        {
            GetKeyBase().SetKey(Key, Value);
        }
    }
}