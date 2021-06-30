using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNF
{
    public class SentenceGroup : MonoBehaviour {
        public float OverrideSpeed = -1;
        public List<Sentence> Sentences;

        public void Awake()
        {
            foreach (Sentence S in GetComponentsInChildren<Sentence>())
                Sentences.Add(S);
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