using System;
using Assets.Scripts.Animations.Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    [RequireComponent(typeof(Image))]
    public class SlideConvasOut : MonoBehaviour,IUiAnimation
    {
        public Transform TexTransform;
        public GameObject OurCanvas;
        public Vector2 Direction;
        public float Time;
        private Vector3 _textRealPostion;

        public event Action AniamtionDone;
        public void StartAnimation()
        {
            _textRealPostion = TexTransform.position;
            Direction += new Vector2(0.5f,0.5f);
            Vector2 ourSize = GetComponent<RectTransform>().rect.size;
            Vector3 moveTo = new Vector3(ourSize.x*Direction.x, ourSize.y*Direction.y,0);
            Debug.Log($"{moveTo}"); 

            transform.DOMove(moveTo, Time).OnComplete(() => { OurCanvas.SetActive(false); AniamtionDone?.Invoke(); }).OnUpdate(() =>
            {
                TexTransform.position = _textRealPostion;
            }); }
    }
}