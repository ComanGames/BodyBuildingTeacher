﻿using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Animations.Scripts
{
    public class SimpleAnimation : MonoBehaviour, IUiAnimation
    {
        public event Action AniamtionDone;
        public Vector2 Direction;
        public float AnimationTime;
        private float _realHeigth;
        public void Start()
        {
            DOTween.Init();
        }
        public void StartAnimation()
        {
            Vector2 ourSize = GetComponent<RectTransform>().rect.size;
            Vector3 moveTo = new Vector3(ourSize.x * Direction.x, ourSize.y * Direction.y, 0);
            transform.DOMove(moveTo, 2).OnComplete(()=>AniamtionDone?.Invoke());
        }

        //mine
        public void StartPosition()
        {
            Vector2 ourSize = GetComponent<RectTransform>().rect.size;
            Vector3 moveTo = new Vector3(ourSize.x*0.5f,ourSize.y*1.5f);
            transform.DOMove(moveTo, 0);
        }
    }
}