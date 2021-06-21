using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Temp_AudioListenerFix : MonoBehaviour {
        public AudioListener AL;

        public void Awake()
        {
            if (FindObjectsOfType<AudioListener>().Length > 1)
                Destroy(AL);
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