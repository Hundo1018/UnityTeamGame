using System.Collections;
using System.Collections.Generic;
using SkillConstructer;
using UnityEngine;
using Entity;
namespace Entity
{
    public class Player : EntityBase
    {
        private string name;
        private Vector2Int location;
        public Player(string name,int Hp){
            this.name = name;
            this.Hp = Hp;
            this.location = new Vector2Int(2,2);
            
        }
        public Vector2Int getLocation(){
            return location;
        }
    }
}