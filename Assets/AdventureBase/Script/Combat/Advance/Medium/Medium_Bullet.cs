using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Medium_Bullet : Medium_Hitscan {

        public override void Ini(Card S, Card T)
        {
            base.Ini(S, T);
            Vector2 Direction = (new Vector2(GetKey("PositionX"), GetKey("PositionY")) - new Vector2(GetKey("SourcePositionX"), GetKey("SourcePositionY"))).normalized;
            if (Direction.x > 0 && Direction.x <= 0.0001f)
                Direction.x = 0.0001f;
            else if (Direction.x < 0 && Direction.x >= -0.0001f)
                Direction.x = -0.0001f;
            if (Direction.y > 0 && Direction.y <= 0.0001f)
                Direction.y = 0.0001f;
            else if (Direction.y < 0 && Direction.y >= -0.0001f)
                Direction.y = -0.0001f;
            SetKey("DirectionX", Direction.x);
            SetKey("DirectionY", Direction.y);
            SetKey("CurrentPositionX", GetKey("SourcePositionX"));
            SetKey("CurrentPositionY", GetKey("SourcePositionY"));
            SetAnimPosition(new Vector2(GetKey("SourcePositionX"), GetKey("SourcePositionY")));
        }

        public override void EffectUpdate(float Value)
        {
            if (GetKey("Delay") > 0)
            {
                ChangeKey("Delay", -Value);
                return;
            }

            Vector2 Ori = new Vector2(GetKey("CurrentPositionX"), GetKey("CurrentPositionY"));
            Vector2 Target = Ori + new Vector2(GetKey("DirectionX"), GetKey("DirectionY")).normalized * Value * GetKey("Speed");
            ChangeKey("Range", -(Target - Ori).magnitude);
            HitEffect(Ori, Target, out Vector2 Contact, out Card HitTarget, out bool Hit);
            if (Hit)
            {
                print("hit");
                SetKey("PositionX", Contact.x);
                SetKey("PositionY", Contact.y);
                SetKey("CurrentPositionX", Contact.x);
                SetKey("CurrentPositionY", Contact.y);
                Effect(HitTarget);
                EndEffect();
            }
            else if (GetKey("Range") <= 0)
            {
                Effect(null);
                EndEffect();
            }
            else
            {
                SetKey("CurrentPositionX", Target.x);
                SetKey("CurrentPositionY", Target.y);
            }
            SetAnimPosition(new Vector2(GetKey("CurrentPositionX"), GetKey("CurrentPositionY")));
        }

        public void SetAnimPosition(Vector2 Value)
        {
            GameObject G = transform.Find("AnimBase").gameObject;
            G.SetActive(true);
            G.transform.position = new Vector3(Value.x, Value.y, transform.position.z);
        }

        public override void CommonKeys()
        {
            // "Range": Max range the bullet can travel
            // "Speed": Bullet's speed
            // "CurrentPositionX": Current position X
            // "CurrentPositionY": Current position Y
            // "DirectionX": Bullet's direction X
            // "DirectionY": Bullet's direction Y
            base.CommonKeys();
        }
    }
}