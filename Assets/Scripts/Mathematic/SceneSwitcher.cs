using Assets.Scripts.Animations.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Mathematic
{
    public class SceneSwitcher : MonoBehaviour
    {
        public SlideConvasOut SceneLider;
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public static void LoadNewScene(int number)
        {
            SceneManager.LoadScene(number);
        }

    
}
}