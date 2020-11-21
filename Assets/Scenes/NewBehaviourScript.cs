using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    bool[,] platform = new bool[5, 5]
    {
    {false,false,false,false,false },
    {false,false,false,false,false },
    {false,false,false,false,false },
    {false,false,false,false,false },
    {false,false,false,false,false }
    };

    bool[,] ability = new bool[3, 3]
    {
        {true,true,true},
        {false,true,false},
        {false,true,false}
    };
    bool[,] UltimateAkill = new bool[5, 5]
    {
        {true   ,true   ,true   ,false  ,false},
        {false  ,true   ,false  ,false  ,false},
        {false  ,true   ,true   ,true   ,false},
        {false  ,false  ,true   ,false  ,false},
        {false  ,false  ,true   ,false  ,false}
    };
    //踩過變紅色
    Vector2Int playerNow = new Vector2Int(3, 3);
    void check3x3()
    {
        ///playerNow保持在檢查矩陣的左下角，
        Vector2Int checkPoint = new Vector2Int(0, 0);

        checkPoint.x = playerNow.x - 2;
        if (checkPoint.x < 0) checkPoint.x = 0;
        checkPoint.y = playerNow.y - 2;
        if (checkPoint.y < 0) checkPoint.y = 0;
        //移動起始點
        for (int i = 0; i < 4; checkPoint.y++, i++)
        {
            for (int j = 0; j < 4; checkPoint.x++, j++)
            {
                //3x3尋訪
                for (int w = checkPoint.y; w < 3; w++)
                {
                    for (int x = checkPoint.x; x < 3; x++)
                    {
                        //每個點對到每個Skill同位置的點
                        //不符合就提前剔除
                        //若都剃除完就結束
                        //最後剩下的就是
                    }
                }
                //起始點超過就從0開始
                checkPoint.x %= 5;
                checkPoint.y %= 5;
            }
        }
    }
}