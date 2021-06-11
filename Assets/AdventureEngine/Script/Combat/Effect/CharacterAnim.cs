using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CharacterAnim : MonoBehaviour {
        public Animator Anim;
        public GameObject Pivot;
        public float RotationSpeed;
        [HideInInspector] public Vector2 TargetDirection;
        public bool IgnorePositionChange;
        [HideInInspector] public SpriteRenderer CERenderer;
        [HideInInspector] public AnimationCurve CECurve;
        [HideInInspector] public float CurrentCETime;
        [HideInInspector] public float MaxCETime;
        [HideInInspector] public GameObject Selection;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RotationUpdate();
            ColorEffectUpdate();
        }

        public void OnSelect()
        {
            if (Selection)
                Selection.SetActive(true);
        }

        public void OnDeselect()
        {
            if (Selection)
                Selection.SetActive(false);
        }

        public void SetPosition(Vector2 Value)
        {
            if (IgnorePositionChange)
                return;
            transform.position = new Vector3(Value.x, Value.y, transform.position.z);
        }

        public void SetDirection(Vector2 Value)
        {
            TargetDirection = Value;
        }

        public void RotationUpdate()
        {
            if (TargetDirection.x == 0 && TargetDirection.y == 0)
                TargetDirection = Pivot.transform.up;
            float OriAngle = AbsoluteAngle(-Pivot.transform.eulerAngles.z);
            float TargetAngle = AbsoluteAngle(DirectionToAngle(TargetDirection));
            int d = GetRotationDirection(OriAngle, TargetAngle, out float v);
            if (v <= RotationSpeed * Time.deltaTime)
                Pivot.transform.up = TargetDirection;
            else
                Pivot.transform.eulerAngles = new Vector3(0, 0, -(OriAngle + d * RotationSpeed * Time.deltaTime));
        }

        public static float DirectionToAngle(Vector2 Direction)
        {
            if (Direction.x == 0f && Direction.y >= 0f)
                return 0f;
            else if (Direction.x > 0f && Direction.y == 0f)
                return 90f;
            else if (Direction.x == 0f && Direction.y < 0f)
                return 180f;
            else if (Direction.x < 0f && Direction.y == 0f)
                return 270f;

            if (Direction.x > 0 && Direction.y > 0)
                return Mathf.Atan(Direction.x / Direction.y) * Mathf.Rad2Deg;
            else if (Direction.x > 0 && Direction.y < 0)
                return Mathf.Atan(-Direction.y / Direction.x) * Mathf.Rad2Deg + 90f;
            else if (Direction.x < 0 && Direction.y < 0)
                return Mathf.Atan(-Direction.x / -Direction.y) * Mathf.Rad2Deg + 180f;
            else if (Direction.x < 0 && Direction.y > 0)
                return Mathf.Atan(Direction.y / -Direction.x) * Mathf.Rad2Deg + 270f;
            return 0;
        }

        public static Vector2 AngleToDirection(float Angle)
        {
            Angle = AbsoluteAngle(Angle);
            
            if (Angle == 0f)
                return new Vector2(0, 1);
            else if (Angle == 90f)
                return new Vector2(1, 0);
            else if (Angle == 180f)
                return new Vector2(0, -1);
            else if (Angle == 270f)
                return new Vector2(-1, 0);

            if (Angle < 90f)
                return new Vector2(Mathf.Sin(Angle * Mathf.Deg2Rad), Mathf.Cos(Angle * Mathf.Deg2Rad));
            else if (Angle < 180f)
                return new Vector2(Mathf.Cos((Angle - 90f) * Mathf.Deg2Rad), -Mathf.Sin((Angle - 90f) * Mathf.Deg2Rad));
            else if (Angle < 270f)
                return new Vector2(-Mathf.Sin((Angle - 180f) * Mathf.Deg2Rad), -Mathf.Cos((Angle - 180f) * Mathf.Deg2Rad));
            else if (Angle < 360f)
                return new Vector2(-Mathf.Cos((Angle - 270f) * Mathf.Deg2Rad), Mathf.Sin((Angle - 270f) * Mathf.Deg2Rad));
            return new Vector2(0, 1);
        }

        public static float AbsoluteAngle(float Ori)
        {
            if (Ori < 0)
                return Ori + 360f;
            else if (Ori >= 360f)
                return Ori - 360f;
            else
                return Ori;
        }

        public static int GetRotationDirection(float Ori, float Target, out float Distance)
        {
            if (Target == Ori)
            {
                Distance = 0;
                return 1;
            }
            else if (Target > Ori)
            {
                float a = Target - Ori;
                float b = Ori + 360 - Target;
                if (a <= b)
                {
                    Distance = a;
                    return 1;
                }
                else
                {
                    Distance = b;
                    return -1;
                }
            }
            else
            {
                float a = Ori - Target;
                float b = 360 - Ori + Target;
                if (a <= b)
                {
                    Distance = a;
                    return -1;
                }
                else
                {
                    Distance = b;
                    return 1;
                }
            }
        }

        public void ColorEffectUpdate()
        {
            if (!CERenderer || MaxCETime < 0)
                return;
            CurrentCETime += Time.deltaTime;
            if (MaxCETime > 0 && CurrentCETime >= MaxCETime)
            {
                StopColorEffect();
                return;
            }
            float a = CurrentCETime / MaxCETime;
            if (a < 0)
                a = 0;
            else if (a > 1)
                a = 1;
            CERenderer.color = new Color(CERenderer.color.r, CERenderer.color.g, CERenderer.color.b, CECurve.Evaluate(a));
        }

        public void ColorEffect(Color C)
        {
            if (!CERenderer)
                return;
            if (MaxCETime > 0)
                StopColorEffect();
            CurrentCETime = 0;
            MaxCETime = 0.5f;
            CERenderer.color = new Color(C.r, C.g, C.b, 1);
        }

        public void StopColorEffect()
        {
            CERenderer.color = new Color(CERenderer.color.r, CERenderer.color.g, CERenderer.color.b, 0);
            CurrentCETime = 0;
            MaxCETime = -1;
        }

        public void Death()
        {
            Anim.SetTrigger("Death");
        }

        public void Revive()
        {
            Anim.SetTrigger("Revive");
        }

        public void SetTrigger(string Key)
        {
            Anim.SetTrigger(Key);
        }
    }
}