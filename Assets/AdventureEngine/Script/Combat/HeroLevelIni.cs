using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESP;

namespace ADV
{
    public class HeroLevelIni : MonoBehaviour {
        public Card Source;

        public void Awake()
        {
            float Level = KeyBase.Main.GetKey(Source.GetInfo().GetID() + "Level");
            Source.ChangeBaseDamage(ValueBase.GetDamageGain(Level));
            Source.ChangeMaxLife(ValueBase.GetLifeGain(Level));
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}