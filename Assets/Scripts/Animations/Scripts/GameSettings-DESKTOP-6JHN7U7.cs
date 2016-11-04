using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Animations.Scripts
{

    public class GameSettings
    {

        public static GlobalSettings Settings
        {
            get
            {
                if (_setting == null)
                {
                    if (IsDataFile(GlobalSettingFile))
                    {
                        _setting = LoadLevelSettings(GlobalSettingFile);
                    }
                    else
                    {
                        _setting = new GlobalSettings();
                        SaveLevelSetings(GlobalSettingFile, _setting);
                    }
                }
                return _setting;
            }
            set
            {
                SaveLevelSetings(GlobalSettingFile,value);
                _setting = value;
            }
        }

        private static GlobalSettings _setting;

        private const string GlobalSettingFile = "GlobalSettings.data";

        private const string ScoreFileName = "HighScore.score";

        private static int? _realScore;

        public static int PlayerScore
        {
            get
            {
                CheckIsFile();
                if (_realScore == null)
                    throw new Exception("You didn't load file with score");
                return (int) _realScore;
            }
        }


        public static void ScoreAdd(int value)
        {
            CheckIsFile();
            _realScore += value;
            string fileAddress = Application.persistentDataPath + @"\" + ScoreFileName;
            using (var fs = File.OpenWrite(fileAddress))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs,new ScoreInfo(PlayerScore));
            }
        }

        private static void CheckIsFile()
        {
            string fileAddress = Application.persistentDataPath + @"\"+ ScoreFileName;
            if (File.Exists(fileAddress))
            {
                if (_realScore == null)
                {
                    int highScore;
                    using (var fs = File.OpenRead(fileAddress))
                    {
                        var bf = new BinaryFormatter();
                        if (new FileInfo(fileAddress).Length>0)
                        {
                            object desiriledData = bf.Deserialize(fs);
                            highScore = ((ScoreInfo)desiriledData).Score;
                        }
                        else
                        {
                            highScore = 0;
                        }
                    }
                    _realScore = highScore;
                }
            }
            else
            {
                var file = File.Create(fileAddress);
                file.Close();
                _realScore = 0;
            }
        }

        public static bool IsDataFile(string name)
        {
            var filePath = FilePathFromName(name);
            return File.Exists(filePath);
        }

        private static string FilePathFromName(string name)
        {
            return Application.persistentDataPath + @"\" + name;
        }

        public static void SaveLevelSetings(string name, GlobalSettings globalSettings)
        {
            using (var fs = File.OpenWrite(FilePathFromName(name)))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, globalSettings);
            }
        }

        public static GlobalSettings LoadLevelSettings(string fileName)
        {
            string fileAddress = FilePathFromName(fileName);
            GlobalSettings globalSettings;
            using (var fs = File.OpenRead(fileAddress))
            {
                var bf = new BinaryFormatter();
                globalSettings = (GlobalSettings) bf.Deserialize(fs);
            }
            return globalSettings;
        }
    }

    [Serializable]
    public class ScoreInfo 
    {
        public ScoreInfo() { }
        public ScoreInfo(int score)
        {
            Score = score;
        }

        public int Score { get; set; }
    }
    [Serializable]
    public class GlobalSettings
    {
        public bool IsIntroduction { get; set; } = true;

        public bool IsSound { get; set; } = true;

    }
}