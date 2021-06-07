using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class Cursor : MonoBehaviour {
        public static Cursor Main;
        public Vector2 Position;
        public UIButton SelectingButton;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            PositionUpdate();
            SelectionUpdate();
            if (Input.GetMouseButtonDown(0))
                Interact();
        }

        public void Interact()
        {
            if (SelectingButton)
                SelectingButton.Effect();
        }

        public void PositionUpdate()
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Position = new Vector2(a.x, a.y);
            transform.position = new Vector3(a.x, a.y, transform.position.z);
        }

        public void SelectionUpdate()
        {
            SelectingButton = null;
            for (int i = GlobalControl.Main.Buttons.Count - 1; i >= 0; i--)
            {
                UIButton B = GlobalControl.Main.Buttons[i];
                if (UIControl.Main.GetCurrentLayer() != -1 && B.Layer != -1 && UIControl.Main.GetCurrentLayer() != B.Layer)
                    continue;
                if (B.InRange())
                {
                    SelectingButton = B;
                    break;
                }
            }
        }

        public Vector2 GetPosition()
        {
            return new Vector2(Position.x, Position.y);
        }
    }
}