﻿using System.Collections;
using System.Collections.Generic;
using SkillConstructer;
using Entity;
using System;
using UnityEngine;
namespace Entity
{
    public class Skeleton : EntityBase
    {
        private string name;
        public Skeleton(string name,int Hp, Vector2Int loc){
            this._object = new GameObject(name);
            this._object.AddComponent<SpriteRenderer>();
            this._object.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load<Sprite>("skeleton");
            this._object.transform.parent = EntityManager.Instance.parentManager.transform;
            this.name = name;
            this.Hp = Hp;
            this.Location = loc;
            //this.Add(new attackforward());
        }
    }
}