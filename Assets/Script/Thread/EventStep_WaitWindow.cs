using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class EventStep_WaitWindow : EventStep {
        [TextArea] public string Description;
        public float Delay;
        [HideInInspector] public float CurrentDelay;

        public override void OnEffect()
        {
            UIWindow W = SubUIControl.Main.ActiveWindow("Wait");
            UIWindow_Wait Win = (UIWindow_Wait)W;
            Win.DescriptionText.text = Description;
            CurrentDelay = Delay;
        }

        public override void EffectUpdate(float Value)
        {
            CurrentDelay -= Value;
            if (CurrentDelay <= 0)
            {
                SubUIControl.Main.CloseWindow();
                ThreadControl.Main.NextStep();
            }
        }
    }
}