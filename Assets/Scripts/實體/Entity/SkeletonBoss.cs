using System.Collections;
using System.Collections.Generic;
using SkillConstructer;
using UnityEngine;
using Entity;
namespace Entity
{
    public class SkeletonBoss : EntityBase
    {
        private string name;
        public SkeletonBoss(string name,int Hp, Vector2Int Loc){
            this._object = new GameObject(name);
            this._object.AddComponent<SpriteRenderer>();
            this._object.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load<Sprite>("skeleton");
            this._object.transform.parent = EntityManager.Instance.parentManager.transform;
            this.name = name;
            this.Hp = Hp;
            this.Location = Loc;
            this.Add(new SummonSkeleton());
        }
    }
}