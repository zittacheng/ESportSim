using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class SubCharacterAnim : MonoBehaviour {
        public Card Source;
        public GameObject AnimBase;
        public string Key;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            AnimBase.SetActive(Source.GetInfo().GetID() == Key);
        }
    }
}