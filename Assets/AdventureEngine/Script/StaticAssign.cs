using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class StaticAssign : MonoBehaviour {
        public static StaticAssign Main;
        public CombatControl CC;
        public UIControl UC;
        public MapControl MPC;
        public Cursor MainCursor;
        [Space]
        public GameObject EffectLine;
        public string SceneName;

        public void Awake()
        {
            Main = this;
            if (!CombatControl.Main)
                CombatControl.Main = CC;
            else if (CC)
                Destroy(CC.gameObject);

            if (!UIControl.Main)
                UIControl.Main = UC;
            else if (UC)
                Destroy(UC.gameObject);

            if (!MapControl.Main)
                MapControl.Main = MPC;
            else if (MPC)
                Destroy(MPC.gameObject);

            if (!Cursor.Main)
                Cursor.Main = MainCursor;
            else if (MainCursor)
                Destroy(MainCursor.gameObject);
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