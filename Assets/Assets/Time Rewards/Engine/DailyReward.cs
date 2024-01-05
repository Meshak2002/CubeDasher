using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ricRM.Time
{
    public class DailyReward
    {
        int dia = 1, max = 1;
        string path;
        DateTime lastTime;

        Action<int> reward;

        /// <summary>
        /// Creates a new Daily reward.
        /// </summary>
        /// <param name="rewardName">File name.</param>
        /// <param name="maxDays">The max number of days of the reward. Default 1.</param>
        public DailyReward(string rewardName, int maxDays = 1)
        {
            max = maxDays;

            if (rewardName == "" || rewardName == null)
            {
                path = "timereward" + maxDays.ToString();
            }
            else
            {
                path = rewardName;
            }

            Load();
        }

        /// <summary>
        /// Used to check if the has pass a day or more.
        /// </summary>
        public void Check()
        {
            DateTime now = DateTime.Now;
            TimeSpan ts = now - lastTime;
            Debug.Log("total: " + ts.TotalSeconds);
            if (ts.TotalSeconds >= 86400 && ts.TotalSeconds < 172800)
            {
                lastTime = DateTime.Now;

                reward(dia);

                if (dia == max)
                {
                    dia = 1;
                }
                else
                {
                    dia++;
                }

                Save(lastTime, dia);
            } else if (ts.TotalSeconds >= 172800)
            {
                dia = 1;
                lastTime = DateTime.Now;
                reward(dia);
                dia++;
                Save(lastTime, dia);
            }
        }

        private void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/" + path + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(Application.persistentDataPath + "/" + path + ".dat", FileMode.Open);
                SaveDaily save = (SaveDaily)bf.Deserialize(fs);

                lastTime = save.time;
                dia = save.dia;

                fs.Close();
            }
            else
            {
                lastTime = DateTime.Now;
                dia = 0;
                Save(lastTime, dia);
            }
        }

        private void Save(DateTime time, int dia)
        {
            if (File.Exists(Application.persistentDataPath + "/" + path + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(Application.persistentDataPath + "/" + path + ".dat", FileMode.Create);

                SaveDaily save = new SaveDaily();
                save.time = time;
                save.dia = dia;

                bf.Serialize(fs, save);
                fs.Close();
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Create(Application.persistentDataPath + "/" + path + ".dat");

                SaveDaily save = new SaveDaily();
                save.time = time;
                save.dia = dia;

                bf.Serialize(fs, save);
                fs.Close();
            }
        }

        /// <summary>
        /// This is used to add an callback to when the timer get down to zero.
        /// </summary>
        /// <param name="callback">The function that will be called back.</param>
        public void AddCallBack(Action<int> callback)
        {
            reward += callback;
        }

        /// <summary>
        /// Removes the callback from being called when the timer get down to zero.
        /// </summary>
        /// <param name="callback">Function.</param>
        public void RemoveCallBack(Action<int> callback)
        {
            reward -= callback;
        }
    }

    [Serializable]
    public class SaveDaily
    {
        public DateTime time;
        public int dia;
    }
}
