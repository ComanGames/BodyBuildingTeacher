using UnityEngine.UI;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Mathematic
{
    public class MathPlugin4Button:MathPlugin
    {
        public Button[] Buttons;
        private int _rightAnswerIndex;
        public int RandomFloat = 5;

        public override void AskQuestion()
        {
            base.AskQuestion();
            Random random = new Random();
            int buttonIndex = random.Next(Buttons.Length);
            Buttons[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = GetLastQuestion.Answer.ToString();
            _rightAnswerIndex = buttonIndex;
            for (int i = 0; i < Buttons.Length; i++)
            {
                if (Buttons[i].GetComponent<RandomButton>() == null)
                {
                    RandomButton rb = Buttons[i].gameObject.AddComponent<RandomButton>();
                    rb.Index = i;
                    rb.Plugin = this;
                    Buttons[i].onClick.AddListener(rb.OnClick);
                }

                if (i != buttonIndex)
                {
                    Buttons[i].transform.GetChild(0).GetComponent<Text>().text = (GetLastQuestion.Answer - random.Next(1, RandomFloat)).ToString();
                }
            }
        }

        public override void EndGame()
        {
            base.EndGame();
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].gameObject.SetActive(false);
            }
        }

        public void ButtonClicked(int index)
        {
            //Debug.Log($"button with index {index} was clicked");
            if (_rightAnswerIndex == index)
                RightAnswer();
            else
                WrongAnswer();

            _managerUi.SetWrongWrite(_mathManager.RightAnswer, _mathManager.WrongAnswer);
        }

        public override void Go()
        {
            _managerUi.TimerText?.StartTimer();
            _managerUi.AnimationTimeLine.StartAnimation();

        }

        public override void Ready()
        {
            base.Ready();
            _mathManager.AskQuestion();
            _managerUi.AnimationTimeLine.ResetAnimation();
        }
    }
}