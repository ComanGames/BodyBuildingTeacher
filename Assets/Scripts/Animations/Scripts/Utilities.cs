using UnityEngine;

namespace Assets.Scripts.Animations.Scripts
{
    public static class Utilities
    {
        public static void SetWidth(this RectTransform rect, float width)
        {
           rect.offsetMax = new Vector2(-width,rect.anchorMax.y);
        }
    }
}