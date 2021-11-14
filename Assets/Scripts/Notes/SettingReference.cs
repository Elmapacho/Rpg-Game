using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingReference : MonoBehaviour
{
    /*
    // Application.Quit(); (close the game)

    //Time.timeScale to pause o create slow mo effects (try to create a static variable so every scripts knows about ex: sound)
      If you want to no stop animation you can change a setting to igonore time scale.

    //If a FUNCTION in the UI menu is a float is going to get recgonice by a ui slider ex: volumen.
      Then we open a new windows call audio mixer and create different layes of volumes.

      Need using UnityEngine.audio;

      public AudioMixer audioMixer;
      public void SetVolume(float volume)
      {
      audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
      }

      instead since the audiomixer volume is not linear
      
      public void SetVolume (float volume) VA A SER RECONOCIDA POR UNITY USANDO UN SLIDER

    ////////////////////////////IMPORTANT TO USE TEXT MASH PRO BUTTON/ETC YOU NEED TO USE using TMPro; //////////////

    //SCREEN RESOLUTIONS (Drop downs menu automatecly detetcs int FUNCTIONS.)
      
        Resolution[] resolution;

        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i <resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    //FULL SCREEN (Toggles automatecly detects bool FUNCTIONS.)

      public void fullScreen (bool fullScreen)
      Screen.fullScreen = bool;

    //In order to change the graphics settings we need to chage it in the projectsetting and we get an index to those levels.
       QualitySettings.SetQualityLevel(int index of the graphic settings);
       
    //You can disable automatic resolution windowns at the start of the game in setting / player

    //if ( Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                Debug.Log("keydown");
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (menuIndex < maxMenuIndex)
                    {
                        menuIndex ++;
                    }
                    else { menuIndex = 0; }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (menuIndex > 0)
                    {
                        menuIndex --;
                    }
                    else { menuIndex = maxMenuIndex; }
                }
             keyDown = true;
            }

        }
        else { keyDown = false; }

     // How to make a loading screen

        AsyncOperation loading = SceneManager.LoadSceneAsync(scenename/index); (this load the next scene while not closing the current scene)
        loading.progress give how much of the next scene was loaded
        loading.isDon is bool which tell if the load has been completed

        loading phases: loading (0-0.9) and activating (0.9-1)
        to convert it from 0.9 to 1: float lodingProgress = Mathf.Clamp01(loaging.progress /0.9f)


    */
}
