using UnityEngine;

namespace Assets.Scripts.Modules.ActionFrom
{
    public class Worker:MonoBehaviour
    {

        protected bool IsWorking ;
         
        public bool Work()
        {
            return IsWorking;
        }
    }
}