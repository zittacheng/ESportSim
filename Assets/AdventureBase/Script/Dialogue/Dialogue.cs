using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Dialogue : MonoBehaviour {
        public float TextDelayScale = 1;
        public DialogueChoice DefaultChoice;
        public List<DialogueChoice> AddChoices;
        [HideInInspector] public List<DialogueUnit> Units;
        [HideInInspector] public DialogueInfo Info;

        public void Awake()
        {
            foreach (DialogueUnit DU in GetComponentsInChildren<DialogueUnit>())
                Units.Add(DU);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public List<DialogueUnit> GetUnits()
        {
            return Units;
        }

        public DialogueChoice GetDefaultChoice()
        {
            return DefaultChoice;
        }

        public DialogueChoice GetChoice(int Index)
        {
            if (AddChoices.Count <= Index)
                return null;
            return AddChoices[Index];
        }

        public DialogueInfo GetInfo()
        {
            if (!Info)
                Info = GetComponent<DialogueInfo>();
            return Info;
        }
    }
}