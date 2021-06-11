using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class ComboUnit : MonoBehaviour {
        public List<GameObject> SkillPrefabs;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Mark_Skill GetSkill(int Index)
        {
            if (Index < SkillPrefabs.Count)
                return SkillPrefabs[Index].GetComponent<Mark_Skill>();
            return null;
        }
    }
}