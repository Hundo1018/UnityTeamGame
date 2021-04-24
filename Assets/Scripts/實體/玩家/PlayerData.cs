using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Player
{
    
    [Serializable]
    public class testClassData
    {

        private int a;
        public int b;
        public testClassData(int i)
        {
            a = i;
            b = i;
        }
    }
    
    [Serializable]
    public enum testEnum {testV1,testV2 };
    [Serializable]
    public class PlayerData
    {

        public testClassData tcd;
        public testEnum a;
        public int level;
        public int id;
        public int money;
        public string name;
        public bool isBool;
        public float floatValue;
        public int nul;

        public PlayerData()
        {


        }
    }


}