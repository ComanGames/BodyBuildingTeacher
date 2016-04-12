using UnityEngine;

namespace Assets.Scripts
{
    public class RotateAroun:MonoBehaviour
    {
        public Vector3 AxisToRotateAround;
        public float Speed;

        public void Update()
        {
           transform.Rotate(AxisToRotateAround,Speed); 
        }
        
    }
}