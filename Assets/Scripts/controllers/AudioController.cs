using UnityEngine;
using UnityEngine.UI;

namespace Project.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;
        [SerializeField] private Slider _sliderVolume;

        public void ChangeVolume() => _music.volume = _sliderVolume.value;
        public void VolumeOn() => _music.volume = 1f;
        public void VolumeOff() => _music.volume = 0f;
        public void PlayMusic() => _music.Play();
    }
}