using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets.Scripts.Mathematic
{
    public class Ads : MonoBehaviour
    {
        //public variables
        public static Ads Instance;
         
        //private variables
        public void Start()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public void StartAds()
        {
            StartCoroutine(ShowAdWhenReady());
        }
        IEnumerator ShowAdWhenReady()
        {
            while (!Advertisement.IsReady())
            {
                Debug.Log($"Is ads Initilized {Advertisement.isInitialized}"); 
                Debug.Log($"Is ads Ready {Advertisement.IsReady()}");
                yield return null;
                
            }
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }
    }
}