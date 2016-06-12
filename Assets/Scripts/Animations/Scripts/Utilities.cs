using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Animations.Scripts
{
    public static class Utilities
    {
        public static void SetWidth(this RectTransform rect, float width)
        {
           rect.offsetMax = new Vector2(-width,rect.anchorMax.y);
        }
        
        public static string GetSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}