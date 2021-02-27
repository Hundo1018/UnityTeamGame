using UnityEngine;
namespace Floor
{
    /// <summary>
    /// 掛在prefab上的script
    /// </summary>
    public class FloorUnit : MonoBehaviour
    {

        public FloorData data;
        public FloorUnit(Vector2 position)
        {
            data = new FloorData(position);
        }
        public void SetStatus(bool s)
        {
            data.status = s;
            ChangeColor(s);
        }
        /// <summary>
        /// 先頂著用，之後換皮改用其他方法
        /// </summary>
        /// <param name="s"></param>
        private void ChangeColor(bool s)
        {
            GetComponent<SpriteRenderer>().color = (s ? Color.red : Color.white);
        }

    }
}