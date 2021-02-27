using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region 單例模式
    private static UIManager _instance;
    private UIManager()
    {
        //TODO：初始化
    }
    public static UIManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new UIManager();
        }
        return _instance;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void click(int data)
    {
        Debug.Log("click by" + data);
    }
}
