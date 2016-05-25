using System;
using MyAsyncUtilites;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class FibonachiManager : MonoBehaviour 
    {
        public UiManager ManagerUi;
        public Toggle IsAsync;
        public string text;

        public void Start()
        {
            ManagerUi.Clear();
            ManagerUi.ClickButtonNumber += NumberInput;
            ManagerUi.ClickNextButton += CountFi;
        }
        public  void CountFi()
        {
            int inputNumber = Int32.Parse(text);
            text ="";
            var isOn = IsAsync.isOn;
             OutputFibobanchiAsyncOrNot(isOn, inputNumber);
        }

        public void NumberInput(int number)
        {
            text += number.ToString();
            ManagerUi.ShowQuestion(text);
        }

        private async void OutputFibobanchiAsyncOrNot(bool isOn, int inputNumber)
        {
            int answer = 0;
            if (isOn)
            {
                answer = await FibonachiAsyncUtilities.FibonachiAsync(inputNumber);
            }
            else
            {
                answer = FibonachiAsyncUtilities.Fibonachi(inputNumber);
            }
                ManagerUi.Clear();
                ManagerUi.UpdateAnswerView(answer.ToString());

        }
    }
}