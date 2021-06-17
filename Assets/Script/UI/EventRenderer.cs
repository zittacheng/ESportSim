using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ESP
{
    public class EventRenderer : MonoBehaviour {
        public GameObject AnimBase;
        public TextMeshPro NameText;
        public Event Target;
        public int TargetIndex = -1;
        public ERIT Interaction;

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            Render();
        }

        public void Render()
        {
            if (!GetTarget())
            {
                NameText.text = "";
                AnimBase.SetActive(false);
            }
            else
            {
                NameText.text = GetTarget().GetDisplayName();
                AnimBase.SetActive(true);
            }
        }

        public void Interact()
        {
            if (!GetTarget())
                return;
            if (Interaction == ERIT.Add && ThreadControl.Main.CanAddEvent(GetTarget()))
                ThreadControl.Main.AddEvent(GetTarget());
            else if (Interaction == ERIT.Remove)
                ThreadControl.Main.RemoveEvent(TargetIndex);
            else if (Interaction == ERIT.Confirm && ThreadControl.Main.CanAddEvent(GetTarget()))
            {
                ThreadControl.Main.AddEvent(GetTarget());
                SubUIControl.Main.CloseWindow();
                ThreadControl.Main.StartProcess();
            }
        }

        public Event GetTarget()
        {
            if (Target)
                return Target;
            if (TargetIndex != -1)
                return ThreadControl.Main.GetEvent(TargetIndex);
            return null;
        }
    }

    public enum ERIT
    {
        Add,
        Remove,
        Confirm
    }
}