using System;
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
    public class TimeLineAnimation:MonoBehaviour
    {
        public float Time;
        public TimeColor[] TimeColors;
        public event Action AniamtionDone;
        private Image _incideImage;
        private RectTransform _ourConavas;
        private int _currentIndex;
        private float _currentLerpDelta = 1;
        private float _realWidth;

        public void Awake()
        {
            StartAnimation();
        }

        public void StartAnimation()
        {
           _incideImage = transform.GetChild(0).GetComponent<Image>();
            _ourConavas = GetComponent<RectTransform>();
            _realWidth = _incideImage.rectTransform.sizeDelta.x;
            _currentIndex = 0;
            DOTween.Init();
            GoNextAnim();
        }

        private void GoNextAnim()
        {
            DOVirtual.Float(1f, 0f,Time,ImageToVAriable).OnComplete((()=> {AniamtionDone?.Invoke(); }));
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
                _incideImage.rectTransform.sizeDelta = new Vector2(f*_realWidth,_incideImage.rectTransform.sizeDelta.y);

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
    }
}