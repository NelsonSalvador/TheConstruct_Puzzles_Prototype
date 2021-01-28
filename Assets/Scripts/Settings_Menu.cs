using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Controls the settings menu options.
/// </summary>
public class Settings_Menu : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] resolutions;

    public Dropdown resolutionDropdown;
    public Slider volumeSlider;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void Values()
    {
        float temp;
        audioMixer.GetFloat("Volume", out temp);
        volumeSlider.value = temp;
    }

    /// <summary>
    /// Sets screen resolution.
    /// </summary>
    /// <param name="resolutionIndex"></param>
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Sets game volume.
    /// </summary>
    /// <param name="volume">Game volume to set.</param>
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    /// <summary>
    /// Sets Quality of the game.
    /// </summary>
    /// <param name="qualityIndex">Quality to set.</param>
    public void SetQuality ( int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Sets the screen to fullscreen.
    /// </summary>
    /// <param name="isFullscreen">true is fullscrren is checked.</param>
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
