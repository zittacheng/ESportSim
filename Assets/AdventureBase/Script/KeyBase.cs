using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class KeyBase : MonoBehaviour {
        public static KeyBase Main;
        public List<string> Keys;

        public void Ini()
        {
            Keys = new List<string>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddKey(string Key)
        {
            Keys.Add(Key + "[0");
        }

        public void AddKeys(KeyBase KB)
        {
            for (int i = 0; i < KB.Keys.Count; i++)
            {
                ChangeKey(Translate(KB.Keys[i], out float V), V);
            }
        }

        public bool HasKey(string Key)
        {
            foreach (string s in Keys)
            {
                if (Translate(s) == Key)
                    return true;
            }
            return false;
        }

        public float GetKey(string Key)
        {
            foreach (string s in Keys)
            {
                if (Translate(s, out float V) == Key)
                    return V;
            }
            return 0;
        }

        public float ChangeKey(string Key, float Value)
        {
            if (!HasKey(Key))
                AddKey(Key);
            float a = GetKey(Key);
            for (int i = 0; i < Keys.Count; i++)
            {
                if (Translate(Keys[i]) == Key)
                    Keys[i] = Key + "[" + (a + Value);
            }
            return GetKey(Key);
        }

        public void SetKey(string Key, float Value)
        {
            ChangeKey(Key, Value - GetKey(Key));
        }

        public static string Translate(string OriKey, out float Value)
        {
            Value = float.Parse(OriKey.Substring(OriKey.IndexOf("[") + 1));
            return OriKey.Substring(0, OriKey.IndexOf("["));
        }

        public static string Translate(string OriKey)
        {
            return Translate(OriKey, out float V);
        }

        public static string Compose(string Key, float Value)
        {
            return Key + "[" + Value;
        }

        public void CommonKeys()
        {
            // "Time": (Legacy) Max time for each turn
            // "Coin": Current coin
            // "Energy": Current energy
            // "MaxEnergy": Max energy for each turn
            
            // "StreamPoint": (Legacy) Current stream ability point
            // "GamePoint": (Legacy) Current gameplay ability point
            // "Level": Current level
            // "Exp": Current exp

            // "[Hero]Level": Current level for the hero
            // "[Character]Value": Current SAT value for the character
            // "[Upgrade]Unlocked": Whether the upgrade is unlocked
        }
    }
}