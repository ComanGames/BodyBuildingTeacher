using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Animations.Scripts
{
    public class GameSettings
    {
        private const string FileExtension = ".data";

        private const string ScoreFileName = "HighScore.score";
        private static GameSettings _instance;

        private static int? _realScore;
        private Dictionary<string, LevelSettings> _levelSettingses;

        private static GameSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameSettings();
                return _instance;
            }
        }

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

        private static Dictionary<string, LevelSettings> LevelSettingses
        {
            get
            {
                if (Instance._levelSettingses == null)
                {
                    Instance._levelSettingses = new Dictionary<string, LevelSettings>();
                    var fileInfos = new DirectoryInfo(Application.persistentDataPath).GetFiles();
                    for (var i = 0; i < fileInfos.Length; i++)
                    {
                        if (fileInfos[i].Extension == FileExtension)
                        {
                            var levelSettings = LoadLevelSettings(fileInfos[i].FullName);
                            Instance._levelSettingses.Add(fileInfos[i].Name.Replace(FileExtension, ""), levelSettings);
                        }
                    }
                }
                return Instance._levelSettingses;
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
                ScoreAdd(10);
                _realScore = 0;
            }
        }

        public static LevelSettings GetlLevelSetting(string levleName)
        {
            LevelSettings levelSettings;
            if (!LevelSettingses.TryGetValue(levleName, out levelSettings))
            {
                levelSettings = new LevelSettings();
                LevelSettingses.Add(levleName, levelSettings);
            }
            return levelSettings;
        }

        public static bool IsDataFile(string name)
        {
            var filePath = FilePathFromName(name);
            return File.Exists(filePath);
        }

        private static string FilePathFromName(string name)
        {
            return Application.persistentDataPath + @"\" + name + FileExtension;
        }

        public static void SaveLevelSetings(string name, LevelSettings levelSettings)
        {
            using (var fs = File.OpenWrite(FilePathFromName(name)))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, levelSettings);
            }
        }

        public static LevelSettings LoadLevelSettings(string fileAddress)
        {
            LevelSettings levelSettings;
            using (var fs = File.OpenRead(fileAddress))
            {
                var bf = new BinaryFormatter();
                levelSettings = (LevelSettings) bf.Deserialize(fs);
            }
            return levelSettings;
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
    public class LevelSettings
    {
        public bool IsIntroduction { get; set; } = true;
    }
}