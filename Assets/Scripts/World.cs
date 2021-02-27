using UnityEngine;
using SkillConstructer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;
public class World : MonoBehaviour
{
    private Player player = new Player("playerName", 5);
    public Player getPlayer(){
        return player;
    }
    
}
