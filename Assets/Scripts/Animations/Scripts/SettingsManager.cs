using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts.Animations.Scripts
{
    public class SettingsManager:MonoBehaviour
    {
        public Toggle IntroductionToggle;
        public Toggle SoundToggle;
        public AudioMixer Mixer;
        private ChangeScenOnClick _sceneChanger;
        

        public void Start()
        {
            _sceneChanger = GetComponent<ChangeScenOnClick>();
            IntroductionToggle.isOn = GameSettings.Settings.IsIntroduction;
            SoundToggle.isOn = GameSettings.Settings.IsSound;
        }

        public void GoHome()
        {
            GlobalSettings settings = new GlobalSettings();
            settings.IsIntroduction = IntroductionToggle.isOn;
            settings.IsSound = SoundToggle.isOn;
            if (SoundToggle.isOn)
                Mixer.SetFloat("volume", 0);
            else
                Mixer.SetFloat("volume", -80);
            GameSettings.Settings = settings;
            _sceneChanger.Click();
        }
    }
}