using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class DialogueControl : MonoBehaviour {
        public static DialogueControl Main;
        public float DefaultTextDelay;
        public float TimeScale;
        public bool IgnoreReturn;
        [Space]
        public Dialogue CurrentDialogue;
        public float AdvanceProtection;
        public bool Advancing;
        public bool InProcess;
        [HideInInspector] public string MainText;
        [Space]
        public Dialogue TempDialogue;
        
        // Recommend Text Delay = 0.04s
        // Recommend Sentence Delay = 0.4s ~ 0.5s

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (AdvanceProtection > 0)
                AdvanceProtection -= Time.deltaTime;

            // Temp
            if (Input.GetKeyDown(KeyCode.S) && !CurrentDialogue)
                StartDialogue(TempDialogue);
        }

        public void StartDialogue(Dialogue D)
        {
            InProcess = true;
            MainText = "";
            CurrentDialogue = D;
            StartCoroutine(ProcessDialogueIE(D, DefaultTextDelay * TimeScale));
        }

        public IEnumerator ProcessDialogueIE(Dialogue Group, float Delay)
        {
            Delay *= Group.TextDelayScale;
            foreach (DialogueUnit Unit in Group.GetUnits())
                yield return ProcessUnitIE(Unit, Delay);
            InProcess = false;
            Advancing = false;
            AdvanceProtection = 0.25f;
        }

        public IEnumerator ProcessUnitIE(DialogueUnit Unit, float Delay)
        {
            Delay *= Unit.TextDelayScale;
            if (!Advancing)
                yield return new WaitForSeconds(Unit.StartDelay * TimeScale);
            for (int i = 0; i < Unit.Content.Length; i++)
            {
                string s = Unit.Content.Substring(i, 1);
                if (s == "[")
                {
                    string key = Unit.Content.Substring(i + 1, 1);
                    if (key == "_")
                    {
                        if (!IgnoreReturn)
                            MainText += "\n";
                        else
                            MainText += " ";
                        i++;
                        if (!Advancing)
                            yield return new WaitForSeconds(Delay);
                    }
                }
                else
                {
                    MainText += Unit.Content.Substring(i, 1);
                    if (!Advancing)
                        yield return new WaitForSeconds(Delay);
                }
            }
        }

        public void Advance()
        {
            if (InProcess)
            {
                Advancing = true;
                return;
            }

            if (AdvanceProtection > 0)
                return;
            if (!GetCurrentDialogue() || !GetCurrentDialogue().GetDefaultChoice())
                return;

            CurrentDialogue.GetDefaultChoice().Effect();
        }

        public Dialogue GetCurrentDialogue()
        {
            return CurrentDialogue;
        }

        /*public IEnumerator IniChoices(DialogueGroup Group)
        {

        }

        public void SelectChoice(int Index)
        {
            if (!ChoicingCV || CurrentTime <= 0)
                return;
            foreach (ChoiceRenderer CR in Choices)
                CR.End(CR.Index == Index);
            ChoicingCV.StartGroup = ChoicingCV.Choices[Index].NextGroup;
            MainText.text += "\n \n";
            StartCoroutine(ProcessConversationIE(ChoicingCV, 0.85f));
        }

        public void EndConversation()
        {
            Active = false;
            StartCoroutine(EndConversationIE());
        }

        public IEnumerator EndConversationIE()
        {
            foreach (ChoiceRenderer CR in Choices)
                CR.End(false);
            WritingSoundVolume = 0f;
            Anim.SetTrigger("Exit");
            yield return new WaitForSeconds(3f);
            CombatControl.Main.ExitConversation();
            CurrentCV = null;
        }

        public void ForceEndConversation()
        {
            Active = false;
            StartCoroutine(ForceEndConversationIE());
        }

        public IEnumerator ForceEndConversationIE()
        {
            foreach (ChoiceRenderer CR in Choices)
                CR.End(false);
            WritingSoundVolume = 0f;
            Anim.SetTrigger("Exit");
            CombatControl.Main.ForceExitConversation();
            CurrentCV = null;
            yield return 0;
        }*/
    }
}