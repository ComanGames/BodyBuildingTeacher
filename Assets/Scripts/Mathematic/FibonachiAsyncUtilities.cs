using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace MyAsyncUtilites
{
    public static class FibonachiAsyncUtilities
    {
        public static Task<int> FibonachiAsync(int number)
        {
            return Task<int>.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                int fi = Fibonachi(number);
                sw.Stop();
                Debug.Log($"Fi({number}) take {Math.Round((double)sw.ElapsedMilliseconds/1000,2)} seconds");
                return fi;
            });
        }

        public static int Fibonachi(int number)
        {
            Stopwatch sw = new Stopwatch();
            if (number <1)
                return  0;
            if (number == 1)
                return 1;
            if (number == 2)
                return  1;
            return Fibonachi(number-2) + Fibonachi(number-1);

        }
    }
}