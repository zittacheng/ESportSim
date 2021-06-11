using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class InventoryOverride_SpawnControl : InventoryOverride {

        public override Mark_Status_Inventory GetInventory()
        {
            return null;
            //return CombatControl.Main.SpawnControl.GetInventory();
        }
    }
}