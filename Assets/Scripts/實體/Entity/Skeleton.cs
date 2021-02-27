using System.Collections;
using System.Collections.Generic;
using SkillConstructer;
using Entity;
namespace Entity
{
    public class Skeleton : EntityBase
    {
        private string name;
        public Skeleton(string name,int Hp){
            this.name = name;
            this.Hp = Hp;
            this.Add(new flyAbility());
            this.Add(new attackforward());
        }
    }
}