using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class EventStep_ResultWindow : EventStep {
        [TextArea] public string Description;
        [TextArea] public string Result;

        public override void OnEffect()
        {
            UIWindow W = SubUIControl.Main.ActiveWindow("Result");
            UIWindow_Result R = (UIWindow_Result)W;
            R.DescriptionText.text = Description;
            R.ResultText.text = Result;
        }
    }
}