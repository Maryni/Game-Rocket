using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Project.Spin
{
    public class SpinController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private Image _spinImage;
        [SerializeField] private bool _isRotating;
        [SerializeField] private float _rotationSpeed;

        #endregion Inspector variables

        #region public variables

        public UnityAction OnSpinReward;

        #endregion public variables

        #region public functions

        public void Spin()
        {
            var rand = Random.Range(1, 10f);
            StartCoroutine(TimerOn(rand));
        }

        #endregion public functions

        #region private functions

        private IEnumerator TimerOn(float value)
        {
            _isRotating = true;
            StartCoroutine(Spinning());
            yield return new WaitForSecondsRealtime(value);
            _isRotating = false;
            OnSpinReward?.Invoke();
        }
        
        private IEnumerator Spinning()
        {
            while(_isRotating)
            {
                yield return new WaitForFixedUpdate();
                var currentRotation = _spinImage.transform.rotation;
                currentRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z + (_rotationSpeed * Time.fixedDeltaTime));
                _spinImage.transform.rotation = currentRotation;
            }
            yield break;
        }

        #endregion private functions
    }
}