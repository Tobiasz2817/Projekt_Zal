using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolutionDropdown; 
    [SerializeField]
    private TMP_Dropdown qualityDropdown;

    [SerializeField] 
    private AudioMixer mixer;
    [SerializeField] 
    private AudioMixer hitMixer;
    
    private Resolution[] resolutions; 
     
    private void Awake()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        
        QualitySettings.vSyncCount = 1;

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray(); 
        resolutionDropdown.ClearOptions(); 
         
        List<string> options = new List<string>(); 
        int currentResolutionIndex = 0; 
        
        for (int i= 0; i<resolutions.Length; i++) 
        {
            string option = resolutions[i].width + " x " + resolutions[i].height; 
            options.Add(option); 
             
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height ) 
            {    
                currentResolutionIndex = i; 
            } 
        }       
        resolutionDropdown.AddOptions(options); 
        resolutionDropdown.value = currentResolutionIndex; 
        resolutionDropdown.RefreshShownValue(); 
    } 
    public void SetResolution(int resolutionIndex) 
    { 
        Resolution resolution = resolutions[resolutionIndex]; 
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); 
    }
    public void SetQuality(int qualityIndex) 
    { 
        QualitySettings.SetQualityLevel(qualityIndex +1); 
    } 
    public void SetFullscreen(bool isFullscreen) 
    { 
        Screen.fullScreen = isFullscreen; 
    }

    public void SetVolume(float sliderVol)
    {
        mixer.SetFloat("Volume", sliderVol);
        hitMixer.SetFloat("Hit", sliderVol);
    }
}
