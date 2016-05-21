using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class UiManager : MonoBehaviour
    {

        private MathManager _managerMath;
        public Text QuestionText;
        public Text AnswerText;

        public void SetMathManager(MathManager mathManager)
        {
            _managerMath = mathManager;
        }
        // Use this for initialization
        public void Start () {
	
        }
	
        // Update is called once per frame
        public void Update () {
	
        }

        public void Clear()
        {
            QuestionText.text = "";
            AnswerText.text = "";
        }

        public void ClickNextQuestion()
        {
            _managerMath.AskQuestion();
            AnswerText.text = "";
        }

        public void ClickNumberButton(int number)
        {
            AnswerText.text += number;
        }

        public void ShowQuestion(MathQuestion mathQuestion)
        {
            QuestionText.text = mathQuestion.ToString();
        }
    }
}
