using UnityEngine;
using UnityEngine.Audio;
using YG;

namespace blocks
{
    public class ChangeMusicStateUI : MonoBehaviour
    {
        [SerializeField] private string audioMixerParameter;
        [SerializeField] private AudioMixerGroup _mixerGroup;
        [SerializeField] private GameObject _volumeBlockImage;

        public void Init()
        {
            _mixerGroup.audioMixer.GetFloat(audioMixerParameter, out var value);
            if (-79 > value)
            {
                _volumeBlockImage.SetActive(true);
            }
        }

        public void ChangeVolumeState()
        {
            if (_volumeBlockImage.activeSelf)
            {
                _volumeBlockImage.SetActive(false);
                _mixerGroup.audioMixer.SetFloat(audioMixerParameter, -80);
            }
            else
            {
                _volumeBlockImage.SetActive(true);
                _mixerGroup.audioMixer.SetFloat(audioMixerParameter, 0);
            }
        }
    }
}