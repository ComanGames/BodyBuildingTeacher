using System;
using System.Collections;
using Assets.Scripts.Animations.Scripts;
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
        public CounterAnimation AnimationCounter;
        public GameObject CounterCanvas;
        public SlideConvasOut CounterRemoveAnimation;
        public TimeLineAnimation AnimationTimeLine;

        private IUiAnimation RemoveCounterAnimationInterface =>CounterRemoveAnimation;
        private IUiAnimation CounterAnimationInterface => AnimationCounter;
        private IUiAnimationExtanded LineAnimationInterface => AnimationTimeLine;

        public event Action ClickNextButton;
        public event Action ClickResetButton;
        public event Action<int> ClickButtonNumber;
        

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

        public void ClearAnwerView()
        {
            ClickResetButton?.Invoke();
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
            //Debug.Log("We done game");
            QuestionText.text = "Level Complete";
            StartCoroutine(LoadNewScen());

        }

        public IEnumerator LoadNewScen()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(6);

        }
        public void StartCounterAnimation(Action callbackAction)
        {
            CounterRemoveAnimation.transform.parent.gameObject.SetActive(true);
            CounterAnimationInterface.StartAnimation();
            CounterAnimationInterface.AniamtionDone += callbackAction;
        }

        public void FadeOutCounterAnimation(Action startTime)
        {
            RemoveCounterAnimationInterface.AniamtionDone += startTime;
            RemoveCounterAnimationInterface.StartAnimation();
        }

        public void SetTimeLineEndAction(Action callbackaction)
        {
            LineAnimationInterface.AniamtionDone += callbackaction;
        }
        public void StartTimeLineAnimation()
        {
            LineAnimationInterface.ResetAnimation();
            LineAnimationInterface.StartAnimation();
        }
    }
}
