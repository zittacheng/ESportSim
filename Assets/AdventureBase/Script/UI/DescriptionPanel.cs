using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESP;
using TMPro;

namespace ADV
{
    public class DescriptionPanel : MonoBehaviour {
        public UIPanel Panel;
        public GameObject AnimBase;
        public TextMeshPro NameText;
        public TextMeshPro DescriptionText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetActive(bool Value)
        {
            AnimBase.SetActive(Value);
        }

        public void Render(Vector2 Pivot, PanelDirection Direction, MarkInfo MInfo)
        {
            if (!MInfo)
            {
                AnimBase.SetActive(false);
                return;
            }

            AnimBase.SetActive(true);
            DescriptionText.text = MInfo.GetDescription();
            DescriptionText.ForceMeshUpdate();
            float Left = DescriptionText.textBounds.min.x;
            float Right = DescriptionText.textBounds.max.x;
            float Down = DescriptionText.textBounds.min.y;
            float Up = DescriptionText.textBounds.max.y;
            float Height = Up - Down + 3;
            float Width = Right - Left + 3;
            Vector2 Center = Pivot;
            if (Direction == PanelDirection.Left)
                Center -= new Vector2(13.5f, 0);
            else if (Direction == PanelDirection.Right)
                Center += new Vector2(13.5f, 0);
            else if (Direction == PanelDirection.Up)
                Center += new Vector2(0, Height * 0.5f + 5);
            transform.position = new Vector3(Center.x, Center.y, transform.position.z);
            NameText.text = MInfo.GetName();
            Panel.Render(Center.x - 8, Center.x + 8, Center.y + Height * 0.5f, Center.y - Height * 0.5f);
        }
    }

    public enum PanelDirection
    {
        Left,
        Right,
        Up
    }
}