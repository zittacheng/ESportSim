using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class InventoryOverride : MonoBehaviour {
        public Card Source;

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual Mark_Status_Inventory GetInventory()
        {
            return null;
        }
    }
}