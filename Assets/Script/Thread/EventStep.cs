using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class EventStep : MonoBehaviour {
        [HideInInspector] public KeyBase KB;

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        public virtual void OnEffect()
        {

        }

        public virtual void EffectUpdate(float Value)
        {

        }

        public virtual void PassMessage(string Text, float Value)
        {

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