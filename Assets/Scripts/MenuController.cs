using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController: MonoBehaviour {
  private AudioSource m_AudioSource;
  private bool isPlaying;
  [SerializeField]
  private AudioMixer musicMixer;
  [SerializeField]
  private AudioMixer soundMixer;
  [SerializeField]
  private GameObject menuPanel;
  public bool settings = false;
  void Start() {
    m_AudioSource = GetComponent<AudioSource>();
    isPlaying = true;
    menuPanel.SetActive(false);
  }

  void Update() {
    if ((Input.GetKeyDown(KeyCode.Escape) && isPlaying) || settings) {
      if (menuPanel != null) {
        menuPanel.SetActive(true);
        if (gameObject.transform.parent != null)
          gameObject.transform.parent.GetComponent<MoveController>().IsPlay = false;
        gameObject.transform.parent.GetComponent<ShootController>().IsPlay = false;
        m_AudioSource.Pause();
        isPlaying = false;
        setIsPlay(isPlaying);
      }
    } else if (Input.GetKeyDown(KeyCode.Escape) && !isPlaying) {
      if (menuPanel != null) {
        menuPanel.SetActive(false);
        m_AudioSource.Play();
        isPlaying = true;
        setIsPlay(isPlaying);
      }
    }
  }

  private void SetSettings() {
    settings = true;
  }

  public void ContinueClick() {
    menuPanel.SetActive(false);
    float musicVol,
    soundVol;
    soundMixer.GetFloat("MasterVolume", out soundVol);
    musicMixer.GetFloat("MasterVolume", out musicVol);
    m_AudioSource.Play();
    isPlaying = true;
    setIsPlay(isPlaying);
  }

  public void MainMenu() {
    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
  }
  public void OnSliderValueChangeSound(float value) {
    soundMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
  }

  public void OnSliderValueChangeMusic(float value) {
    musicMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
  }

  public void setIsPlay(bool val) {
    if (gameObject.transform.parent != null) {
      gameObject.transform.parent.GetComponent<MoveController>().IsPlay = val;

      gameObject.transform.parent.GetComponent<ShootController>().IsPlay = val;
      GameObject[] massEnemy = GameObject.FindGameObjectsWithTag("Enemy");
      foreach(GameObject obj in massEnemy) {
        obj.GetComponentInChildren<PatrolController>().IsPlay = val;
        obj.GetComponent<Animator>().enabled = val;
      }
      GameObject[] massBullets = GameObject.FindGameObjectsWithTag("Bullet");
      foreach(GameObject obj in massBullets) {
        obj.GetComponent<BulletController>().IsPlay = val;
      }
    }
  }

}