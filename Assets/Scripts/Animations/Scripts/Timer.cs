﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts.Animations.Scripts
{
    public class Timer : MonoBehaviour
    {
        private float _time;

        public Text MyTimer;

        public bool TimerOn = true;

        void Update()
        {
            if (TimerOn)
            {
                _time += Time.deltaTime;

                float minutes = _time/60f;
                float seconds = _time%60f;
                float fraction = (_time*100)%100;

                //update the label value
                MyTimer.text = string.Format("{0:00} : {1:00} : {2:00}", minutes, seconds, fraction);
            }
            else
            {
                _time = 0;
            }
        }

        public void Stop()
        {
            TimerOn = false;
        }
    }
}
