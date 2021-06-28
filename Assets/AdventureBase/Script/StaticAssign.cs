using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class StaticAssign : MonoBehaviour {
        public static StaticAssign Main;
        public CombatControl CC;
        public UIControl UC;
        public PathControl MPC;
        public UndoControl UDC;
        public ButtonControl BC;
        public Cursor MainCursor;
        public KeyBase MainKey;
        [Space]
        public GameObject EffectLine;
        public string SceneName;

        public void Awake()
        {
            Main = this;
            if (!CombatControl.Main)
                CombatControl.Main = CC;
            else if (CC)
                Destroy(CC);

            if (!UIControl.Main)
                UIControl.Main = UC;
            else if (UC)
                Destroy(UC);

            if (!PathControl.Main)
                PathControl.Main = MPC;
            else if (MPC)
                Destroy(MPC.gameObject);

            if (!Cursor.Main)
                Cursor.Main = MainCursor;
            else if (MainCursor)
                Destroy(MainCursor.gameObject);

            if (!UndoControl.Main)
                UndoControl.Main = UDC;
            else if (UDC)
                Destroy(UDC);

            if (!ButtonControl.Main)
                ButtonControl.Main = BC;
            else if (BC)
                Destroy(BC);

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

        public void Retry()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneName);
        }

        public static StaticAssign GetMain()
        {
            return Camera.main.GetComponent<StaticAssign>();
        }
    }
}