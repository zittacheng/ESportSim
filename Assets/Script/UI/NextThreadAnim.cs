using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class NextThreadAnim : MonoBehaviour {
        public List<EventRenderer> Renderers;
        public GameObject AnimBase;
        public float Speed;
        public float Target;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < Renderers.Count; i++)
            {
                if (!Renderers[i].GetTarget())
                {
                    Target = Renderers[i].transform.localPosition.y;
                    AnimBase.SetActive(true);
                    break;
                }
                else if (i == Renderers.Count - 1)
                    AnimBase.SetActive(false);
            }

            if (Target >= transform.localPosition.y)
            {
                float y = transform.localPosition.y + Speed * Time.deltaTime;
                if (y > Target)
                    y = Target;
                transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
            }
            else if (Target <= transform.localPosition.y)
            {
                float y = transform.localPosition.y - Speed * Time.deltaTime;
                if (y < Target)
                    y = Target;
                transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
            }
        }
    }
}