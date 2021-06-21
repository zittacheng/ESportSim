using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESP;

namespace ADV
{
    public class Cursor : MonoBehaviour {
        public static Cursor Main;
        public Vector2 Position;
        public List<UIButton> SelectingButtons;

        public void Awake()
        {
            Main = this;
        }

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
            if (Input.GetMouseButtonUp(0))
                UnInteract();
        }

        public void Interact()
        {
            for (int i = SelectingButtons.Count - 1; i >= 0; i--)
                SelectingButtons[i].MouseDownEffect();
        }

        public void UnInteract()
        {
            for (int i = SelectingButtons.Count - 1; i >= 0; i--)
                SelectingButtons[i].MouseUpEffect();
        }

        public void PositionUpdate()
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Position = new Vector2(a.x, a.y);
            transform.position = new Vector3(a.x, a.y, transform.position.z);
        }

        public void SelectionUpdate()
        {
            for (int i = SelectingButtons.Count - 1; i >= 0; i--)
            {
                if (!SelectingButtons[i].InRange())
                {
                    SelectingButtons[i].MouseExitEffect();
                    SelectingButtons.RemoveAt(i);
                }
            }
            for (int i = ButtonControl.Main.Buttons.Count - 1; i >= 0; i--)
            {
                UIButton B = ButtonControl.Main.Buttons[i];
                if (!SelectingButtons.Contains(B) && B.InRange())
                {
                    B.MouseEnterEffect();
                    SelectingButtons.Add(B);
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