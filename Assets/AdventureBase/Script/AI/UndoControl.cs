using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UndoControl : MonoBehaviour {
        public static UndoControl Main;
        public List<UndoUnit> Units;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Undo()
        {
            if (!CanUndo())
                return;
            Units[Units.Count - 1].Execute();
            DestroyUnit(Units[Units.Count - 1]);
        }

        public bool CanUndo()
        {
            return CombatControl.Main.Waiting && Units.Count > 0;
        }

        public void Clear()
        {
            for (int i = Units.Count -1; i >= 0; i--)
                DestroyUnit(Units[i]);
        }

        public void NewUnit(GameObject AddItem, GameObject RemoveItem, float CoinChange, string SwitchHero)
        {
            UndoUnit U = gameObject.AddComponent<UndoUnit>();
            U.AddItem = AddItem;
            U.RemoveItem = RemoveItem;
            U.CoinChange = CoinChange;
            U.SwitchHero = SwitchHero;
            Units.Add(U);
        }

        public void DestroyUnit(UndoUnit U)
        {
            Units.Remove(U);
            Destroy(U);
        }
    }
}