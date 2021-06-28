using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CameraControl : MonoBehaviour {
        public GameObject Target;
        public float MovementSpeed;
        public float RotationSpeed;
        public bool DelayMode;
        [HideInInspector] public List<GameObject> AttachedObjects;
        [HideInInspector] public List<Vector3> AttachedPositions;

        public void Awake()
        {
            for (int i = 0; i < AttachedObjects.Count; i++)
                AttachedPositions.Add(AttachedObjects[i].transform.position - transform.position);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            PositionUpdate();
            //RotationUpdate();
        }

        public void PositionUpdate()
        {
            if (!Target)
                return;
            Vector2 TP = Target.transform.position;
            float X = TP.x;
            float Y = TP.y;
            if (DelayMode)
                transform.position = Vector3.Lerp(transform.position, new Vector3(X, Y, transform.position.z), MovementSpeed * Time.deltaTime);
            else
                transform.position = new Vector3(X, Y, transform.position.z);

            for (int i = 0; i < AttachedObjects.Count; i++)
                AttachedObjects[i].transform.position = transform.position + AttachedPositions[i];
        }

        public void RotationUpdate()
        {
            if (!Target)
                return;
            if (DelayMode)
                transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, Target.transform.eulerAngles.z, RotationSpeed * Time.deltaTime));
            else
                transform.eulerAngles = new Vector3(0, 0, Target.transform.eulerAngles.z);
        }
    }
}