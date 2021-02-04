using UnityEngine;
namespace Entity
{
    //  組合模式
    /// <summary>
    /// 實體元件
    /// </summary>
    public class EntityBase : EntityComponent {
        public int Hp {get; set;} = 1;

        public void tellHp(){
            Debug.Log("血量為" + Hp);
        }

    }
}