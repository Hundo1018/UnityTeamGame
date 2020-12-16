using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
public class monsterData 
{

    private List<int> _skills;
    private int _Hp;
    private int _speed;
    private Vector2Int _location;
    private void init(int Hp, int speed, List<int> skills, Vector2Int location){
        this._Hp = Hp;
        this._speed = speed;
        this._skills = skills.ToList();
        this._location = location;
    }
    public monsterData(){
        init(1, 0, new List<int>(), new Vector2Int(2, 2));
    }

    public monsterData(int Hp, int speed, Vector2Int location){
        init(Hp, speed, new List<int>(), location);
    }

    public void addSkill(int Skill){
        _skills.Add(Skill);
    }

    public delegate void EventHandler();
    public event EventHandler behaviorEvent;
    protected virtual void behavior(){
        if(behaviorEvent != null){
            behavior();
        }
    } 

    public virtual void move(){
        System.Random rnd = new System.Random();
        _location.x += rnd.Next(-1,1);
        _location.y += rnd.Next(-1,1);
        _location.x = Mathf.Clamp(_location.x , 0, 4);
        _location.y = Mathf.Clamp(_location.y , 0, 4);
    }
    public virtual void attack(){}
    //public virtual void HealthChange(){}
    public virtual void effects(){}
    public virtual void idle(){}
}
