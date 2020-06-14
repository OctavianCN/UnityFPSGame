using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Dropdown resolutionsDropdown;
    private bool muted = false;
    Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> resolutionsString = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resolutionsString.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionsDropdown.AddOptions(resolutionsString);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

    }
    public void SetVolume(float volume)
    {
        if(muted == false)
            audioMixer.SetFloat("volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetMute(bool check)
    {
        
        if (check)
        {
            audioMixer.SetFloat("volume", -80f);
            muted = true;
        }
        else
        {
            muted = false;
        }
    }
    public  void SetResloution(int resIndex)
    {
        Screen.fullScreen = true;
        Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height,Screen.fullScreen);
    }
    public void SetExit()
    {
        this.gameObject.SetActive(false);
    }
}
