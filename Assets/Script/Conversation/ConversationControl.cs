using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LNF
{
    public class ConversationControl : MonoBehaviour {
        public static ConversationControl Main;
        public float DefaultTextSpeed;
        public TextMeshPro MainText;
        public Animator Anim;
        public Conversation CurrentCV;
        public bool Active;
        [Space]
        public Conversation ChoicingCV;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartConversation(Conversation CV)
        {
            MainText.text = "";
            StartCoroutine(ProcessConversationIE(CV, CV.StartGroup, 0f));
        }

        public IEnumerator ProcessConversationIE(Conversation CV, SentenceGroup SG, float StartDelay)
        {
            yield return new WaitForSeconds(StartDelay);
            float Speed = DefaultTextSpeed;
            if (CV.OverrideSpeed >= 0)
                Speed = CV.OverrideSpeed;
            yield return ProcessGroupIE(SG, Speed);
            if (CV.Choices.Count > 0)
                yield return IniChoices(CV);
        }

        public IEnumerator ProcessGroupIE(SentenceGroup Group, float Speed)
        {
            if (Group.OverrideSpeed >= 0)
                Speed = Group.OverrideSpeed;
            foreach (Sentence Unit in Group.Sentences)
                yield return ProcessUnitIE(Unit, Speed);
        }

        public IEnumerator ProcessUnitIE(Sentence Unit, float Speed)
        {
            if (Unit.OverrideSpeed >= 0)
                Speed = Unit.OverrideSpeed;
            yield return new WaitForSeconds(Unit.StartDelay);
            if (Unit.StartReturn)
            {
                yield return new WaitForSeconds(Speed);
                MainText.text += "\n";
            }
            for (int i = 0; i < Unit.Content.Length; i++)
            {
                MainText.text += Unit.Content.Substring(i, 1);
                yield return new WaitForSeconds(Speed);
            }
        }
        
        // Ini choice renderer
        public IEnumerator IniChoices(Conversation CV)
        {
            ChoicingCV = CV;
            yield return 0;
        }

        public void SelectChoice(int Index)
        {
            MainText.text += "\n \n";
            StartCoroutine(ProcessConversationIE(ChoicingCV, ChoicingCV.Choices[Index].NextGroup, 0.85f));
        }

        public void EndConversation()
        {
            Active = false;
            StartCoroutine(EndConversationIE());
        }

        public IEnumerator EndConversationIE()
        {
            Anim.SetTrigger("Exit");
            yield return new WaitForSeconds(3f);
            CurrentCV = null;
        }

        public void ForceEndConversation()
        {
            Active = false;
            StartCoroutine(ForceEndConversationIE());
        }

        public IEnumerator ForceEndConversationIE()
        {
            Anim.SetTrigger("Exit");
            CurrentCV = null;
            yield return 0;
        }
    }
}