using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ESP
{
    public class SentenceRenderer : MonoBehaviour {
        public UIPanel Panel;
        public TextMeshPro Text;
        public bool RightAlign;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Render(string Content, float PositionY, out float NextPositionY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, PositionY, transform.localPosition.z);

            Text.text = Content;
            Text.ForceMeshUpdate();

            float Width = Text.textBounds.extents.x * 2f;
            if (RightAlign)
                Text.transform.localPosition = new Vector3(-Width, Text.transform.localPosition.y, Text.transform.localPosition.z);

            float Left = Text.textBounds.min.x + Text.transform.localPosition.x + transform.position.x - 0.4f;
            float Right = Text.textBounds.max.x + Text.transform.localPosition.x + transform.position.x + 0.4f;
            float Down = Text.textBounds.min.y + Text.transform.localPosition.y + transform.position.y;
            float Up = Text.textBounds.max.y + Text.transform.localPosition.y + transform.position.y;
            float Height = Up - Down;

            Panel.Render(Left, Right, Up, Down);
            NextPositionY = PositionY - Height;
        }
    }
}