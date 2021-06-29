using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ValueBase : MonoBehaviour {

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

        }

        public static float GetLevel(float Exp)
        {
            if (Exp < 20)
                return 1;
            else if (Exp < 60)
                return 2;
            else if (Exp < 120)
                return 3;
            else if (Exp < 200)
                return 4;
            else if (Exp < 350)
                return 5;
            else if (Exp < 530)
                return 6;
            else if (Exp < 740)
                return 7;
            else if (Exp < 980)
                return 8;
            else if (Exp < 1340)
                return 9;
            else if (Exp < 1740)
                return 10;
            else if (Exp < 2180)
                return 11;
            else
                return 12;
        }

        public static float GetMaxExp(float Level)
        {
            return 99999f;
        }

        public static float GetHeroLevelGain(float Level)
        {
            return 4 + Level * 2;
        }

        public static float GetDamageGain(float Level)
        {
            int TrueLevel = 0;
            for (int i = 0; i <= Level; i++)
                TrueLevel += i;
            return TrueLevel * 0.0012f;
        }

        public static float GetLifeGain(float Level)
        {
            int TrueLevel = 0;
            for (int i = 0; i <= Level; i++)
                TrueLevel += i;
            return TrueLevel * 0.01f;
        }

        public static float GetCoinGain(float Level, int Turn)
        {
            float l = Level;
            return (1 + (1 + 0.1f * l) * Turn) * 10;
        }

        public static float GetEnemyCoinLevel(float HeroLevel)
        {
            if (HeroLevel < 3)
                return 0;
            if (HeroLevel < 7.9f)
                return 1;
            if (HeroLevel < 19)
                return 2;
            if (HeroLevel < 25)
                return 3;
            if (HeroLevel < 33)
                return 4;
            if (HeroLevel < 50)
                return 5;
            return 6;
        }
    }
}