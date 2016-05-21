﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class UiManager : MonoBehaviour
    {

        private IMathManager _managerMath;
        public Text QuestionText;
        public Text AnswerText;

        public void SetMathManager(IMathManager mathManager)
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
        }

        public void ClickNumberButton(int number)
        {
            _managerMath.NumberInput(number);
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
            Debug.Log("Wrong Answer");
        }

        public void RightAnswer()
        {
           Debug.Log("Right Answer"); 
        }

        public void EndGame()
        {
            Debug.Log("We done game");
            SceneManager.LoadScene(0);
        }
    }
}
