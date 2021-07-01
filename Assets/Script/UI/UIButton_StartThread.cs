using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class UIButton_StartThread : UIButton_Square {
        public GameObject ActiveBase;
        public GameObject EmptyBase;

        public override void Update()
        {
            if (ThreadControl.Main.CanStartProcess())
            {
                ActiveBase.SetActive(true);
                EmptyBase.SetActive(false);
            }
            else
            {
                ActiveBase.SetActive(false);
                EmptyBase.SetActive(true);
            }
            base.Update();
        }

        public override void MouseDownEffect()
        {
            if (!ThreadControl.Main.CanStartProcess())
                return;
            SubUIControl.Main.CloseWindow();
            ThreadControl.Main.StartProcess();
            base.MouseDownEffect();
        }
    }
}