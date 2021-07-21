using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CharacterAnim : MonoBehaviour {
        public Card Source;
        public Animator Anim;
        public GameObject Pivot;
        public float RotationSpeed;
        [HideInInspector] public Vector2 TargetDirection;
        public bool IgnorePositionChange;
        [Space]
        [HideInInspector] public Vector2 OriPosition;
        [HideInInspector] public Vector2 TargetPosition;
        [HideInInspector] public float TargetDelay;
        [HideInInspector] public float CurrentDelay;
        [HideInInspector] public bool Moving;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!CombatControl.Main.Waiting && CombatControl.Main.HoldingCard == Source)
                MouseUp();
            /*
            if (CombatControl.Main.HoldingCard == Source)
            {
                ForcePosition(Cursor.Main.GetPosition());
                return;
            }
            else if (Moving)
            {
                if ((TargetPosition - OriPosition).magnitude <= 0.01f)
                    Moving = false;
                else
                {
                    CurrentDelay += Time.deltaTime;
                    Vector2 Position = TargetPosition;
                    if (CurrentDelay >= TargetDelay)
                        Moving = false;
                    else
                        Position = OriPosition + (TargetPosition - OriPosition) * (CurrentDelay / TargetDelay);
                    Source.SetPosition(Position);
                }
            }

            if (Source.GetSide() == 0 && Source.GetGroup())
            {
                if (CombatControl.Main.FriendlyCards.Contains(Source))
                {
                    if (CombatControl.Main.HoldingCard != Source)
                        SetMovement(UIControl.Main.GetFriendlySlotPosition(CombatControl.Main.FriendlyCards.IndexOf(Source)), 0.1f);
                }
                else
                    ForcePosition(new Vector2(-300, -300));
            }
            else if (Source.GetSide() == 1 && Source.GetGroup())
            {
                if (CombatControl.Main.EnemyCards.Contains(Source))
                    SetMovement(UIControl.Main.GetEnemySlotPosition(CombatControl.Main.EnemyCards.IndexOf(Source)), 0.1f);
                else
                    ForcePosition(new Vector2(-300, -300));
            }*/
        }

        public void MouseDown()
        {
            if (!CombatControl.Main.Waiting)
                return;
            if (Source.GetSide() != 0 || !CombatControl.Main.FriendlyCards.Contains(Source))
                return;
            CombatControl.Main.HoldingCard = Source;
        }

        public void MouseUp()
        {
            CombatControl.Main.HoldingCard = null;
        }

        public void ForcePosition(Vector2 Position)
        {
            if (Moving)
                Moving = false;
            Source.SetPosition(Position);
        }

        public void SetMovement(Vector2 Target, float Delay)
        {
            /*if (Moving)
                return;*/
            Moving = true;
            TargetPosition = Target;
            CurrentDelay = 0;
            TargetDelay = Delay;
            OriPosition = Source.GetPosition();
        }

        public void SetPosition(Vector2 Value)
        {
            if (IgnorePositionChange)
                return;
            transform.position = new Vector3(Value.x, Value.y, Value.y * 0.05f);
        }

        public void SetDirection(Vector2 Value)
        {
            TargetDirection = Value;
            if (Value.x < 0)
                Pivot.transform.localScale = new Vector3(-1, 1, 1);
            else if (Value.x > 0)
                Pivot.transform.localScale = new Vector3(1, 1, 1);
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