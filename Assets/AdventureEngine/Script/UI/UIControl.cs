using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIControl : MonoBehaviour {
        public static UIControl Main;
        public DescriptionPanel ItemPanel;
        [HideInInspector] public List<UIButton> Buttons;
        public List<Vector2> FriendlySlotPositions;
        public List<Vector2> EnemySlotPositions;

        public void AddButton(UIButton B)
        {
            if (!Buttons.Contains(B))
                Buttons.Add(B);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Vector2 GetFriendlySlotPosition(int Index)
        {
            return FriendlySlotPositions[Index];
        }

        public Vector2 GetEnemySlotPosition(int Index)
        {
            return EnemySlotPositions[Index];
        }
    }
}