using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Card : MonoBehaviour {
        public float MaxLife;
        public float Life;
        public float BaseDamage;
        public float Aggro;
        [Space]
        public int Side;
        [HideInInspector] public CardInfo Info;
        [HideInInspector] public CharacterAnim Anim;
        [HideInInspector] public CardCollider SLC;
        [HideInInspector] public bool AlreadyDead;
        [Space]
        public Vector2 Position;
        [HideInInspector] public Vector2 Direction = new Vector2(0.01f, 1f);
        public Card CurrentTarget;
        [HideInInspector] public Mark_Skill CurrentCast;
        [HideInInspector] public Card CastTarget;
        [HideInInspector] public Vector2 CastPosition;
        [Space]
        public GameObject IniTargeting;
        [HideInInspector] public GameObject IniMovement;
        [HideInInspector] public List<GameObject> CollisionPoints;
        [HideInInspector] public GameObject TargetingTrigger;
        [HideInInspector] public Movement movement;
        [HideInInspector] public List<Movement> AddMovements;
        [HideInInspector] public Targeting targeting;
        [HideInInspector] public float TargetingDelay;
        [HideInInspector] public Mark targetingTrigger;
        [HideInInspector] public PathFinder PF;
        [Space]
        public List<GameObject> IniSkills;
        public List<GameObject> IniStatus;
        public Mark KeyMark;
        [Space]
        public List<Mark_Skill> Skills;
        public List<Mark_Status> Status;
        public List<Mark_Skill> RenderSkills;
        [HideInInspector] public List<Mark_Status> RenderStatus;
        public List<Signal> WaitingSignals;
        public float GlobalCoolDown;
        [HideInInspector] public float MaxGCD;

        public void Awake()
        {
            Skills = new List<Mark_Skill>();
            for (int i = 0; i < IniSkills.Count; i++)
            {
                if (IniSkills[i])
                    Skills.Add(IniSkills[i].GetComponent<Mark_Skill>());
                else
                    Skills.Add(null);
            }
            foreach (Mark_Skill M in Skills)
            {
                if (M)
                    M.Source = this;
            }

            Status = new List<Mark_Status>();
            for (int i = 0; i < IniStatus.Count; i++)
            {
                if (IniStatus[i])
                    Status.Add(IniStatus[i].GetComponent<Mark_Status>());
                else
                    Status.Add(null);
            }
            foreach (Mark_Status M in Status)
            {
                if (M)
                    M.Source = this;
            }

            UpdateRenderSkills();
            UpdateRenderStatus();

            GetTargeting();
            GetMovement();
            GetPathFinder();
            if (Position.x == 0 && Position.y == 0)
                Position = transform.position;
            if (GetAnim())
            {
                GetAnim().SetPosition(GetPosition());
                GetAnim().SetDirection(GetDirection());
            }

            SetKey("Life", Life);
            SetKey("MaxLife", MaxLife);
            SetKey("BaseDamage", BaseDamage);
        }

        public void AddCollisionPoint(GameObject G, int Index)
        {
            while (CollisionPoints.Count <= Index)
                CollisionPoints.Add(gameObject);
            CollisionPoints[Index] = G;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            CombatUpdate(CombatControl.Main.CombatTime());
        }

        public void CombatUpdate(float Value)
        {
            if (!CombatControl.Main.InCombat || !CombatControl.Main.Cards.Contains(this))
            {
                CurrentTarget = null;
                return;
            }

            TimePassed(Value);
            GlobalCoolDown -= Value * GetAttackSpeedScale();
            if (CurrentCast)
                ChangeKey("CCT", Value);
            if (GetKey("TurnDelay") > 0)
                ChangeKey("TurnDelay", -Value);
            foreach (Mark_Skill Skill in Skills)
            {
                if (!Skill)
                    continue;
                if (Skill.GetKey("Auto") != 1)
                    continue;
                Skill.TryUse();
            }
            if (CurrentCast && GetKey("CCT") >= GetKey("MCT") && CurrentCast.GetKey("HoldCast") <= 0)
            {
                TryFinishCast();
            }
            for (int i = WaitingSignals.Count - 1; i >= 0; i--)
            {
                if (WaitingSignals[i].GetKey("Delay") >= 0)
                    WaitingSignals[i].ChangeKey("Delay", -Value);
                else
                {
                    Signal S = WaitingSignals[i];
                    WaitingSignals.RemoveAt(i);
                    RealSendSignal(S);
                }
            }
            if (GlobalCoolDown < 0)
                GlobalCoolDown = 0;
            SetKey("Life", Life);
            SetKey("MaxLife", MaxLife);

            if (GetMovement())
                MovementUpdate(Value);
            if (GetTargeting())
                TargetingUpdate(Value);
            if (GetPathFinder())
            {
                if (GetTarget())
                    GetPathFinder().PathUpdate(GetPosition(), GetTarget().GetPosition());
                else
                    GetPathFinder().PathUpdate(GetPosition(), GetPosition());
            }
            DeathUpdate();
        }

        public void StartOfTurn()
        {
            SetKey("TurnDelay", 0.1f);
        }

        public void EndOfTurn()
        {

        }

        public void StartOfCombat()
        {
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (Skills[i])
                    Skills[i].StartOfCombat();
            }
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].StartOfCombat();

            foreach (Mark_Skill Skill in Skills)
            {
                if (!Skill)
                    continue;
                if (Skill.GetKey("StartOfCombat") != 1)
                    continue;
                Skill.TryUse();
            }
        }

        public void EndOfCombat()
        {
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (Skills[i])
                    Skills[i].EndOfCombat();
            }
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].EndOfCombat();

            foreach (Mark_Skill Skill in Skills)
            {
                if (!Skill)
                    continue;
                if (Skill.GetKey("EndOfCombat") != 1)
                    continue;
                Skill.TryUse();
            }
        }

        public void UseSkill(Mark_Skill Skill, Card Target)
        {
            if (Skill.GetKey("GCD") > 0)
                GlobalCoolDown += Skill.GetKey("GCD");
            Skill.Use(Target);
        }

        public void UseSkill(Mark_Skill Skill, Vector2 Position)
        {
            if (Skill.GetKey("GCD") > 0)
                GlobalCoolDown += Skill.GetKey("GCD");
            Skill.Use(Position);
        }

        public void CastSkill(Mark_Skill Skill, Card Target)
        {
            if (CurrentCast)
                CastInterrupt();
            CurrentCast = Skill;
            CastTarget = Target;
            CastPosition = GetPosition();
            SetKey("CCT", 0);
            SetKey("MCT", Skill.GetKey("CastTime"));

            if (GetMovement())
                GetMovement().Stop();
        }

        public void CastSkill(Mark_Skill Skill, Vector2 Position)
        {
            if (CurrentCast)
                CastInterrupt();
            CurrentCast = Skill;
            CastTarget = null;
            CastPosition = Position;
            SetKey("CCT", 0);
            SetKey("MCT", Skill.GetKey("CastTime"));

            if (GetMovement())
                GetMovement().Stop();
        }

        public void TryFinishCast()
        {
            if (!CurrentCast)
                return;
            if (GetKey("CCT") < CurrentCast.GetKey("CastTime"))
            {
                CastInterrupt();
                return;
            }
            if (CurrentCast.GetKey("Positional") > 0)
            {
                if (!CurrentCast.CanUse(CastPosition))
                {
                    CastInterrupt();
                    return;
                }
            }
            else
            {
                if (!CurrentCast.CanUse(CastTarget))
                {
                    CastInterrupt();
                    return;
                }
            }
            CastFinish();
        }

        public void CastInterrupt()
        {
            CurrentCast = null;
            CastTarget = null;
            CastPosition = GetPosition();
            SetKey("CCT", 0);
            SetKey("MCT", 0.1f);
        }

        public void CastFinish()
        {
            if (CurrentCast.GetKey("Positional") > 0)
            {
                if (!CurrentCast.TryUse(CastPosition, GetKey("CCT")))
                    return;
            }
            else
            {
                if (!CurrentCast.TryUse(CastTarget, GetKey("CCT")))
                    return;
            }
            CurrentCast = null;
            CastTarget = null;
            CastPosition = GetPosition();
            SetKey("CCT", 0);
            SetKey("MCT", 0.1f);
        }

        public Mark_Skill GetCast()
        {
            return CurrentCast;
        }

        public void SendSignal(GameObject Prefab)
        {
            SendSignal(Prefab, null, null);
        }

        public void SendSignal(GameObject Prefab, List<string> AddKeys, Card Target)
        {
            GameObject G = Instantiate(Prefab);
            Signal S = G.GetComponent<Signal>();
            S.Source = this;
            S.IniAddKeys(AddKeys);
            if (S.GetKey("Target") == 1)
                S.Target = Target;
            else if (S.GetKey("Target") == 0)
                S.Target = this;
            if (S.GetKey("Delay") <= 0)
                RealSendSignal(S);
            else
                WaitingSignals.Add(S);
        }

        public void RealSendSignal(Signal S)
        {
            S.StartEffect();
            S.Source.OutputSignal(S);
            if (S.Target)
                S.Target.InputSignal(S);
            S.EndEffect();
            if (!S)
                return;
            S.Source.ReturnSignal(S);
            if (S.Target)
                S.Target.ConfirmSignal(S);
            Destroy(S.gameObject, 5);
        }

        public float PassValue(string Key)
        {
            return PassValue(Key, 0);
        }

        public float PassValue(string Key, float Value)
        {
            float V = Value;
            for (int i = Status.Count - 1; i >= 0; i--)
                V = Status[i].PassValue(Key, V);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    V = Skills[i].PassValue(Key, V);
            return FinalPassValue(Key, V);
        }

        public float FinalPassValue(string Key, float Value)
        {
            if (Key == "Speed" && GetCast())
                return Value * PassValue("CMSpeed", GetKey("CMSpeed"));
            else if (Key == "Life")
                return Life;
            else if (Key == "MaxLife")
                return MaxLife;
            else
                return Value;
        }

        public void TimePassed(float Value)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].TimePassed(Value);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].TimePassed(Value);
        }

        public void InvokeSkill(string ID)
        {
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (!Skills[i])
                    continue;
                if (Skills[i].GetID() == ID)
                    Skills[i].TryUse();
            }
        }

        public void TargetingUpdate(float Value)
        {
            Targeting T = targeting;
            if (CurrentTarget)
            {
                if (!CurrentTarget.CardActive() || !T.CheckTarget(this, CurrentTarget))
                    CurrentTarget = null;
            }
            else
            {
                if (TargetingDelay > 0 || (targetingTrigger && targetingTrigger.GetKey("CCD") > 0))
                {
                    TargetingDelay -= Value;
                    return;
                }
                TargetingDelay = 0.15f;
                CurrentTarget = T.FindTarget(this);
            }
        }

        public Card GetTarget()
        {
            return CurrentTarget;
        }

        public void MovementUpdate(float Value)
        {
            Movement M = GetMovement();
            M.TimePassed(Value);
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public void ChangePosition(Vector2 Value)
        {
            ChangePosition(Value, true);
        }

        public void ChangePosition(Vector2 Value, bool Colliding)
        {
            ChangePosition(Value.x, Value.y, Colliding);
        }

        public void ChangePosition(float X, float Y)
        {
            ChangePosition(X, Y, true);
        }

        public void ChangePosition(float X, float Y, bool Colliding)
        {
            Vector2 a = GetPosition();
            if (Colliding)
            {
                float Scale = 0.3f;
                if (new Vector2(X, Y).magnitude > Scale - 0.2f)
                    Scale += new Vector2(X, Y).magnitude - 0.1f;
                if (!MovementCollisionCheck(a, a + new Vector2(X, Y).normalized * Scale, out Vector2 Contact))
                    SetPosition(a.x + X, a.y + Y);
                else
                {
                    //SetPosition(Contact);
                    if (GetMovement())
                        GetMovement().OnContact();
                }
            }
            else
                SetPosition(a.x + X, a.y + Y);
            /*if ((X != 0 || Y != 0) && GetCast())
                CastInterrupt();*/
        }

        public void SetPosition(Vector2 Value)
        {
            SetPosition(Value.x, Value.y);
        }

        public void SetPosition(float X, float Y)
        {
            Position = new Vector2(X, Y);
            GetAnim().SetPosition(GetPosition());
        }

        public bool MovementCollisionCheck(Vector2 Ori, Vector2 Target, out Vector2 Contact)
        {
            if (!PathControl.Main.CanMove(Ori, Target, out Contact))
                return true;
            foreach (GameObject G in CollisionPoints)
            {
                Vector2 Add = (Vector2)G.transform.position - (Vector2)transform.position;
                if (!PathControl.Main.CanMove(Ori + Add, Target + Add, out Contact))
                    return true;
            }
            return false;
        }

        public bool LineCollisionCheck(Vector2 Ori, Vector2 Target, out Vector2 Contact)
        {
            Contact = new Vector2(Mathf.Infinity, Mathf.Infinity);
            if (CollisionPoints.Count <= 1)
                return false;
            List<Vector2> points = new List<Vector2>();
            for (int i = 0; i + 1 < CollisionPoints.Count; i++)
            {
                if (PathControl.Colliding(CollisionPoints[i].transform.position, CollisionPoints[i + 1].transform.position, Ori, Target, out Contact))
                    points.Add(Contact);
            }
            if (PathControl.Colliding(CollisionPoints[CollisionPoints.Count - 1].transform.position, CollisionPoints[0].transform.position, Ori, Target, out Contact))
                points.Add(Contact);
            if (points.Count <= 0)
                return false;
            float D = Mathf.Infinity;
            Vector2 FinalPoint = new Vector2();
            for (int i = 0; i < points.Count; i++)
            {
                if ((points[i] - Ori).magnitude <= D)
                {
                    FinalPoint = points[i];
                    D = (points[i] - Ori).magnitude;
                }
            }
            Contact = FinalPoint;
            return true;
        }

        public Vector2 GetDirection()
        {
            return Direction;
        }

        public void SetDirection(Vector2 Value)
        {
            SetDirection(Value.x, Value.y);
        }

        public void SetDirection(float X, float Y)
        {
            if (X == 0)
                X = 0.01f;
            if (Y == 0)
                Y = 0.01f;
            Direction = new Vector2(X, Y);
            GetAnim().SetDirection(GetDirection());
        }

        public void LookAt(Vector2 Position)
        {
            LookAt(Position.x, Position.y);
        }

        public void LookAt(float X, float Y)
        {
            Vector2 Ori = GetPosition();
            SetDirection(X - Ori.x, Y - Ori.y);
        }

        public void Rotate(float Angle)
        {
            float Ori = CharacterAnim.AbsoluteAngle(CharacterAnim.DirectionToAngle(GetDirection()));
            float Target = CharacterAnim.AbsoluteAngle(Ori + Angle);
            Vector2 Set = CharacterAnim.AngleToDirection(Target).normalized;
            SetDirection(Set);
        }

        public void DeathUpdate()
        {
            if (GetLife() <= 0 && GetLife() != -999 && !AlreadyDead)
                Death();
        }

        public bool CombatActive()
        {
            return !AlreadyDead;
        }

        public bool CardActive()
        {
            return !AlreadyDead;
        }

        public void Death()
        {
            if (AlreadyDead)
                return;

            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].Death();
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].Death();

            AlreadyDead = true;
            //CombatControl.Main.RemoveCard(this);
            CombatControl.Main.OnCardDeath(this);
            if (GetAnim())
                GetAnim().Death();
            /*if (GetComponent<Collider>())
                GetComponent<Collider>().enabled = false;*/
            //Destroy(gameObject, 5f);
        }

        public void Revive()
        {
            ChangeLife(GetMaxLife() * 2f);
            if (!AlreadyDead)
                return;

            AlreadyDead = false;
            CombatControl.Main.OnCardRevive(this);
            if (GetAnim())
                GetAnim().Revive();
        }

        public float GetLife()
        {
            return Life;
        }

        public void ChangeLife(float Value)
        {
            float V = Value;
            for (int i = Status.Count - 1; i >= 0; i--)
                if (Status[i].GetComponent<Mark_Status_Shield>())
                    V = Status[i].GetComponent<Mark_Status_Shield>().ProcessLifeChange(V);
            if (Life + V > GetMaxLife())
                V = GetMaxLife() - Life;
            Life += V;
            SetKey("Life", Life);
        }

        public float GetMaxLife()
        {
            return MaxLife;
        }

        public void ChangeMaxLife(float Value)
        {
            MaxLife += Value;
            if (Life > MaxLife)
                Life = MaxLife;
            SetKey("Life", Life);
            SetKey("MaxLife", MaxLife);
        }

        public float GetBaseDamage()
        {
            return BaseDamage;
        }

        public float GetAggro()
        {
            return Aggro;
        }

        public void ChangeBaseDamage(float Value)
        {
            BaseDamage += Value;
            SetKey("BaseDamage", BaseDamage);
        }

        public float GetAttackSpeedScale()
        {
            return PassValue("AttackSpeed", 1);
        }

        public float GetRange(float BaseRange)
        {
            return PassValue("Range", BaseRange);
        }

        public float GetSpeed(float BaseSpeed)
        {
            return PassValue("Speed", BaseSpeed);
        }

        public int GetSide()
        {
            return (int)PassValue("Side", Side);
        }

        public void SetGCD(float Value)
        {
            GlobalCoolDown = Value;
            MaxGCD = Value;
        }

        public float GetGCD()
        {
            return GlobalCoolDown;
        }

        public float GetMaxGCD()
        {
            return MaxGCD;
        }

        public void OutputSignal(Signal S)
        {
            if (S.GetKey("IgnoreSource") != 0)
                return;

            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].OutputSignal(S);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].OutputSignal(S);
        }

        public void InputSignal(Signal S)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].InputSignal(S);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].InputSignal(S);

            for (int i = Status.Count - 1; i >= 0; i--)
                S.InputMark(Status[i]);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    S.InputMark(Skills[i]);
        }

        public void ReturnSignal(Signal S)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].ReturnSignal(S);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].ReturnSignal(S);
        }

        public void ConfirmSignal(Signal S)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
                Status[i].ConfirmSignal(S);
            for (int i = Skills.Count - 1; i >= 0; i--)
                if (Skills[i])
                    Skills[i].ConfirmSignal(S);
        }

        public Mark_Skill GetSkill(int Index)
        {
            if (Index < 0 || Index >= Skills.Count)
                return null;
            return Skills[Index];
        }

        public Mark_Skill GetSkill(string ID, out int Index)
        {
            Index = -1;
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (Skills[i] && Skills[i].GetID() == ID)
                {
                    Index = i;
                    return Skills[i];
                }
            }
            return null;
        }

        public Mark_Skill GetRenderSkill(int Index)
        {
            if (Index < 0 || Index >= RenderSkills.Count)
                return null;
            return RenderSkills[Index];
        }

        public void AddSkill(Mark_Skill M, int Index)
        {
            if (Index >= Skills.Count)
                return;
            GameObject G = Instantiate(M.gameObject);
            G.transform.parent = transform;
            Mark_Skill S = G.GetComponent<Mark_Skill>();
            S.Source = this;
            if (S.HasKey("Count") && S.GetInfo() && !S.HasKey("IgnoreStack"))
            {
                for (int i = Skills.Count - 1; i >= 0; i--)
                {
                    if (!Skills[i] || !Skills[i].GetInfo())
                        continue;
                    if (Skills[i].GetID() == S.GetID())
                    {
                        if (Skills[i].CanItemStack(S))
                        {
                            Skills[i].ItemStack(S);
                            Destroy(S.gameObject);
                            return;
                        }
                        else
                            break;
                    }
                }
            }
            if (Index < 0)
            {
                Destroy(G);
                return;
            }
            if (Skills[Index])
                RemoveSkill(Index);
            Skills[Index] = S;
            UpdateRenderSkills();
        }

        public void AddSkill(Mark_Skill M)
        {
            int n = GetNextSkillIndex();
            AddSkill(M, n);
        }

        public int GetNextSkillIndex()
        {
            for (int i = 0; i < Skills.Count; i++)
            {
                if (!Skills[i])
                    return i;
            }
            Skills.Add(null);
            return Skills.Count - 1;
        }

        public void RemoveSkill(int Index)
        {
            if (Index < 0 || Index >= Skills.Count)
                return;
            if (Skills[Index])
                Destroy(Skills[Index].gameObject, 1f);
            Skills[Index] = null;
            UpdateRenderSkills();
        }

        public void RemoveSkill(Mark_Skill M)
        {
            if (!M || !Skills.Contains(M))
                return;
            RemoveSkill(Skills.IndexOf(M));
        }

        public void RemoveSkill(string Key, int Count)
        {
            Mark_Skill M = GetSkill(Key, out int Index);
            if (!M)
                return;
            if (M.HasKey("Count") && M.GetKey("Count") > Count)
                M.ChangeKey("Count", -Count);
            else
                RemoveSkill(Index);
        }

        public void SoftRemoveSkill(Mark_Skill M)
        {
            if (!M || !Skills.Contains(M))
                return;
            M.SetKey("Render", 0);
        }

        public void UpdateRenderSkills()
        {
            RenderSkills = new List<Mark_Skill>();
            for (int i = 0; i < Skills.Count; i++)
            {
                if (!Skills[i])
                    RenderSkills.Add(null);
                else if (Skills[i].GetKey("Render") != 0)
                    RenderSkills.Add(Skills[i]);
            }
        }

        public bool HasSkill(string ID, out int Index)
        {
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (Skills[i] && Skills[i].GetID() == ID)
                {
                    Index = i;
                    return true;
                }
            }
            Index = -1;
            return false;
        }

        public bool HasSkill(Mark_Skill Skill, out int Index)
        {
            for (int i = Skills.Count - 1; i >= 0; i--)
            {
                if (Skills[i] == Skill)
                {
                    Index = i;
                    return true;
                }
            }
            Index = -1;
            return false;
        }

        public Mark_Status GetStatus(int Index)
        {
            if (Index < 0 || Index >= RenderStatus.Count)
                return null;
            return RenderStatus[Index];
        }

        public Mark_Status GetStatus(string Key)
        {
            for (int i = Status.Count - 1; i >= 0; i--)
            {
                if (Status[i] && Status[i].GetInfo() && Status[i].GetInfo().GetID() == Key)
                    return Status[i];
            }
            return null;
        }

        public Mark_Status AddStatus(Mark_Status M)
        {
            return AddStatus(M, new List<string>());
        }

        public Mark_Status AddStatus(Mark_Status M, List<string> AddKeys)
        {
            GameObject G = Instantiate(M.gameObject);
            G.transform.parent = transform;
            Mark_Status S = G.GetComponent<Mark_Status>();
            S.Source = this;
            foreach (string s in AddKeys)
                S.SetKey(KeyBase.Translate(s, out float v), v);
            if (S.GetInfo() && !S.HasKey("IgnoreStack"))
            {
                for (int i = Status.Count - 1; i >= 0; i--)
                {
                    if (!Status[i].GetInfo())
                        continue;
                    if (Status[i].GetID() == S.GetID())
                    {
                        Status[i].Stack(S);
                        Destroy(S.gameObject);
                        return null;
                    }
                }
            }
            Status.Add(S);
            UpdateRenderStatus();
            return S;
        }

        public void RemoveStatus(Mark_Status M)
        {
            if (!Status.Contains(M))
                return;
            Status.Remove(M);
            UpdateRenderStatus();
            Destroy(M.gameObject);
        }

        public void UpdateRenderStatus()
        {
            RenderStatus.Clear();
            for (int i = 0; i < Status.Count; i++)
            {
                if (Status[i] && Status[i].GetKey("Render") != 0 && (!Status[i].HasKey("Count") || Status[i].GetKey("Count") > 0))
                    RenderStatus.Add(Status[i]);
            }
        }

        public Targeting GetTargeting()
        {
            if (!targeting && IniTargeting)
            {
                targeting = IniTargeting.GetComponent<Targeting>();
                targeting.Source = this;
                if (TargetingTrigger)
                    targetingTrigger = TargetingTrigger.GetComponent<Mark>();
            }
            return targeting;
        }

        public Movement GetMovement()
        {
            if (!movement && IniMovement)
            {
                movement = IniMovement.GetComponent<Movement>();
                movement.Source = this;
            }
            if (AddMovements.Count > 0)
                return AddMovements[0];
            return movement;
        }

        public Movement AddMovement(Movement M)
        {
            return AddMovement(M, new List<string>());
        }

        public Movement AddMovement(Movement M, List<string> AddKeys)
        {
            if (AddMovements.Count > 0)
            {
                if (AddMovements[0].GetKey("Priority") <= M.GetKey("Priority"))
                    RemoveMovement(AddMovements[0]);
                else
                    return null;
            }
            else if (movement)
            {
                if (movement.GetKey("Priority") > M.GetKey("Priority"))
                    return null;
            }
            GameObject G = Instantiate(M.gameObject);
            G.transform.parent = transform;
            Movement MV = G.GetComponent<Movement>();
            MV.Source = this;
            foreach (string s in AddKeys)
                MV.SetKey(KeyBase.Translate(s, out float v), v);
            AddMovements.Add(MV);
            return MV;
        }

        public void RemoveMovement(Movement M)
        {
            if (!AddMovements.Contains(M))
                return;
            AddMovements.Remove(M);
            Destroy(M.gameObject);
        }

        public CardGroup GetGroup()
        {
            foreach (CardGroup CG in CombatControl.Main.Groups)
                if (CG.Cards.Contains(this))
                    return CG;
            return null;
        }

        public PathFinder GetPathFinder()
        {
            if (!PF)
                PF = GetComponent<PathFinder>();
            return PF;
        }

        public CardInfo GetInfo()
        {
            if (!Info)
                Info = GetComponent<CardInfo>();
            return Info;
        }

        public CharacterAnim GetAnim()
        {
            if (!Anim)
                Anim = GetComponent<CharacterAnim>();
            return Anim;
        }

        public float GetSize()
        {
            if (!SLC)
                SLC = GetComponent<CardCollider>();
            if (!SLC)
                return 0;
            return SLC.GetColliderRange();
        }

        public float GetSelectionRange()
        {
            if (!SLC)
                SLC = GetComponent<CardCollider>();
            if (!SLC)
                return 0;
            return SLC.GetSelectionRange();
        }

        public string GetName()
        {
            return GetInfo().GetName();
        }

        public void SetName(string Value)
        {
            GetInfo().Name = Value;
        }

        public Sprite GetSprite()
        {
            return GetInfo().GetSprite();
        }

        public bool HasKey(string Key)
        {
            if (!KeyMark)
                return false;
            return KeyMark.HasKey(Key);
        }

        public float GetKey(string Key)
        {
            if (!KeyMark)
                return 0;
            return KeyMark.GetKey(Key);
        }

        public float ChangeKey(string Key, float Value)
        {
            if (!KeyMark)
                return 0;
            return KeyMark.ChangeKey(Key, Value);
        }

        public void SetKey(string Key, float Value)
        {
            if (!KeyMark)
                return;
            KeyMark.SetKey(Key, Value);
        }
    }
}