using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class Event : MonoBehaviour {
        [HideInInspector] public KeyBase KB;
        public string Name;
        public string DisplayName;
        [Space]
        public List<EventStep> Steps;
        public EventStep CurrentStep;
        public int CurrentIndex = -1;
        public bool Active;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void Effect()
        {
            CurrentIndex = 0;
            Active = true;
            ActiveStep(0);
        }

        public void ActiveStep(int Index)
        {
            if (Index >= Steps.Count)
                return;
            if (!Steps[Index])
                return;
            CurrentStep = Steps[Index];
            CurrentStep.OnEffect();
        }

        public virtual void EffectUpdate(float Value)
        {
            if (!Active || !CurrentStep)
                return;
            CurrentStep.EffectUpdate(Value);
        }

        public void NextStep()
        {
            if (!Active)
                return;
            CurrentIndex++;
            ActiveStep(CurrentIndex);
        }

        public void OnEnd()
        {
            Active = false;
            CurrentStep = null;
            CurrentIndex = -1;
        }

        public void PassMessage(string Text, float Value)
        {
            if (CurrentStep)
                CurrentStep.PassMessage(Text, Value);
        }

        public string GetName()
        {
            return Name;
        }

        public string GetDisplayName()
        {
            if (DisplayName == "")
                return GetName();
            return DisplayName;
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