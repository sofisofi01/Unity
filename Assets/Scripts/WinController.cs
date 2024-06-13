using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController: MonoBehaviour {
  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.name == "Player")
      SceneManager.LoadScene("Win", LoadSceneMode.Single);
  }
}