using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class controller: MonoBehaviour {

  public Dropdown resolutionDropdown;
  Resolution[] resolutions;

  void Start() {
    resolutionDropdown.ClearOptions();
    List<string> options = new List<string>();
    resolutions = Screen.resolutions;
    int currentResolutionIndex = 0;

    for (int i = 0; i < resolutions.Length; i++) {
      string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
      options.Add(option);
      if (resolutions[i].width == Screen.currentResolution.width &&
        resolutions[i].height == Screen.currentResolution.height)
        currentResolutionIndex = i;
    }

    resolutionDropdown.AddOptions(options);
    resolutionDropdown.RefreshShownValue();
    LoadSettings(currentResolutionIndex);
  }

  public void SetResolution(int resolutionIndex) {
    Resolution resolution = resolutions[resolutionIndex];
    Screen.SetResolution(resolution.width,
      resolution.height, Screen.fullScreen);
  }

  public void SaveSettings() {
    PlayerPrefs.SetInt("ResolutionPreference",
      resolutionDropdown.value);
  }

  public void LoadSettings(int currentResolutionIndex) {
    if (PlayerPrefs.HasKey("ResolutionPreference"))
      resolutionDropdown.value =
      PlayerPrefs.GetInt("ResolutionPreference");
    else
      resolutionDropdown.value = currentResolutionIndex;
  }
}