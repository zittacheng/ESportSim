using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ConversationRendererGroup : MonoBehaviour {
        public List<ConversationRenderer> Renderers;
        public GameObject Pivot;
        public float CurrentHeight;
        public float MaxHeight;
        public float Sensitivity;
        public float EndHeight;
        public bool MouseOn;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetMaxHeight();
            HeightUpdate();
        }

        public void GetMaxHeight()
        {
            for (int i = 0; i < Renderers.Count; i++)
            {
                if (!Renderers[i].GetTarget())
                {
                    if (i > 0)
                        MaxHeight = -Renderers[i - 1].transform.localPosition.y + 2.5f - EndHeight;
                    else
                        MaxHeight = 0 - EndHeight;
                    return;
                }
            }
            MaxHeight = -Renderers[Renderers.Count - 1].transform.localPosition.y + 2.5f - EndHeight;
        }

        public void HeightUpdate()
        {
            if (MouseOn)
            {
                float Change = Input.GetAxisRaw("Mouse ScrollWheel") * Sensitivity;
                CurrentHeight -= Change;
            }

            if (CurrentHeight <= 0)
                CurrentHeight = 0;
            else if (CurrentHeight > MaxHeight)
            {
                CurrentHeight = MaxHeight;
                if (CurrentHeight <= 0)
                    CurrentHeight = 0;
            }

            Pivot.transform.localPosition = new Vector3(Pivot.transform.localPosition.x, CurrentHeight, Pivot.transform.localPosition.z);
        }
    }
}