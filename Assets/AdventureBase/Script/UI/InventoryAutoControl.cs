using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class InventoryAutoControl : MonoBehaviour {
        public Animator Anim;
        public bool LastState;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (CombatControl.Main.Waiting && !LastState)
            {
                LastState = true;
                Anim.SetBool("ItemBaseActive", true);
            }
            if (!CombatControl.Main.Waiting && LastState)
            {
                LastState = false;
                Anim.SetBool("ItemBaseActive", false);
            }
        }
    }
}