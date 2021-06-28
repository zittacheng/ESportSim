using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Movement_ShipControl : Movement {
        public Rigidbody2D Rig;

        public override void TimePassed(float Value)
        {
            if (!Source)
                return;

            if (GetKey("SpeedScale") < GetKey("TargetSpeedScale"))
            {
                ChangeKey("SpeedScale", Value * GetKey("Acceleration"));
                if (GetKey("SpeedScale") > GetKey("TargetSpeedScale"))
                    SetKey("SpeedScale", GetKey("TargetSpeedScale"));
            }
            else if (GetKey("SpeedScale") > GetKey("TargetSpeedScale"))
            {
                ChangeKey("SpeedScale", -Value * GetKey("Acceleration"));
                if (GetKey("SpeedScale") < GetKey("TargetSpeedScale"))
                    SetKey("SpeedScale", GetKey("TargetSpeedScale"));
            }

            Vector2 D = Source.GetDirection().normalized * GetSpeed() * GetKey("SpeedScale");
            Rig.velocity = D;
            Source.SetPosition(Rig.position);

            if (GetKey("InputX") != 0)
                Source.Rotate(GetRotationSpeed() * Value * GetKey("InputX"));
        }

        public virtual float GetSpeed()
        {
            if (Source)
                return Source.PassValue("Speed", GetKey("Speed"));
            return GetKey("Speed");
        }

        public virtual float GetRotationSpeed()
        {
            if (Source)
                return Source.PassValue("RotationSpeed", GetKey("RotationSpeed"));
            return GetKey("RotationSpeed");
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "SpeedChange")
            {
                if (Value > 0 && GetKey("TargetSpeedScale") < 1)
                    ChangeKey("TargetSpeedScale", 1);
                else if (Value < 0 && GetKey("TargetSpeedScale") > -1)
                    ChangeKey("TargetSpeedScale", -1);
            }
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "InputX": Input value x
            // "InputY": Input value y
            // "RotationSpeed": Base rotation speed
            // "SpeedScale": Current speed scale
            // "TargetSpeedScale": Target speed scale
            // "Acceleration": Speed scale change rate
            // "SpeedChange": Trigger for speed change function
            base.CommonKeys();
        }
    }
}