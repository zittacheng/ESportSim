using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class ShopSwitchPivot : MonoBehaviour {
        public List<ShopSwitch> Switches;
        public float StartDistance;
        public float ActiveDistance;
        public float EmptyDistance;
        public float CommonDistance;

        // Start is called before the first frame update
        void Start()
        {
            UpdatePosition();
        }

        // Update is called once per frame
        void Update()
        {
            UpdatePosition();
        }

        public void OnInteract(ShopSwitch Target)
        {
            foreach (ShopSwitch SS in Switches)
            {
                if (SS != Target)
                    SS.Active = false;
                else
                    SS.Active = true;
            }
        }

        public void UpdatePosition()
        {
            float d = -StartDistance;
            for (int i = 0; i < Switches.Count; i++)
            {
                d -= Switches[i].Length;
                Switches[i].SetPosition(d);
                d -= Switches[i].Length;
                d -= CommonDistance;
            }
        }
    }
}