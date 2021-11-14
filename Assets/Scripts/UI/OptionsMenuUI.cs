using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using Doozy.Engine;
using Doozy.Engine.UI;

namespace ElMapacho
{
    public class OptionsMenuUI : MonoBehaviour
    {
        private Resolution _lastResolution;
        private bool _lastFullscreen;
        private float _lastAudioVolumen;

        [SerializeField] AudioMixer _audioMixer;
        [SerializeField] TMP_Dropdown _resolutionDropDown;
        [SerializeField] Slider _volumeSlider;
        Resolution[] _resolutions;

        private UIPopup _popUpToShow;

        public void OnEnable()
        {
            Debug.Log("Options enabled");
            if (_audioMixer == null)
            {
                Debug.LogError("Missing audio mixer reference in " + gameObject.name);
            }
            if (_resolutionDropDown == null)
            {
                Debug.LogError("Missing drop down in " + gameObject.name);
            }
            if (_volumeSlider == null)
            {
                Debug.LogError("Missing slider in " + gameObject.name);
            }


            CheckResolutions();
            RememberPreviousSettings();
            _volumeSlider.value = _lastAudioVolumen;
        }
        private void CheckResolutions()
        {
            _resolutions = Screen.resolutions;
            _resolutionDropDown.ClearOptions();

            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + "x" + _resolutions[i].height;
                options.Add(option);

                if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }
            _resolutionDropDown.AddOptions(options);
            _resolutionDropDown.value = currentResolutionIndex;
            _resolutionDropDown.RefreshShownValue();
        }

        private void RememberPreviousSettings()
        {
            _lastResolution = Screen.currentResolution;
            _lastFullscreen = Screen.fullScreen;
            _audioMixer.GetFloat("Volume", out _lastAudioVolumen);
        }
        public void SetResolution(int resolutionIndex)
        {
            Resolution resolutionToSet = _resolutions[resolutionIndex];
            if (Screen.currentResolution.height == resolutionToSet.height && Screen.currentResolution.width == resolutionToSet.width)
            {
                return;
            }
            Screen.SetResolution(resolutionToSet.width, resolutionToSet.height, Screen.fullScreen);
        }

        public void FullScreen(bool fullScreen)
        {
            Screen.fullScreen = fullScreen;
        }

        public void SetVolume(float volume)
        {
            _audioMixer.SetFloat("Volume", volume);
        }

        private void SetPreviousVolume(float oldVolume)
        {
            _audioMixer.SetFloat("Volume", oldVolume);
        }

        /// <summary>
        /// Returns to the previous settings when dont want to save settings
        /// </summary>
        public void xReturnToPreviousValues()
        {
            Screen.SetResolution(_lastResolution.width, _lastResolution.height, _lastFullscreen);
            SetPreviousVolume(_lastAudioVolumen);
            Debug.Log("Returning to previous options values.");
        }
        /// <summary>
        /// Gets call by the back button.
        /// </summary>
        public void xBackOptions()
        {
            _popUpToShow = UIPopup.GetPopup(PopUpNames.SaveSettings.ToString());
            _popUpToShow.Data.SetButtonsCallbacks(xReturnToPreviousValues);
            _popUpToShow.Show();
        }
    }
}