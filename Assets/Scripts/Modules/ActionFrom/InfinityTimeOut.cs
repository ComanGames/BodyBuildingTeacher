using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Modules.ActionFrom
{
    public class InfinityTimeOut : Worker
    {
        public float FirstTimeOut = 5f;
        public float SecondTimeOut = 2f;
        public bool WorkOnFirst = false;

        public void Start()
        {
            StartCoroutine(InfinityWork());
        }

        private IEnumerator InfinityWork()
        {
            if (WorkOnFirst)
            {
                IsWorking = true;
                yield return new WaitForSeconds(FirstTimeOut);
            }
            else
            {
               yield return new WaitForSeconds(FirstTimeOut);
            }
            while (true)
            {
                if (WorkOnFirst)
                {
                    if (!IsWorking)
                    {
                        IsWorking = true;
                        yield return new WaitForSeconds(FirstTimeOut);
                    }
                    else if (IsWorking)
                    {
                        IsWorking = false;
                        yield return new WaitForSeconds(SecondTimeOut);
                    }
                }
                // if WorkOnFirst = false;
                else
                {
                    if (!IsWorking)
                    {
                        IsWorking = true;
                        yield return new WaitForSeconds(SecondTimeOut);
                    }
                    else if (IsWorking)
                    {
                        IsWorking = false;
                        yield return new WaitForSeconds(FirstTimeOut);
                    }

                }
            } 
        }
    }
}