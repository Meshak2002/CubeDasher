using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ricRM.Time
{
    public class TimeReward
    {

        int timeSeconds, index = 0;
        int[] seq;
        string path;
        DateTime lastTime;
        bool isSeq = false, reset = false;

        Action<int> reward;

        /// <summary>
        /// Creates a new offline timer.
        /// </summary>
        /// <param name="rewardName">File name.</param>
        /// <param name="seconds">Number of seconds between each reward.</param>
        public TimeReward(string rewardName, int seconds)
        {
            timeSeconds = seconds;

            if (rewardName == "" || rewardName == null)
            {
                path = "timereward" + timeSeconds.ToString();
            }
            else
            {
                path = rewardName;
            }

            Load();
        }

        /// <summary>
        /// Create a new offline timer.
        /// </summary>
        /// <param name="rewardName">File name.</param>
        /// <param name="seconds">Number of seconds between each reward.</param>
        /// <param name="minutes">Minutes between each reward, it will be transform to seconds.</param>
        public TimeReward(string rewardName, int seconds, int minutes)
        {
            timeSeconds = seconds;
            timeSeconds += minutes * 60;

            if (rewardName == "" || rewardName == null)
            {
                path = "timereward" + timeSeconds.ToString();
            }
            else
            {
                path = rewardName;
            }
            
            Load();
        }

        /// <summary>
        /// Create a new offline timer.
        /// </summary>
        /// <param name="rewardName">File name.</param>
        /// <param name="seconds">Number of seconds between each reward.</param>
        /// <param name="minutes">Minutes between each reward, it will be transform to seconds.</param>
        /// <param name="hours">Hours between each reward, it will be transform to seconds.</param>
        public TimeReward(string rewardName, int seconds, int minutes, int hours)
        {
            timeSeconds = seconds;
            timeSeconds += minutes * 60;
            timeSeconds += (hours * 60) * 60;

            if (rewardName == "" || rewardName == null)
            {
                path = "timereward" + timeSeconds.ToString();
            }
            else
            {
                path = rewardName;
            }

            Load();
        }

        /// <summary>
        /// Creates a new offline timer.
        /// </summary>
        /// <param name="rewardName">File name.</param>
        /// <param name="seconds">The sequence of rewards. Exemple: First 5 seconds, second 10 seconds, third 15 seconds.</param>
        /// <param name="reset">If when the sequence runs the last index, true => goes to index 0 or false and keep on the last index.</param>
        public TimeReward(string rewardName, int[] seconds, bool reset = false)
        {
            isSeq = true;
            this.reset = reset;
            seq = seconds;

            if (rewardName == "" || rewardName == null)
            {
                path = "timereward" + seq.ToString();
            }
            else
            {
                path = rewardName;
            }

            Load();
        }

        private void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/" + path + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(Application.persistentDataPath + "/" + path + ".dat", FileMode.Open);
                SaveDateTime save = (SaveDateTime)bf.Deserialize(fs);

                lastTime = save.time;
                index = save.index;

                fs.Close();
            } else
            {
                lastTime = DateTime.Now;
                index = 0;
                Save(lastTime, index);
            }
        }

        private void Save(DateTime time, int index)
        {
            if (File.Exists(Application.persistentDataPath + "/" + path + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(Application.persistentDataPath + "/" + path + ".dat", FileMode.Create);

                SaveDateTime save = new SaveDateTime();
                save.time = time;
                save.index = index;

                bf.Serialize(fs, save);
                fs.Close();
            } else
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Create(Application.persistentDataPath + "/" + path + ".dat");

                SaveDateTime save = new SaveDateTime();
                save.time = time;
                save.index = index;

                bf.Serialize(fs, save);
                fs.Close();
            }
        }

        /// <summary>
        /// Used to check if the timer already is down to zero. If it is 0 it will call the callback added with AddCallBack() method.
        /// </summary>
        public void Check()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan ts = currentTime - lastTime;
            if (isSeq)
            {
                if (ts.TotalSeconds > seq[index])
                {
                    reward(index);
                    if (index < seq.Length)
                        index++;
                    if (index == seq.Length && reset)
                        index = 0;
                    lastTime = DateTime.Now;
                    Save(lastTime, index);
                }
            }
            else
            {
                if (ts.TotalSeconds > timeSeconds)
                {
                    reward(index);
                    lastTime = DateTime.Now;
                    Save(lastTime, 0);
                }
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
    public class SaveDateTime
    {
        public DateTime time;
        public int index;
    }
}
