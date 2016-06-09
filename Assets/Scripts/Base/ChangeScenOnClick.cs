using Assets.Scripts.Mathematic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ChangeScenOnClick : MonoBehaviour
    {
        public int SceneNumber;

        public void Click()
        {
            SceneSwitcher.LoadNewScene(SceneNumber);
        }
    }
}
