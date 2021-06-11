using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CastIndicator : MonoBehaviour {
        public Mark_Skill Skill;
        public GameObject Anim;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!Skill.Source)
            {
                SetActive(false);
                return;
            }
            Card C = Skill.Source;
            if (C.GetCast() && C.GetCast() == Skill)
            {
                if (C.CastTarget)
                    SetPosition(C.CastTarget.GetPosition());
                else
                    SetPosition(C.CastPosition);
                SetActive(true);
            }
            else
            {
                SetActive(false);
            }
        }

        public void SetActive(bool Value)
        {
            Anim.SetActive(Value);
        }

        public void SetPosition(Vector2 Position)
        {
            Anim.transform.position = new Vector3(Position.x, Position.y, transform.position.z);
        }
    }
}