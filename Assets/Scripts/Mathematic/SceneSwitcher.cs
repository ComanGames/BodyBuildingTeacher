using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class SceneSwitcher : MonoBehaviour
    {
        public static Transform GlobalParent;
        public static SceneSwitcher Instance;
        public float Time = 0.5f;
        private Image _ourImage;
        private bool _isFrist = true;

        public void Awake()
        {
            DontDestroyOnLoad(this);
            if (Instance == null)
                Instance = this;
            else
            {
               Destroy(gameObject); 
            }

            _ourImage = GetComponent<Image>();
            _ourImage.enabled = false;
        }

        public static void LoadNewScene(int number)
        {
            if (Instance != null)
            {
                Instance._ourImage.enabled = true;
                DOTween.Init();
                DOVirtual.Float(0, 1, Instance.Time, UpdateColor).OnComplete(() =>
                {
                    StartLoadingNewScne(number);
                });
            }
            else
            {
                SceneManager.LoadScene(number);
            }
        }
        public static void UpdateColor(float newT)
        {
            Color color = Instance._ourImage.color;
            color.a = newT;
            Instance._ourImage.color = color;
        }

        private static void StartLoadingNewScne(int number)
        {
            SceneManager.LoadScene(number);
            DOVirtual.Float(1, 0, Instance.Time, UpdateColor).OnComplete(() =>
            {
                Instance._ourImage.enabled = false;
            });
        }
    }
}