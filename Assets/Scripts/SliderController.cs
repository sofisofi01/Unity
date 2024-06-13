using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderController: MonoBehaviour {
  [SerializeField]
  private AudioMixer soundMixer;
  [SerializeField]
  private AudioMixer musicMixer;

  public void ChangeSliderMusic(float value) {
    if (musicMixer != null) {
      musicMixer.SetFloat("MasterVolume", value);
    }
  }

  public void ChangeSliderSound(float value) {
    if (soundMixer != null) {
      soundMixer.SetFloat("MasterVolume", value);
    }
  }
}