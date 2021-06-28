using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_EffectLine_Self : Signal {
        public GameObject EffectPrefab;
        public Color EffectColor;

        public override void EndEffect()
        {
            if (!Target)
                return;
            if (HasKey("LineAngle"))
                LineActive(GetKey("LineAngle"), GetKey("LineStart"), GetKey("LineEnd"), GetKey("LineAddAngle"));
            if (HasKey("LineAngle2"))
                LineActive(GetKey("LineAngle2"), GetKey("LineStart2"), GetKey("LineEnd2"), GetKey("LineAddAngle2"));
            if (HasKey("LineAngle3"))
                LineActive(GetKey("LineAngle3"), GetKey("LineStart3"), GetKey("LineEnd3"), GetKey("LineAddAngle3"));
            if (HasKey("LineAngle4"))
                LineActive(GetKey("LineAngle4"), GetKey("LineStart4"), GetKey("LineEnd4"), GetKey("LineAddAngle4"));
            if (HasKey("LineAngle5"))
                LineActive(GetKey("LineAngle5"), GetKey("LineStart5"), GetKey("LineEnd5"), GetKey("LineAddAngle5"));
            if (HasKey("LineAngle6"))
                LineActive(GetKey("LineAngle6"), GetKey("LineStart6"), GetKey("LineEnd6"), GetKey("LineAddAngle6"));
            if (HasKey("LineAngle7"))
                LineActive(GetKey("LineAngle7"), GetKey("LineStart7"), GetKey("LineEnd7"), GetKey("LineAddAngle7"));
            if (HasKey("LineAngle8"))
                LineActive(GetKey("LineAngle8"), GetKey("LineStart8"), GetKey("LineEnd8"), GetKey("LineAddAngle8"));
            base.EndEffect();
        }

        public void LineActive(float Angle, float Start, float End, float AddAngle)
        {
            Vector2 StartPoint = GetPosition(Angle, Start);
            Vector2 EndPoint = GetPosition(Angle + AddAngle, End);
            StartPoint = StartPoint * Start * Target.GetSize() + Target.GetPosition();
            EndPoint = EndPoint * End * Target.GetSize() + Target.GetPosition();
            GameObject G = Instantiate(EffectPrefab);
            EffectLine EL = G.GetComponent<EffectLine>();
            EL.Ori = StartPoint;
            EL.Target = EndPoint;
            EL.MainColor = EffectColor;
            EL.BaseWidth = GetKey("Width");
            EL.Ini();
        }

        public Vector2 GetPosition(float Angle, float Value)
        {
            if (Angle < 0)
                Angle += 360;
            else if (Angle >= 360)
                Angle -= 360;
            if (Angle == 0)
                return new Vector2(0, Value);
            else if (Angle == 90)
                return new Vector2(Value, 0);
            else if (Angle == 180)
                return new Vector2(0, -Value);
            else if (Angle == 270)
                return new Vector2(-Value, 0);
            else if (Angle > 0 && Angle < 90)
                return new Vector2(Mathf.Sin(Mathf.Deg2Rad * Angle) * Value, Mathf.Cos(Mathf.Deg2Rad * Angle) * Value);
            else if (Angle > 90 && Angle < 180)
                return new Vector2(Mathf.Cos(Mathf.Deg2Rad * (Angle - 90f)) * Value, -Mathf.Sin(Mathf.Deg2Rad * (Angle - 90f)) * Value);
            else if (Angle > 180 && Angle < 270)
                return new Vector2(-Mathf.Sin(Mathf.Deg2Rad * (Angle - 180f)) * Value, -Mathf.Cos(Mathf.Deg2Rad * (Angle - 180f)) * Value);
            else if (Angle > 270 && Angle < 360)
                return new Vector2(-Mathf.Cos(Mathf.Deg2Rad * (Angle - 270f)) * Value, Mathf.Sin(Mathf.Deg2Rad * (Angle - 270f)) * Value);
            else
                return new Vector2(0, 0);
        }

        public override void CommonKeys()
        {
            // "Width": Width of all effect lines
            // "LineAngle": Angle of the first line
            // "LineStart": Relative start point of the first line
            // "LineEnd": Relative end point of the first line
            // "LineAddAngle": End point angle change
            // "LineAngle2" "LineStart2" "LineEnd2" "LineAddAngle2"
            // "LineAngle3" "LineStart3" "LineEnd3" "LineAddAngle3"
            // "LineAngle4" "LineStart4" "LineEnd4" "LineAddAngle4"
            // "LineAngle5" "LineStart5" "LineEnd5" "LineAddAngle5"
            // "LineAngle6" "LineStart6" "LineEnd6" "LineAddAngle6"
            // "LineAngle7" "LineStart7" "LineEnd7" "LineAddAngle7"
            // "LineAngle8" "LineStart8" "LineEnd8" "LineAddAngle8"
            base.CommonKeys();
        }
    }
}