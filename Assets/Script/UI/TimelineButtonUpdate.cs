using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ADV;

namespace ESP
{
    public class TimelineButtonUpdate : MonoBehaviour {
        public GameObject BaseObject;
        public int Index = -1;
        public float UnitLength;
        public UIPanel Panel;
        public UIButton_Square Button;
        public TextMeshPro TEXT;
        public float TextLength;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!ThreadControl.Main.GetEvent(Index))
            {
                transform.position = new Vector3(999, transform.position.y, transform.position.z);
                return;
            }
            float LastTime = ThreadControl.Main.GetTimeCost(Index);
            float NextTime = ThreadControl.Main.GetTimeCost(Index + 1);
            float Left = BaseObject.transform.position.x + (UnitLength * LastTime);
            float Right = BaseObject.transform.position.x + (UnitLength * NextTime);
            transform.position = new Vector3(Left + (Right - Left) * 0.5f, transform.position.y, transform.position.z);

            float Up = transform.position.y + 5;
            float Down = transform.position.y - 5;
            float Center = transform.position.x;
            Panel.Render(Center - UnitLength * 0.5f * (NextTime - LastTime), Center + UnitLength * 0.5f * (NextTime - LastTime), Up, Down);
            Button.CursorRangeX = new Vector2(-UnitLength * 0.5f * (NextTime - LastTime), UnitLength * 0.5f * (NextTime - LastTime));
            TEXT.rectTransform.sizeDelta = new Vector2(TextLength * (NextTime - LastTime), 4);
        }
    }
}