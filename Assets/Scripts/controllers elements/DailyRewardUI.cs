using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class DailyRewardUI : MonoBehaviour
    {
        public Image BackgroundImage;
        public TMP_Text DayText;
        public TMP_Text RewardText;

        private void OnValidate()
        {
            if (BackgroundImage == null)
            {
                BackgroundImage = transform.GetChild(0).gameObject.GetComponent<Image>();
            }
        
            if (DayText == null)
            {
                DayText = transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
            }
        
            if (RewardText == null)
            {
                RewardText = transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
            }
        }
    }
}