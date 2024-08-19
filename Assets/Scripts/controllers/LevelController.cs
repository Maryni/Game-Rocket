using System;
using UnityEngine;

namespace Project.Level
{
    public class LevelController : MonoBehaviour
    {

        [SerializeField] private int _countBoost;
        [SerializeField] private int _maxBoost;

        private bool isPlaying;
        private int countPerSecond;
        
        private void Start()
        {
            _countBoost = _maxBoost;
        }

        private void FixedUpdate()
        {
            if (isPlaying)
            {
                if (_countBoost > 0)
                {
                    countPerSecond++;
                    if (countPerSecond > 60)
                    {
                        _countBoost--;
                        countPerSecond = 0;
                    }
                }
            }
        }

        #region public functions

        public void MoreClicks()
        {
            
        }

        public void EnableGame() => isPlaying = true;
        public void DisableGame() => isPlaying = false;

        #endregion public functions
    }
}