using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class GlobalControl : MonoBehaviour {
        public static GlobalControl Main;
        [HideInInspector] public List<UIButton> Buttons;

        public void AddButton(UIButton B)
        {
            if (!Buttons.Contains(B))
                Buttons.Add(B);
        }

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

        }
    }
}