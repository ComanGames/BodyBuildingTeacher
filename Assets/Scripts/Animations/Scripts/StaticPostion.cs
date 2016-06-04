using UnityEngine;

namespace Assets.Scripts.Animations.Scripts
{
    public class StaticPostion : MonoBehaviour
    {
        public bool IsWorking;
        private Vector3 _startPostion;

        public void Start()
        {
            _startPostion = GetComponent<RectTransform>().position;
        }

        public void Update()
        {
            GetComponent<RectTransform>().position = _startPostion;
        }
    }
}