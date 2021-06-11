using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal : MonoBehaviour {
        public KeyBase KB;
        [HideInInspector] public SignalInfo Info;
        [HideInInspector] public Card Source;
        [HideInInspector] public Card Target;

        public void Awake()
        {
            Info = null;
        }

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        // Modify Marks
        public virtual void InputMark(Mark M)
        {

        }

        public virtual void StartEffect()
        {

        }

        public virtual void EndEffect()
        {
            //OutputMessage();
        }

        // For PassSignal()
        public virtual string ReturnKey(out float Value)
        {
            Value = 0;
            return "Empty";
        }

        // Process AddKeys
        public virtual void IniAddKeys(List<string> Keys)
        {
            foreach (string s in Keys)
                SetKey(KeyBase.Translate(s, out float v), v);
        }

        public void OutputMessage()
        {
            /*if (GetMessage(out string M, out bool SR))
                CombatControl.Main.RecieveMessage(M, SR);*/
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

        public SignalInfo GetInfo()
        {
            if (!Info)
                Info = GetComponent<SignalInfo>();
            return Info;
        }

        public bool GetMessage(out string Message, out bool SubRender)
        {
            if (!GetInfo() || GetInfo().GetMessage() == "")
            {
                Message = "";
                SubRender = false;
                return false;
            }

            Message = ProcessMessage(GetInfo().GetMessage());
            SubRender = GetInfo().SubRender;
            return true;
        }

        public string ProcessMessage(string Value)
        {
            string Cici = "";
            string S = Value;
            while (S.IndexOf("*") != -1/* && S.IndexOf("*") != S.Length - 1*/)
            {
                Cici += S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (S.IndexOf("*") == -1)
                    break;
                string Key = S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (Key == "Source")
                    Cici += "[" + Source.GetName() + "]";
                else if (Key == "Target")
                    Cici += "[" + Target.GetName() + "]";
                else if (Key == "Its")
                    Cici += "Its";
                else if (Key == "its")
                    Cici += "its";
                else if (Key == "Itself")
                    Cici += "Itself";
                else if (Key == "itself")
                    Cici += "itself";
                else if (Key == "(it")
                    Cici += "it";
                else if (Key == "(It")
                    Cici += "It";
                else if (Key == "it")
                    Cici += "it";
                else if (Key == "It")
                    Cici += "It";
                else if (Key == "它")
                    Cici += "它";
                else
                    Cici += GetKey(Key);
            }
            Cici += S;
            return Cici;
        }

        public virtual void CommonKeys()
        {
            // "Target": Whether the signal should be send to the opponent
            // "Position": Which position the signal is targeting
            // "SourcePosition": Which position the signal is targeting related to the source position
            // "AllPosition": Whether the signal is targeting all positions
            // "ReverseSource": Whether the signal should be send from the opponent
            // "IgnoreSource": Whether OutputSignal should be ignored
            // "ItemCount": The count of the source skill (item)
            // "Delay": The delay before the signal is resolved
            // "TargetPositionX": Target position X
            // "TargetPositionY": Target position Y
        }
    }
}