using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIControl : MonoBehaviour {
        public static UIControl Main;
        [HideInInspector] public List<UIButton> Buttons;

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
    }
}