using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark : MonoBehaviour {
        public KeyBase KB;
        [HideInInspector] public MarkInfo Info;
        [HideInInspector] public Card Source;

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        // Modify Signals sent
        public virtual void OutputSignal(Signal S)
        {

        }

        // Modify Signals recieved
        public virtual void InputSignal(Signal S)
        {

        }

        // After the signal's effect
        public virtual void ReturnSignal(Signal S)
        {

        }

        // After the signal's effect
        public virtual void ConfirmSignal(Signal S)
        {

        }

        // Passive stat modifier
        public virtual float PassValue(string Key, float Value)
        {
            return Value;
        }

        public virtual void TimePassed(float Value)
        {

        }

        public virtual void StartOfCombat()
        {

        }

        public virtual void EndOfCombat()
        {

        }

        public virtual void Death()
        {

        }

        public virtual void SendSignal(GameObject Prefab)
        {
            SendSignal(Prefab, new List<string>());
        }

        public virtual void SendSignal(GameObject Prefab, List<string> LS)
        {

        }

        public virtual bool Active()
        {
            return true;
        }

        public KeyBase GKB()
        {
            return KB;
        }

        public void AddKey(string Key)
        {
            GKB().AddKey(Key);
        }

        public void AddKeys(KeyBase KB)
        {
            GKB().AddKeys(KB);
        }

        public bool HasKey(string Key)
        {
            return GKB().HasKey(Key);
        }

        public float GetKey(string Key)
        {
            return GKB().GetKey(Key);
        }

        public float ChangeKey(string Key, float Value)
        {
            return GKB().ChangeKey(Key, Value);
        }

        public void SetKey(string Key, float Value)
        {
            GKB().SetKey(Key, Value);
        }

        public MarkInfo GetInfo()
        {
            if (!Info)
                Info = GetComponent<MarkInfo>();
            return Info;
        }

        public string GetName()
        {
            if (!GetInfo())
                return "";
            return GetInfo().GetName();
        }

        public string GetID()
        {
            if (!GetInfo())
                return "";
            return GetInfo().GetID();
        }

        public virtual string GetDescription()
        {
            if (!GetInfo())
                return "";
            return ProcessDescription(GetInfo().GetDescription());
        }

        public virtual Sprite GetIcon()
        {
            if (!GetInfo())
                return null;
            return GetInfo().GetIcon();
        }

        public string ProcessDescription(string Value)
        {
            string Cici = "";
            string S = Value;
            while (S.IndexOf("*") != -1)
            {
                Cici += S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (S.IndexOf("*") == -1)
                    break;
                string Key = S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (Key == "CoolDown" && (!HasKey("CoolDown") || GetKey("CCD") <= 0))
                    Cici += "[Cool Down: " + GetKey("CoolDown") + " turns]";
                else if (Key == "CoolDown" && GetKey("CCD") > 0)
                    Cici += "[Cool Down: " + GetKey("CCD") + "/" + GetKey("CoolDown") + " turns]";
                else if (Key == "Count")
                    Cici += "[Remaining Usage: " + GetKey("Count") + "]";
                else if (Key == "Upgrade")
                    Cici += "[Upgrade after " + GetKey("Upgrade") + " Usage]";
                else if (Key == "Duration" && GetKey("Duration") > 1)
                    Cici += "[Duration: " + GetKey("Duration") + " turns]";
                else if (Key == "Duration")
                    Cici += "[Duration: " + GetKey("Duration") + " turn]";
                else if (HasKey(Key))
                    Cici += ((int)GetKey(Key)).ToString();
                else
                    Cici += GetKey(Key);
            }
            Cici += S;
            return Cici;
        }

        public virtual void CommonKeys()
        {

        }
    }
}