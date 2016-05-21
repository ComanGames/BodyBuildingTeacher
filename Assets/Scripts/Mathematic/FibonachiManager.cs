using MyAsyncUtilites;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class FibonachiManager : MonoBehaviour, IMathManager
    {
        public UiManager ManagerUi;
        public Toggle IsAsync;

        public void Start()
        {
            ManagerUi.SetMathManager(this);
            ManagerUi.Clear();
        }

        
        public  void AskQuestion()
        {
           
            int inputNumber = ManagerUi.GetAnswerNumber();
            var isOn = IsAsync.isOn;
             AskQuestionAsyncOrNot(isOn, inputNumber);
        }

        private async void AskQuestionAsyncOrNot(bool isOn, int inputNumber)
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
                ManagerUi.ShowQuestion(answer.ToString());

        }
    }
}