using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Daily
{
    public class DailyController : MonoBehaviour
    {
        
        #region properties
        
        public int CountDayReward { get; private set; }

        #endregion properties

        #region Inspector variables

        [SerializeField] private List<DailyReward> _dailyRewards;

        #endregion Inspector variables
        
        #region public variables

        public List<DailyReward> DailyRewards => _dailyRewards;
        public UnityAction OnDailyRewardReady;
        public UnityAction OnDailyRewardClick;

        #endregion public variables

        #region private variables

        private int lastDayGetReward;

        #endregion private variables

        #region Unity functions
        
        private void OnApplicationQuit()
        {
            SaveData();
        }

        #endregion Unity functions

        #region public functions

        public void ClickCollect() => OnDailyRewardClick?.Invoke();
        
        public int ClaimDailyReward()
        {
            var current = DailyRewards.FirstOrDefault(x => x.IsAvailable);
            current.IsClaimed = true;
            return current.Reward;
        }

        public void CheckDaily() => CheckDailyAvailable();
        
        #endregion public functions

        #region private functions

        private void CheckDailyAvailable()
        {
            string lastRewardDateStr = PlayerPrefs.GetString("LastDailyRewardDate", string.Empty);
            if (string.IsNullOrEmpty(lastRewardDateStr) || DateTime.Parse(lastRewardDateStr).Date < DateTime.Now.Date)
            {
                DateTime lastRewardDate = DateTime.Parse(lastRewardDateStr);
                TimeSpan difference = DateTime.Now.Date - lastRewardDate.Date;
                    
                var countDays= difference.Days;
                if (countDays > 0)
                {
                    if (countDays == 1)
                    {
                        CountDayReward++; 
                    }
                    else
                    {
                        CountDayReward = 1;
                    }
                }
            }

            SetAvailableDays();
        }

        private void SetAvailableDays()
        {
            for (int i = 0; i < DailyRewards.Count; i++)
            {
                if (CountDayReward > i)
                {
                    DailyRewards[i].IsAvailable = true;
                }
                else
                {
                    DailyRewards[i].IsAvailable = false;
                }
            }

            OnDailyRewardReady?.Invoke();
        }
        
        private void SaveData()
        {
            PlayerPrefs.SetString("LastDailyRewardDate", DateTime.Now.ToString("yyyy-MM-dd"));
            PlayerPrefs.Save();
        }

        #endregion private functions
    }
}

[System.Serializable]
public class DailyReward
{    
    public int Reward;
    public bool IsAvailable;
    public bool IsClaimed;
}