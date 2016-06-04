using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class UiManager : MonoBehaviour
    {

        public Text QuestionText;
        public Text AnswerText;
        public Text CorrectnessText;

        public event Action ClickNextButton;
        public event Action<int> ClickButtonNumber;

        //        public delegate int GiveNumber(string input);
        //        public event GiveNumber NumberInput;

        // What is really going around with delegates.
        //        public delegate void ActionOn();
        //        public delegate void ButtonOn(int n);
        //        public event ActionOn ClickNextButton;
        //        public event ButtonOn ClickButtonNumber;




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
            ClickNextButton?.Invoke();
        }

        public void ClickNumberButton(int number)
        {
//            _managerMath.NumberInput(number);
                ClickButtonNumber?.Invoke(number);
        }

        public void UpdateAnswerView(string text)
        {
            AnswerText.text = text;
        }

        public int GetAnswerNumber()
        {
            if (AnswerText.text == "")
                return 0;
            return int.Parse(AnswerText.text);
        }

        public void ShowQuestion(string questoin)
        {
            QuestionText.text = questoin;
        }
        public void WrongAnswar()
        {
            CorrectnessText.text = "Incorrect";
            CorrectnessText.color = Color.red;
        }

        public void RightAnswer()
        {
            CorrectnessText.text = "Correct";
            CorrectnessText.color = Color.green;
        }

        public void EndGame()
        {
            Debug.Log("We done game");
            SceneManager.LoadScene(0);
        }
    }
}
