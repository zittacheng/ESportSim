using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class Temp_TimelinePivot : MonoBehaviour {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            int a = ThreadControl.Main.Thread.Count - 1;
            transform.localPosition = new Vector3(a * -7.5f, transform.localPosition.y, transform.localPosition.z);
        }
    }
}