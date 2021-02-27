using UnityEngine;
using SkillConstructer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Entity
{
    public class attackforward : EntityComponent {
        Skill attackf;
        World world;
        public void attack(){
            world = GameObject.Find("World").GetComponent<World>();
            attackf = new Skill(";00100;01010;00100;", new Vector2Int(2, 2));
            attackf.DoAttackForEachCube = (x, y) => {
                if(world.getPlayer().getLocation().x == x && world.getPlayer().getLocation().y == y){
                    world.getPlayer().Hp = world.getPlayer().Hp - 1;
                }
            };
            attackf.Attack();
        }
    }
}