using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class UIWindow_SubGroup : UIWindow {
        public List<GameObject> Groups;
        public GameObject DefaultGroup;
        public Vector2 ActivePosition;
        public Vector2 EmptyPosition;

        public void SwitchGroup(GameObject Group)
        {
            foreach (GameObject G in Groups)
            {
                if (G != Group)
                    G.transform.localPosition = new Vector3(EmptyPosition.x, EmptyPosition.y, G.transform.localPosition.z);
                else
                    G.transform.localPosition = new Vector3(ActivePosition.x, ActivePosition.y, G.transform.localPosition.z);
            }
        }

        public override void OnOpen()
        {
            SwitchGroup(DefaultGroup);
            base.OnOpen();
        }
    }
}