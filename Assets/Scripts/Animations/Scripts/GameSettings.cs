using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Animations.Scripts
{
    public class GameSettings { 
    
        private const string FileExtension = ".data";
        private static GameSettings _instance;
        private static GameSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameSettings();
                return _instance;
            }
        }

        private Dictionary<string, LevelSettings> _levelSettingses;

        private static Dictionary<string, LevelSettings> LevelSettingses
        {
            get
            {
                if (Instance._levelSettingses == null)
                {
                    Instance._levelSettingses = new Dictionary<string, LevelSettings>();
                    FileInfo[] fileInfos = new DirectoryInfo(Application.persistentDataPath).GetFiles();
                    for (int i = 0; i < fileInfos.Length; i++)
                    {
                        if (fileInfos[i].Extension == FileExtension)
                        {
                            LevelSettings levelSettings = LoadLevelSettings(fileInfos[i].FullName);
                            Instance._levelSettingses.Add(fileInfos[i].Name.Replace(FileExtension,""),levelSettings);
                        }
                    }
                }
                return Instance._levelSettingses;
            }
        }

        public static LevelSettings GetlLevelSetting(string levleName)
        {
            LevelSettings levelSettings;
            if (!LevelSettingses.TryGetValue(levleName,out levelSettings))
            {
                levelSettings = new LevelSettings();
                LevelSettingses.Add(levleName,levelSettings);
            }
            return levelSettings;
        }

        public bool IsDataFile(string name)
        {
            string filePath = FilePathFromName(name);
            return File.Exists(filePath);
        }

        private static string FilePathFromName(string name)
        {
            return Application.persistentDataPath + @"\" + name + FileExtension;
        }

        public static void SaveLevelSetings(string name, LevelSettings levelSettings)
        {
            using (FileStream fs = File.OpenWrite(FilePathFromName(name)))
            {
               BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs,levelSettings);
            }
    }

        public static LevelSettings LoadLevelSettings(string fileAddress)
        {
            LevelSettings levelSettings;
            using (FileStream fs = File.OpenRead(fileAddress))
            {
                BinaryFormatter bf = new BinaryFormatter();
                levelSettings = (LevelSettings)bf.Deserialize(fs);
            }
            return levelSettings;
        }
    }

    [Serializable]
    public class LevelSettings
    {
        public bool IsIntroduction { get; set; } = true;
    }
}

