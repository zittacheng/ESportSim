using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESP;

namespace ADV
{
    public class UIButton : MonoBehaviour {
        public List<ButtonEffect> Effects;
        public List<ButtonEffect> UpEffects;
        public List<ButtonEffect> EnterEffects;
        public List<ButtonEffect> ExitEffects;

        public void Awake()
        {
            ButtonControl.Main.AddButton(this);
        }

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        public virtual Vector2 GetPosition()
        {
            return transform.position;
        }

        public virtual bool InRange()
        {
            return false;
        }

        public virtual void MouseDownEffect()
        {
            foreach (ButtonEffect BE in Effects)
                BE.Effect();
        }

        public virtual void MouseUpEffect()
        {
            foreach (ButtonEffect BE in UpEffects)
                BE.Effect();
        }

        public virtual void MouseEnterEffect()
        {
            foreach (ButtonEffect BE in EnterEffects)
                BE.Effect();
        }

        public virtual void MouseExitEffect()
        {
            foreach (ButtonEffect BE in ExitEffects)
                BE.Effect();
        }
    }
}