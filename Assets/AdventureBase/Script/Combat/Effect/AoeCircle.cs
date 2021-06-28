using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AoeCircle : MonoBehaviour {
        public Vector2 Position;
        public float AddDelay;
        public float Size;
        public float BaseAlpha;
        public Color MainColor;
        [Space]
        public AnimationCurve MaskCurve;
        public AnimationCurve AlphaCurve;
        public float StartDelay;
        public float FadeDelay = 0.5f;
        public float CurrentFadeDelay;
        public float DeathDelay = 5;
        public GameObject AnimBase;
        public GameObject Mask;
        public SpriteRenderer Circle;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (StartDelay > 0)
            {
                StartDelay -= Time.deltaTime;
                SetColor(MainColor, 0);
                return;
            }
            if (CurrentFadeDelay < FadeDelay)
            {
                CurrentFadeDelay += Time.deltaTime;
                float Alpha = AlphaCurve.Evaluate(CurrentFadeDelay / FadeDelay) * BaseAlpha;
                SetColor(MainColor, Alpha);
                float MaskSize = MaskCurve.Evaluate(CurrentFadeDelay / FadeDelay);
                Mask.transform.localScale = new Vector3(MaskSize, MaskSize, 1);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime);
            }
        }

        public void Ini()
        {
            transform.position = new Vector3(Position.x, Position.y, transform.position.z);
            AnimBase.transform.localScale = new Vector3(Size, Size, 1);
            BaseAlpha = MainColor.a;
            SetColor(MainColor, 0);
            Destroy(gameObject, DeathDelay);
            StartDelay += AddDelay;
        }

        public void SetColor(Color C, float Alpha)
        {
            Color c = new Color(C.r, C.g, C.b, Alpha);
            SetColor(c);
        }

        public void SetColor(Color C)
        {
            Circle.color = C;
        }
    }
}