using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Animations.Scripts
{
    public enum TypesOfTime
    {
       PerEach, 
       ByTotal,
    }

    [Serializable]
    public class CountAnimationData 
    {
        public string Text;
        public float Time;
    }

    [RequireComponent (typeof (Text))]
   public class CounterAnimation:MonoBehaviour,IUiAnimation
    {
        public AudioSource BeepSound;
        public TypesOfTime TimeTypes;
        public event Action AniamtionDone;
        public float TotalTime;
        public CountAnimationData[] AnimationTextAndTime;
        private int _currentIndex;
        private RectTransform _uiTransform;
        private Text _uiText;

        private void SetAnimation()
        {
            _uiTransform = GetComponent<RectTransform>();
            _uiText = GetComponent<Text>();

            if (TimeTypes == TypesOfTime.ByTotal)
            {
                float timeForEach = TotalTime/AnimationTextAndTime.Length;
                foreach (CountAnimationData animationData in AnimationTextAndTime)
                {
                    animationData.Time = timeForEach;
                }
            }
        }


        public void StartAnimation()
        {
            SetAnimation();
            DOTween.Init();
            GoNext();
        }

        private void GoNext()
        {
            if(_currentIndex>=AnimationTextAndTime.Length)
            {
                AniamtionDone?.Invoke();
                return;
            }
            BeepSound.Stop();
            BeepSound.Play();
            CountAnimationData currentData = AnimationTextAndTime[_currentIndex++];
            _uiText.text = currentData.Text;
            Vector3 tragetScale = transform.localScale;
            transform.localScale = Vector3.zero;
            _uiTransform.DOScale(tragetScale, currentData.Time).OnComplete(GoNext);
        }

       
    }

}
