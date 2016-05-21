using Assets.Scripts.Modules.ActionFrom;
using UnityEngine;

namespace Assets.Scripts.Modules.MRS
{
    public enum MRSActions:int
    {
       Move,
       Rotate,
       Scale 
    }
    public class MoveRotateScale:MonoBehaviour
    {
        public  Worker ActionHolder;
        public MRSActions ActionsToDo;
        public Vector3 Axis;
        public float Speed;
        //Create some new class based on worker but with work do while 

        public void Update()
        {
            if (!ActionHolder.Work())
                return;
            switch (ActionsToDo)
            {
                case MRSActions.Move:
                    transform.Translate(Axis*Speed/Time.deltaTime);
                    break;
                case MRSActions.Rotate:
                    transform.RotateAround(transform.position,Axis,Speed/Time.deltaTime);
                    break;
                case MRSActions.Scale:
                    transform.localScale+=(Axis*Speed/Time.deltaTime) ;
                    break;
            }
        }
    }
}