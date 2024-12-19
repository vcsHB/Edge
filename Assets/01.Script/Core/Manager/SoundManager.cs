using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Core
{
    class SoundData
    {
        public float BGM;
        public float SFX;
    }
    public class SoundManager : MonoSingleton<SoundManager>
    {
        public Slider bgmSlider;
        public Slider sfxSlider;
        public AudioMixer mixer;
        private SoundData _soundData;

        [Header("SliderSetting")]
        public float minVlaue;
        public float maxVlaue;

        private void Awake()
        {

            bgmSlider.minValue = minVlaue;
            bgmSlider.maxValue = maxVlaue;

            sfxSlider.minValue = minVlaue;
            sfxSlider.maxValue = maxVlaue;

            bgmSlider.onValueChanged.AddListener(BGMValueChange);
            sfxSlider.onValueChanged.AddListener(SFXValueChange);

            _soundData = BGDJson.FromJson<SoundData>("Sound");
            if(_soundData == null)
            {
                _soundData = new SoundData();
                _soundData.BGM = maxVlaue;
                _soundData.SFX = maxVlaue;

                sfxSlider.value = maxVlaue;
                sfxSlider.value = maxVlaue;
                BGDJson.ToJson(_soundData, "Sound", true);
            }
            else
            {
                bgmSlider.value = _soundData.BGM;
                sfxSlider.value = _soundData.SFX;
            }
        }

        private void SFXValueChange(float value)
        {
            _soundData.SFX = value;
            mixer.SetFloat("Volume_SFX", value);
            
        }

        private void BGMValueChange(float value)
        {
            _soundData.BGM = value;
            mixer.SetFloat("Volume_BGM", value);
        }

        public void SaveSoundData()
        {
            BGDJson.ToJson(_soundData,"Sound",true);
        }

        private void OnDestroy()
        {
            bgmSlider.onValueChanged.RemoveListener(BGMValueChange);
        }
    }
}


