using UnityEngine;
namespace Entity
{
    //  組合模式
    /// <summary>
    /// 實體元件
    /// </summary>
    public class EntityBase : EntityComponent {
        public int Hp = 1;
        public GameObject _object;

        public Vector2Int Location = new Vector2Int(0, 0);
        public void tellHp(){
            Debug.Log("血量為" + Hp);
        }

    }
}