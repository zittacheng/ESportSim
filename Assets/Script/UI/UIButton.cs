using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class UIButton : MonoBehaviour {
        public float Layer = -1;
        public List<ButtonEffect> Effects;

        public void Awake()
        {
            GlobalControl.Main.AddButton(this);
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

        public void Effect()
        {
            foreach (ButtonEffect BE in Effects)
                BE.Effect();
        }
    }
}