using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class UIControl : MonoBehaviour {
        public static UIControl Main;
        public Vector2 WindowPosition;
        public UIWindow CurrentWindow;
        [Space]
        public UIWindow W_Schedule;
        public UIWindow_Result W_Result;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public UIWindow GetCurrentWindow()
        {
            return CurrentWindow;
        }

        public float GetCurrentLayer()
        {
            if (!GetCurrentWindow())
                return 0;
            return GetCurrentWindow().Layer;
        }

        public UIWindow ActiveWindow(UIWindow W)
        {
            W.transform.position = new Vector3(WindowPosition.x, WindowPosition.y, W.transform.position.z);
            CurrentWindow = W;
            return W;
        }

        public UIWindow ActiveWindow(string Key)
        {
            if (Key == "Result")
                return ActiveWindow(W_Result);
            return null;
        }

        public void CloseWindow()
        {
            CurrentWindow.transform.position = new Vector3(0, -100, CurrentWindow.transform.position.z);
        }
    }
}