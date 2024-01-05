using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ricRM.Time
{
    public class ActiveTimeReward
    {
        int timeSeconds, index = 0;
        int timePass = 0;
        int[] seq;
        string path;
        bool isSeq = false, reset = false;

        Action<int> reward;

        bool run = false;

        /// <summary>
        /// Create a new active timer.
        /// </summary>
        /// <param name="rewardName">File name.</param>
        /// <param name="seconds">Number of seconds between each reward.</param>
        public ActiveTimeReward(string rewardName, int seconds)
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
        /// Create a new active timer.
        /// </summary>
        /// <param name="rewardName">File name.</param>
        /// <param name="seconds">Number of seconds between each reward.</param>
        /// <param name="minutes">Minutes between each reward, it will be transform to seconds.</param>
        public ActiveTimeReward(string rewardName, int seconds, int minutes)
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
        /// Create a new active timer.
        /// </summary>
        /// <param name="rewardName">File name.</param>
        /// <param name="seconds">Number of seconds between each reward.</param>
        /// <param name="minutes">Minutes between each reward, it will be transform to seconds.</param>
        /// <param name="hours">Hours between each reward, it will be transform to seconds.</param>
        public ActiveTimeReward(string rewardName, int seconds, int minutes, int hours)
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
        /// Creates a new active timer.
        /// </summary>
        /// <param name="rewardName">File name.</param>
        /// <param name="seconds">The sequence of rewards. Exemple: First 5 seconds, second 10 seconds, third 15 seconds.</param>
        /// <param name="reset">If when the sequence runs the last index, true => goes to index 0 or false and keep on the last index.</param>
        public ActiveTimeReward(string rewardName, int[] seconds, bool reset = false)
        {
            this.reset = reset;
            isSeq = true;
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
        
        public IEnumerator Run()
        {
            while (run)
            {
                yield return new WaitForSeconds(1f);
                timePass++;
                Check();
            }
        }

        /// <summary>
        /// Start the countdown.
        /// </summary>
        public void Start()
        {
            run = true;
            Load();
        }

        /// <summary>
        /// Stop the countdown and saves it.
        /// </summary>
        public void Stop()
        {
            Save(timePass, index);
            run = false;
        }

        /// <summary>
        /// Used to check if the timer already is down to zero. If it is 0 it will call the callback added with AddCallBack() method.
        /// </summary>
        public void Check()
        {
            if (isSeq)
            {
                if (timePass >= seq[index])
                {
                    if (index < seq.Length)
                        index++;
                    if (index == seq.Length && reset)
                        index = 0;
                    reward(index);
                    timePass = 0;
                }
            }
            else
            {
                if (timePass >= timeSeconds)
                {
                    reward(index);
                    timePass = 0;
                }
            }
            Save(timePass, index);
        }

        private void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/" + path + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(Application.persistentDataPath + "/" + path + ".dat", FileMode.Open);
                SaveRewardActive save = (SaveRewardActive)bf.Deserialize(fs);

                timePass = save.timePass;
                index = save.index;

                fs.Close();
            }
            else
            {
                timePass = 0;
                index = 0;
                Save(timePass, index);
            }
        }

        private void Save(int time, int index)
        {
            if (File.Exists(Application.persistentDataPath + "/" + path + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(Application.persistentDataPath + "/" + path + ".dat", FileMode.Create);

                SaveRewardActive save = new SaveRewardActive();
                save.timePass = time;
                save.index = index;

                bf.Serialize(fs, save);
                fs.Close();
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Create(Application.persistentDataPath + "/" + path + ".dat");

                SaveRewardActive save = new SaveRewardActive();
                save.timePass = time;
                save.index = index;

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
    public class SaveRewardActive
    {
        public int timePass;
        public int index;
    }
}
