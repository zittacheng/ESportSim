using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class ShopSwitch : MonoBehaviour {
        public ShopSwitchPivot Pivot;
        public Animator Anim;
        public float Length;
        public float Speed;
        public bool Active;
        [HideInInspector] public float OriPosition;
        [HideInInspector] public float TargetPosition;
        [HideInInspector] public bool Moving;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Moving)
            {
                if (Mathf.Abs(TargetPosition - OriPosition) <= 0.01f)
                    Moving = false;
                else
                {
                    OriPosition = Mathf.Lerp(OriPosition, TargetPosition, Speed * Time.deltaTime);
                    transform.localPosition = new Vector3(transform.localPosition.x, OriPosition, transform.localPosition.z);
                }
            }

            Anim.SetBool("Active", Active);
        }

        public void SetPosition(float Value)
        {
            /*if (Mathf.Abs(Value - transform.localPosition.y) <= 0.1f)
                return;*/
            /*OriPosition = transform.localPosition.y;
            TargetPosition = Value;
            Moving = true;*/
            transform.localPosition = new Vector3(transform.localPosition.x, Value, transform.localPosition.z);
        }

        public void Interact()
        {
            Pivot.OnInteract(this);
        }
    }
}