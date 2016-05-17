using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollerSetter : MonoBehaviour
    {

        public float StartPosition = 0;
        public void Start ()
        {
            ScrollRect scrollRect = GetComponent<ScrollRect>();
            scrollRect.verticalNormalizedPosition = StartPosition;
        }
    }
}
