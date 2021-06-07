using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class StaticAssign : MonoBehaviour {
        public GlobalControl GC;
        public ThreadControl TC;
        public UIControl UIC;
        public Cursor MainCursor;
        public KeyBase MainKey;

        public void Awake()
        {
            GlobalControl.Main = GC;
            ThreadControl.Main = TC;
            UIControl.Main = UIC;
            Cursor.Main = MainCursor;
            KeyBase.Main = MainKey;
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