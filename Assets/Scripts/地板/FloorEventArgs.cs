using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Floor
{

    public class FloorEventArgs : System.EventArgs
    {
        public bool[] newStatus;
        //public int positionI;
        //public Vector2 positionV;

        public FloorEventArgs(bool[] status)
        {
            newStatus = status;
        }
    }
}