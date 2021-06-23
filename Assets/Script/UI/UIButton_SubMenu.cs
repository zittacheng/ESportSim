using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class UIButton_SubMenu : UIButton_Square {
        public UIWindow TargetWindow;

        public override void MouseDownEffect()
        {
            if (!CanInteract())
                return;
            if (SubUIControl.Main.GetCurrentWindow() != TargetWindow)
            {
                SubUIControl.Main.CloseWindow();
                SubUIControl.Main.ActiveWindow(TargetWindow);
            }
            else
                SubUIControl.Main.CloseWindow();
            base.MouseDownEffect();
        }

        public bool CanInteract()
        {
            return !ThreadControl.Main.Active;
        }
    }
}