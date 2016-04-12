using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ChangeScenOnClick : MonoBehaviour
    {
        public int SceneNumber;

        public void Click()
        {
            SceneManager.LoadScene(SceneNumber);
        }
    }
}
