using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class DialogueChoiceRenderer : MonoBehaviour {
        public GameObject EmptyBase;
        public GameObject ActiveBase;
        public GameObject SelectionBase;
        public TextMeshPro ContentText;
        public int Index;
        public bool MouseOn;

        public void OnEnable()
        {
            Render();
        }

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

        public void Interact()
        {
            if (!GetTarget())
                return;
            GetTarget().Effect();
        }

        public DialogueChoice GetTarget()
        {
            if (!DialogueControl.Main.GetCurrentDialogue() || DialogueControl.Main.InProcess)
                return null;
            return DialogueControl.Main.GetCurrentDialogue().GetChoice(Index);
        }
    }
}