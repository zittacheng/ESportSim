using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControl : MonoBehaviour {
        public Mark_Skill BaseSkill;

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        public virtual Mark_Skill GetSkill()
        {
            return BaseSkill;
        }
    }
}