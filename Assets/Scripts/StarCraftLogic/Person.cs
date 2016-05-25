using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.WSA;

namespace Assets.Scripts.StarCraftLogic
{
    class Person
    {
        public event Action Attacked;
        public  Action AttackedSome;
        public  List<Action> Todo;

        public Person()
        {

            Attacked += OnAttacked;
        }

        public void OnOver(int write, Action OnEnd)
        {
            for (int i = 0; i < write; i++)
            {
                Console.WriteLine(i); 
            }
            OnEnd();

        }
        public void DoNext()
        {
           Todo[0].Invoke();
            Todo.RemoveAt(0);
        }

        public void Update()
        {
            
        }
        public void Move()
        {
           Attack(Update); 
        }
        public void Attack(Action doAfter)
        {
            //code for fight
        }

        public void RunAway()
        {
            Todo.Add(Move);
            Todo.Add(Move);
            Todo.Add(Move);
            Todo.Add(Move);
        }
        private void OnAttacked()
        {
            Todo.Add(Move);
        }
    }
}
