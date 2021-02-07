
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