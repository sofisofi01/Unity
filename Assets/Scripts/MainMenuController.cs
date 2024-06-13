using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenuController: MonoBehaviour

{
  [SerializeField]
  private GameObject menuPanel;

  public void StartGame() {
    SceneManager.LoadScene("Level1", LoadSceneMode.Single);
  }

  public void ExitGame() {
    Application.Quit();
  }

  public void Settings() {
    menuPanel.SetActive(true);
  }
}