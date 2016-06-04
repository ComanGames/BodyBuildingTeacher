using System;
using Assets.Scripts.Animations.Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class UiManager : MonoBehaviour
    {

        public Text QuestionText;
        public Text AnswerText;
        public float FadeOutTime = 1.0f;
        public CounterAnimation CounterAnimationScript;
        public Image CounterBgImage;
        public GameObject CounterCanvas;
        private Text _counterText;

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
        public void Start ()
        {

            _counterText = CounterAnimationScript.GetComponent<Text>();
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

        public void StartCounterAnimation(Action callbackAction)
        {
            CounterAnimationScript.StartAnimation();
            CounterAnimationScript.AnimationEnd += callbackAction;
        }

        public void FadeOutCounterAnimation(Action askQuestion)
        {
            DOVirtual.Float(1.0f, 0.0f, FadeOutTime, FadeUpdate).OnComplete(()=> {CounterCanvas.SetActive(false); askQuestion();});
        }

        private void FadeUpdate(float value)
        {
            CounterBgImage.color = new Color(1, 1, 1, value);
            _counterText.color = new Color(_counterText.color.r,_counterText.color.g,_counterText.color.b,value);
        }
    }
}
