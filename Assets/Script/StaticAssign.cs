using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class StaticAssign : MonoBehaviour {
        public GlobalControl GC;
        public ThreadControl TC;
        public UIControl UIC;
        public SubUIControl SUIC;
        public ADV.Cursor MainCursor;
        public KeyBase MainKey;

        public void Awake()
        {
            GlobalControl.Main = GC;
            ThreadControl.Main = TC;
            UIControl.Main = UIC;
            SubUIControl.Main = SUIC;
            ADV.Cursor.Main = MainCursor;
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