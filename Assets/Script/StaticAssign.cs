using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class StaticAssign : MonoBehaviour {
        public GlobalControl GC;
        public ThreadControl TC;
        public LevelControl LC;
        public ConversationControl CVC;
        public ConversationContentRenderer CVCR;
        public DialogueControl DC;
        public UIControl UIC;
        public SubUIControl SUIC;
        public ButtonControl BC;
        public ADV.Cursor MainCursor;
        public KeyBase MainKey;

        public void Awake()
        {
            GlobalControl.Main = GC;
            LevelControl.Main = LC;
            UIControl.Main = UIC;
            SubUIControl.Main = SUIC;
            ButtonControl.Main = BC;
            ADV.Cursor.Main = MainCursor;
            ConversationContentRenderer.Main = CVCR;

            if (!ThreadControl.Main)
            {
                ThreadControl.Main = TC;
                DontDestroyOnLoad(TC.gameObject);
            }
            else
                Destroy(TC);

            if (!ConversationControl.Main)
            {
                ConversationControl.Main = CVC;
                DontDestroyOnLoad(CVC.gameObject);
            }
            else
                Destroy(CVC.gameObject);

            if (!DialogueControl.Main)
            {
                DialogueControl.Main = DC;
                DontDestroyOnLoad(DC.gameObject);
            }
            else
                Destroy(DC.gameObject);

            if (!KeyBase.Main)
            {
                KeyBase.Main = MainKey;
                DontDestroyOnLoad(MainKey.gameObject);
            }
            else
                Destroy(MainKey);
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