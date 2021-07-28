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
        public GameObject ToolTipBase;
        public Vector3 ToolTipPosition_Left;
        public Vector3 ToolTipPosition_Right;
        public List<ToolTipRenderer> ToolTips;

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

        public void Render(Vector3 Pivot, PanelDirection Direction, MarkInfo MInfo)
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

            if (Direction == PanelDirection.Left)
                ToolTipRender(ToolTipDirection.Left, MInfo);
            else if (Center.x >= 62f)
                ToolTipRender(ToolTipDirection.Left, MInfo);
            else
                ToolTipRender(ToolTipDirection.Right, MInfo);
        }

        public void ToolTipRender(ToolTipDirection Direction, MarkInfo MInfo)
        {
            float Height = 0;
            
            foreach (ToolTipRenderer TTR in ToolTips)
            {
                if (!MInfo.ToolTips.Contains(TTR.Key))
                    TTR.gameObject.SetActive(false);
            }

            foreach (string s in MInfo.ToolTips)
            {
                foreach (ToolTipRenderer TTR in ToolTips)
                {
                    if (TTR.Key == s)
                    {
                        TTR.gameObject.SetActive(true);
                        TTR.transform.localPosition = new Vector3(TTR.transform.localPosition.x, -Height, TTR.transform.localPosition.z);
                        Height += TTR.Height;
                    }
                }
            }

            if (Direction == ToolTipDirection.Left)
                ToolTipBase.transform.localPosition = ToolTipPosition_Left;
            else if (Direction == ToolTipDirection.Right)
                ToolTipBase.transform.localPosition = ToolTipPosition_Right;

            transform.parent = null;
        }

        public void Render(GameObject Pivot, PanelDirection Direction, MarkInfo MInfo)
        {
            Render(Pivot.transform.position, Direction, MInfo);
            transform.parent = Pivot.transform;
        }
    }

    public enum PanelDirection
    {
        Left,
        Right,
        Up
    }

    public enum ToolTipDirection
    {
        Left,
        Right
    }
}