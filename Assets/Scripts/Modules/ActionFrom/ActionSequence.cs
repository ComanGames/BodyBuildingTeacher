using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Modules.ActionFrom
{
    [Serializable]
    public class WorkForTime
    {
        public float Time ;
        public bool IsWorking;
    }
    public class ActionSequence :Worker
    {
        public WorkForTime[] ForTime;
        public bool WorkAfterEnd;
        public void Start()
        {
            StartCoroutine(TimeOuts());
        }

        IEnumerator TimeOuts()
        {
            foreach (WorkForTime forTime in ForTime)
            {
                IsWorking = forTime.IsWorking;
                yield return new WaitForSeconds(forTime.Time);
            }
            IsWorking = WorkAfterEnd;
        } 
    }
}