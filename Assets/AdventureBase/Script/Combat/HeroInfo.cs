using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESP;

namespace ADV
{
    public class HeroInfo : MonoBehaviour {
        public LevelScalingType Scaling;
        public string HeroKey;
        public int Role;
        public float MaxLife;
        public float MaxMana;
        public float BaseDamage;
        public float Size = 2;
        public GameObject Targeting;
        public List<GameObject> SkillPrefabs;
        public List<GameObject> StatusPrefabs;

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

        }

        public void SwitchHero(Card Target)
        {
            if (Target.GetInfo().GetID() == HeroKey)
                return;
            Target.GetInfo().Name = HeroKey;
            float ML = MaxLife;
            float BD = BaseDamage;

            float Level = 0;
            if (Scaling == LevelScalingType.Enemy)
                Level = KeyBase.Main.GetKey("EnemyLevel");
            else if (Scaling == LevelScalingType.Player)
                Level = KeyBase.Main.GetKey(HeroKey + "Level");
            BD += ValueBase.GetDamageGain(Level);
            ML += ValueBase.GetLifeGain(Level);
            Target.SetMaxLife(ML);
            Target.SetBaseDamage(BD);
            Target.PassValue("MaxManaChange", MaxMana);
            Target.SetKey("Role", Role);

            for (int i = Target.Skills.Count - 1; i >= 0; i--)
            {
                if (!Target.Skills[i])
                    continue;
                if (Target.Skills[i].GetKey("General") == 0)
                    Target.Skills[i] = null;
            }

            for (int i = Target.Status.Count - 1; i >= 0; i--)
            {
                if (!Target.Status[i])
                    continue;
                if (Target.Status[i].GetKey("General") == 0)
                    Target.RemoveStatus(Target.Status[i]);
            }

            foreach (GameObject G in SkillPrefabs)
            {
                Mark_Skill S = G.GetComponent<Mark_Skill>();
                Target.AddSkill(S);
            }

            foreach (GameObject G in StatusPrefabs)
            {
                Mark_Status S = G.GetComponent<Mark_Status>();
                Target.AddStatus(S);
            }

            if (Targeting)
                Target.ChangeTargeting(Targeting.GetComponent<Targeting>());
            Target.SetKey("Size", Size);
        }
    }

    public enum LevelScalingType
    {
        Player,
        Enemy
    }
}