using System;
using Assets.Scripts.Animations.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class UiManager : MonoBehaviour
    {
        public UiPlugin PlaginUi;
        public Text GameOverText;
        public Text QuestionText;
        public Text AnswerText;  
        public Text CurrentLevelText;
        public Text AnswersInfoText;
        public float FadeOutTime = 1.0f;
        public float EndTimeOut = 0.5f;
        public bool IsIntroduction;
        public bool IntrodcutionEnableAndNotNull => IsIntroduction && IntruductionAnimation != null;
        public CounterAnimation AnimationCounter;
        public GameObject CounterCanvas;
        public SlideConvasOut CounterRemoveAnimation;
        public TimeLineAnimation AnimationTimeLine;
        public SimpleAnimation GameOverAnimation;
        public SimpleAnimation IntruductionAnimation;
        public Toggle IntroductionToggle;
        public Timer TimerText;

        public SimpleAnimation LevelOverAnimation;
        private IUiAnimation LevelOverAnimationInterface => LevelOverAnimation;

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

            IntruductionAnimation?.transform.parent.gameObject.SetActive(true);
            LoadSettings();
            CurrentLevelText.text = Utilities.GetSceneName();
            AnswersInfoText.text = "True = 0\n" +
                                   "False = 0\n";
        }

        public void Start()
        {
            
            GameOverAnimation.StartPosition();
        }
       

        private void SaveSetting()
        {
            LevelSettings levelSettings = GameSettings.GetlLevelSetting(Utilities.GetSceneName());
            levelSettings.IsIntroduction = IntroductionToggle.isOn;
            GameSettings.SaveLevelSetings(Utilities.GetSceneName(), levelSettings);
        }

        private void LoadSettings()
        {
            LevelSettings levelSettings = GameSettings.GetlLevelSetting(Utilities.GetSceneName());
            IsIntroduction = levelSettings.IsIntroduction;
            IntroductionToggle.isOn = IsIntroduction;
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
           // LineAnimationInterface.ResetAnimation();
            QuestionText.text = questoin;
        }

        public void ShowQuestionWithAnswer(string text)
        {
            if(PlaginUi!=null)
                PlaginUi.ShowQuestionWithAnswer(text);
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
            AnswersInfoText.text = $"Right = {right}\n" +
                                   $"Wrong = {wrong}\n";
            if (_isOver)
            {
                AnswersInfoText.alignment = TextAnchor.MiddleCenter;
                AnswersInfoText.text = "Completed\n";
                TimerText?.gameObject.SetActive(false);
            }
        }

        public void EndGame(string gameOverText)
        {
            SaveSetting();
            GameSettings.ScoreAdd(100);
            _isOver = true;
            //Debug.Log("We done game");
            LineAnimationInterface.ResetAnimation();
            
            //GameOverPanel.SetActive(true);
            GameOverText.text = gameOverText;
            GameOverAnimationInterface.StartAnimation();
        }

        public void StartInputAnimation()
        {
           LevelOverAnimationInterface.StartAnimation(); 
        }

        public string GetGameOverText(int right, int wrong)
        {
            return "Congratulations!\n " +
                                "Your result:\n" +
                                $"Correct answers = {right}\n" +
                                $"Incorrect answers = {wrong}\n"+
                                $"Total time is {TimerText.MyTimer.text}";
        }
        public void StartCounterAnimation(Action callbackAction)
        {
            CounterRemoveAnimation.transform.parent.gameObject.SetActive(true);
            CounterAnimationInterface.StartAnimation();
            CounterAnimationInterface.AniamtionDone += callbackAction;

            SaveSetting();
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


        //my code
        public void StopTimeLineAnimation()
        {
            LineAnimationInterface.StopAnimation();
        }

        public void DisableIntroduction()
        {
            if(IntruductionAnimation!=null)
                IntruductionAnimation.gameObject.SetActive(false);
        }

        public void WrongAnswarsLimit()
        {
            TimerText?.gameObject.SetActive(false);
            EndGame("To much of wrong Answers");
        }
    }
}
