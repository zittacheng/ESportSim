using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class SubUIControl : MonoBehaviour {
        public static SubUIControl Main;
        public Vector2 WindowPosition;
        public UIWindow CurrentWindow;
        [Space]
        public UIWindow W_Day;
        public UIWindow W_Night;
        public UIWindow_Wait W_Wait;
        public UIWindow_Result W_Result;
        public UIWindow_Result W_HeroResult;
        public UIWindow_Result W_GameResult;
        [Space]
        public Animator UIMask;
        public Location CurrentLocation;
        public bool InTransit;

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
            if (InTransit)
                return W;
            W.transform.position = new Vector3(WindowPosition.x, WindowPosition.y, W.transform.position.z);
            CurrentWindow = W;
            W.OnOpen();
            return W;
        }

        public UIWindow ActiveWindow(string Key)
        {
            if (Key == "Day")
                return ActiveWindow(W_Day);
            else if (Key == "Night")
                return ActiveWindow(W_Night);
            else if (Key == "Wait")
                return ActiveWindow(W_Wait);
            else if (Key == "Result")
                return ActiveWindow(W_Result);
            else if (Key == "HeroResult")
                return ActiveWindow(W_HeroResult);
            else if (Key == "GameResult")
                return ActiveWindow(W_GameResult);
            return null;
        }

        public void CloseWindow()
        {
            if (!CurrentWindow || InTransit)
                return;
            CurrentWindow.transform.position = new Vector3(0, -100, CurrentWindow.transform.position.z);
            CurrentWindow = null;
        }

        public void MoveToShp()
        {
            if (InTransit || CurrentLocation == Location.Shop)
                return;
            CurrentLocation = Location.Shop;
            InTransit = true;
            StartCoroutine("MoveToShopIE");
        }

        public IEnumerator MoveToShopIE()
        {
            UIMask.SetBool("Active", true);
            yield return new WaitForSeconds(0.52f);
            Camera.main.transform.position = new Vector3(-300, 0, -10);
            yield return new WaitForSeconds(0.2f);
            UIMask.SetBool("Active", false);
            InTransit = false;
        }

        public void MoveBack()
        {
            if (InTransit || CurrentLocation == Location.Common)
                return;
            CurrentLocation = Location.Common;
            InTransit = true;
            StartCoroutine("MoveBackIE");
        }

        public IEnumerator MoveBackIE()
        {
            UIMask.SetBool("Active", true);
            yield return new WaitForSeconds(0.52f);
            Camera.main.transform.position = new Vector3(0, 0, -10);
            yield return new WaitForSeconds(0.2f);
            UIMask.SetBool("Active", false);
            InTransit = false;
        }
    }

    public enum Location
    {
        Common,
        Shop
    }
}