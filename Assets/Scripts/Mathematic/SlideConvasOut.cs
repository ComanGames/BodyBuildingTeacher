using System;
using System.Collections.Generic;
using Assets.Scripts.Animations.Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    [RequireComponent(typeof(Image))]
    public class SlideConvasOut : MonoBehaviour,IUiAnimation
    {
        public GameObject OurCanvas;
        public Vector2 Direction;
        public float Time;
        public event Action AniamtionDone;
        private Dictionary<int, Vector3> _realPostionsOfChilds;
        public void StartAnimation()
        {
            _realPostionsOfChilds = new Dictionary<int, Vector3>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform childTransform = transform.GetChild(i);
                _realPostionsOfChilds.Add(childTransform.GetHashCode(),childTransform.position);
            }
            Direction += new Vector2(0.5f,0.5f);
            Vector2 ourSize = GetComponent<RectTransform>().rect.size;
            Vector3 moveTo = new Vector3(ourSize.x*Direction.x, ourSize.y*Direction.y,0);
            transform.DOMove(moveTo, Time).OnComplete(() => { OurCanvas.SetActive(false); AniamtionDone?.Invoke(); }).OnUpdate(() =>
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform childTransform = transform.GetChild(i);
                    childTransform.position = _realPostionsOfChilds[childTransform.GetHashCode()];
                }

            }); }
    }
}