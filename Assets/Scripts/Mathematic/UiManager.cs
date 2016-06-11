using System;
using System.Collections;
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
        public Text CurrentLevelText;
        public Text AnswersInfoText;
        public float FadeOutTime = 1.0f;
        public CounterAnimation AnimationCounter;
        public GameObject CounterCanvas;
        public SlideConvasOut CounterRemoveAnimation;
        public TimeLineAnimation AnimationTimeLine;
        public SimpleAnimation AnimationSimple;

        private IUiAnimation RemoveCounterAnimationInterface => CounterRemoveAnimation;
        private IUiAnimation CounterAnimationInterface => AnimationCounter;
        private IUiAnimation SimpleAnimationInterface => AnimationSimple;
        private IUiAnimationExtanded LineAnimationInterface => AnimationTimeLine;


        public event Action ClickNextButton;
        public event Action ClickResetButton;
        public event Action<int> ClickButtonNumber;



        public void Awake()
        {
            CurrentLevelText.text = Utilities.GetSceneName();
           
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
            LineAnimationInterface.ResetAnimation();
            QuestionText.text = questoin;
        }


        public void WrongAnswar()
        {
            Debug.Log($"Wrong Answer total");
        }

        public void RightAnswer()
        {
            Debug.Log($"Right Answer total");
        }

        public void SetWrongWrite(int right, int wrong)
        {
            AnswersInfoText.text = $"RA = {right}. WA = {wrong}";
        }

        public void EndGame()
        {
            //Debug.Log("We done game");
            AnswerText.text = "Level Complete";
            LineAnimationInterface.ResetAnimation();
            SimpleAnimationInterface.StartAnimation();
            StartCoroutine(LoadNewScen());

        }

        public IEnumerator LoadNewScen()
        {
            //CurrentLevelText.text = Utilities.GetSceneName();
            yield return new WaitForSeconds(5);
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
