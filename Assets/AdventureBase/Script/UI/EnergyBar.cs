using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ADV
{
    public class EnergyBar : MonoBehaviour {
        public GameObject Left;
        public GameObject Main;
        public GameObject Right;
        public Vector2 RightPositionRange;
        [Space]
        public List<SpriteRenderer> SRs;
        public Color MainColor;
        [HideInInspector] public float CurrentValue;

        public void Awake()
        {
            ApplyColor();
        }

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

        }

        public virtual void Render(float Value)
        {
            CurrentValue = Value;
            float a = Value;
            if (a < 0)
                a = 0;
            if (a > 1)
                a = 1;
            if (a <= 0)
            {
                foreach (SpriteRenderer SR in SRs)
                    SR.enabled = false;
            }
            else
            {
                foreach (SpriteRenderer SR in SRs)
                    SR.enabled = true;
            }
            Main.transform.localScale = new Vector3(a, 1, 1);
            Right.transform.localPosition = new Vector3(RightPositionRange.x + (RightPositionRange.y - RightPositionRange.x) * a, 
                Right.transform.localPosition.y, Right.transform.localPosition.z);
        }

        public virtual void ApplyColor()
        {
            foreach (SpriteRenderer SR in SRs)
                SR.color = MainColor;
        }
    }
}