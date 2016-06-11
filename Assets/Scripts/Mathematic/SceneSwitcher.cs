using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.Animations.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class SceneSwitcher : MonoBehaviour
    {
        public SlideConvasOut SceneLider;
        public static Transform GlobalParent;
        public static SceneSwitcher Instance;
        public void Start()
        {
            Instance = this;
        }


        public static void LoadNewScene(int number)
        {
            
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(number, LoadSceneMode.Additive);
            Scene newScene = SceneManager.GetSceneAt(1);
            Instance.StartCoroutine("WaitForSceneLoaded",newScene);
        }

        private void SceneIsLoaded(Scene newScene)
        {
            GameObject[] rootGameObjects = newScene.GetRootGameObjects();
            GlobalParent = new GameObject("parent for all").AddComponent<RectTransform>();
            GlobalParent.transform.position = Vector3.zero;
            GlobalParent.parent = rootGameObjects[0].transform.parent;
            foreach (GameObject rootGameObject in rootGameObjects)
            {
                rootGameObject.transform.parent = GlobalParent;
            }
            GlobalParent.gameObject.AddComponent<Image>();
        }
        private  IEnumerator WaitForSceneLoaded(Scene scene)
        {
            if (!scene.isLoaded)
            {
                yield return null;
            }
            SceneIsLoaded(scene);
        }
    }
   
}