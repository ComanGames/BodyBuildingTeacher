using System;
using Assets.Scripts.Mathematic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Animations.Scripts
{
    [Serializable]
    public class TimeColor
    {
        public float Procent;
        public Color ColorLine;
    }


    public class TimeLineAnimation:MonoBehaviour,IUiAnimationExtanded
    {
        public float StartTime;
        public TimeColor[] TimeColors;
        public event Action AniamtionDone;
        private Image _incideImage;
        private RectTransform _ourConavas;
        private int _currentIndex;
        private float _currentLerpDelta = 1;
        private float _realWidth;
        private Tweener _ourTweener;
        


        public void StartAnimation()
        {
            _currentIndex = 0;
            Debug.Log("Animation should start");
            GoNextAnim();
        }

        public void Start()
        {
            DOTween.Init();
            _incideImage = transform.GetChild(0).GetComponent<Image>();
            _ourConavas = GetComponent<RectTransform>();
            _realWidth = _incideImage.rectTransform.rect.width;
        }

        private void GoNextAnim()
        {
           _ourTweener =  DOVirtual.Float(1f, 0f,StartTime,ImageToVAriable);
            _ourTweener.OnComplete(()=> {AniamtionDone?.Invoke(); });
        }

        private void ImageToVAriable(float f)
        {
            if (f <=GetNextElement().Procent)
            {
                _currentIndex++;
                _currentLerpDelta = 1/(TimeColors[_currentIndex].Procent-GetNextElement().Procent );
                return;
            }
            else
            {
                _incideImage.rectTransform.SetWidth( _realWidth-(f * _realWidth));

                float lerp = 1-((f - GetNextElement().Procent)*_currentLerpDelta);
                _incideImage.color = Color.Lerp(TimeColors[_currentIndex].ColorLine, GetNextElement().ColorLine,lerp);
            }


        }

        private TimeColor GetNextElement()
        {
            if (_currentIndex >= TimeColors.Length-1)
            {
                return TimeColors[TimeColors.Length-1];
            }
            return TimeColors[_currentIndex+1];
        }

        public void ResetAnimation()
        {
            StopAnimation();
            _incideImage.rectTransform.SetWidth(_realWidth);
        }
        

        public void PauswAnimatio()
        {
            throw new NotImplementedException();
        }

        public void StopAnimation()
        {
            if (_ourTweener != null)
            {
                _ourTweener.Kill();
                _ourTweener = null;
            }
        }


    }
}