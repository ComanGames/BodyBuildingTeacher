using System;
using UnityEngine;
using System.Threading;
using System.Timers;
using UnityEngine.UI;

namespace Assets.Scripts.Animations.Scripts
{
    public class Timer : MonoBehaviour
    {
        public Text MyTimer;
        public bool TimerOn;
        private System.Timers.Timer _timer;
        private SynchronizationContext sc;
        private TimeSpan _timerTime;

        public void StartTimer()
        {
            _timer = new System.Timers.Timer(1);
            _timer.Elapsed += UpdateTimeText;
            _timer.Start();
            sc = SynchronizationContext.Current;
            _timerTime = new TimeSpan();
        }

        private void UpdateTimeText(object sender, ElapsedEventArgs e)
        {
            _timerTime = _timerTime.Add(TimeSpan.FromMilliseconds(1));

            if (_timerTime.Milliseconds % 64 != 0)
            {
                return;
            }
            //update the label value
            sc.Post((s) =>
            {
                UpdateTimerText();
            }, null);
        }

        private void UpdateTimerText()
        {
            float minutes = _timerTime.Minutes;
            float seconds = _timerTime.Seconds;
            // ReSharper disable once PossibleLossOfFraction
            float fraction = _timerTime.Milliseconds;
            if (MyTimer == null)
            {
                Stop();
                return;
            }
            MyTimer.text = $"{minutes:00} : {seconds:00} : {fraction:000}";
        }

        public void OnApplicationQuit()
        {
            Stop();
        }

        public void OnLevelWasLoaded(int level)
        {
            Stop();
        }
        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Close();
                _timer = null;
                UpdateTimerText();
            }
            //MyTimer.text = "0";
        }
    }
}
