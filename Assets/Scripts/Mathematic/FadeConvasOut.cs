using System;
using Assets.Scripts.Animations.Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class FadeConvasOut : MonoBehaviour,IUiAnimation
    {
        //public variables
        public Text IncideText;
        public Image BgImage;
        public float FadeOutTime;
        public GameObject CounterCanvas;

        public event Action AniamtionDone;

        public void StartAnimation()
        {

            DOVirtual.Float(1.0f, 0.0f, FadeOutTime, FadeUpdate).OnComplete(()=> {CounterCanvas.SetActive(false); AniamtionDone?.Invoke();});
        }

        private void FadeUpdate(float value)
        {
            BgImage.color = new Color(1, 1, 1, value);
            IncideText.color = new Color(IncideText.color.r,IncideText.color.g,IncideText.color.b,value);
        }
    }
}