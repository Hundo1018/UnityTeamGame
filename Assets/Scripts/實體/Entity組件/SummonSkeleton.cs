using UnityEngine;
using SkillConstructer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Entity
{
    public class SummonSkeleton : EntityComponent {
        Skill attackf;
        public void Summon(){
            attackf = new Skill("011;0011;11111;011;0011", 1, 3); //  在指定座標隨機召喚3個實體
            attackf.DoAttackForEachCube = (x, y) => {
                EntityManager.Instance.entities.Add(new Skeleton("skeleton", 1, new Vector2Int(x, y)));
            };
            attackf.Attack();
        }
    }
}