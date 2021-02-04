using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Floor
{
    /// <summary>
    /// 定義資料，給FloorUnit用
    /// </summary>
    public class FloorData
    {

        public bool status;
        public EntityUnit floor;
        public List<FloorUnit> entities;

        /// <summary>
        /// 這個地板在5x5中的哪個位置
        /// </summary>
        public Vector2 position;
        public FloorData(Vector2 position)
        {
            status = false;
            entities = new List<FloorUnit>();
            this.position = position;
        }
    }
}