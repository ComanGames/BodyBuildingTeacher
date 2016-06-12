using System;
using Assets.Scripts.Animations.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class UiManager : MonoBehaviour
    {
        public Text GameOverText;
        public Text QuestionText;
        public Text AnswerText;
        public Text CurrentLevelText;
        public Text AnswersInfoText;
        public float FadeOutTime = 1.0f;
        public float EndTimeOut = 0.5f;
        public bool IsIntroduction;
        public bool IntrodcutionEnableAndNotNull => IsIntroduction && IntruductionAnimation != null;
        public GameObject FakeObject;
        public CounterAnimation AnimationCounter;
        public GameObject CounterCanvas;
        public SlideConvasOut CounterRemoveAnimation;
        public TimeLineAnimation AnimationTimeLine;
        public SimpleAnimation GameOverAnimation;
        public SimpleAnimation IntruductionAnimation;

        private IUiAnimation CounterAnimationInterface => AnimationCounter;
        private IUiAnimation RemoveCounterAnimationInterface => CounterRemoveAnimation;
        private IUiAnimation GameOverAnimationInterface => GameOverAnimation;
        private IUiAnimation IntroductionAnimationInterface => IntruductionAnimation; 
        private IUiAnimationExtanded LineAnimationInterface => AnimationTimeLine;
        private bool _isOver;


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
            AnswersInfoText.text = $"Correct = {right}. Incorrect = {wrong}";
            if (_isOver) { AnswersInfoText.text = "Completed"; }

        }

        public void EndGame(int right,int wrong)
        {
            _isOver = true;
            //Debug.Log("We done game");
            AnswerText.text = "Level Complete";
            LineAnimationInterface.ResetAnimation();
            //GameOverPanel.SetActive(true);
            GameOverText.text = "Congratulations!\n " +
                                "Your result:\n" +
                                $"Correct answers = {right}\n"+
                                $"Incorrect answers = {wrong}\n"+
                                "You have done it\n and now\n just get\n FUN\n with\n " +
                                "Campaign \n Adventure \n and \n Non-Stop Playing \n\n Good Luck!\n";
            GameOverAnimationInterface.StartAnimation();
        }


        public void StartCounterAnimation(Action callbackAction)
        {
            CounterRemoveAnimation.transform.parent.gameObject.SetActive(true);
            CounterAnimationInterface.StartAnimation();
            CounterAnimationInterface.AniamtionDone += callbackAction;
        }

        public void WaitForIntroduction(Action startTime)
        {
            CounterRemoveAnimation.transform.parent.gameObject.SetActive(true);
            IntroductionAnimationInterface.AniamtionDone += startTime;
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

        public void DisableIntroduction()
        {
            if(IntruductionAnimation!=null)
                IntruductionAnimation.gameObject.SetActive(false);
        }

    }
}
