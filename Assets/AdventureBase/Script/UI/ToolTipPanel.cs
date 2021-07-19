using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ESP;

namespace ADV
{
    public class ToolTipPanel : MonoBehaviour {
        public float Length;
        public Vector2 RenderHeight;
        public UIPanel Panel;
        public TextMeshPro Text;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Apply()
        {
            float Left = Text.textBounds.min.x;
            float Right = Text.textBounds.max.x;
            float Down = Text.textBounds.min.y;
            float Up = Text.textBounds.max.y;
            print(Up + "     " + Down);
            Vector2 Center = new Vector2(0, Down + (Up - Down) * 0.5f + Text.transform.localPosition.y);
            Panel.transform.localPosition = new Vector3(0, Text.transform.localPosition.y);
            Panel.Render_Local(Center.x - Length, Center.x + Length, Up + RenderHeight.y, Down - RenderHeight.x);
        }
    }
}