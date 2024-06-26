using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController: MonoBehaviour {
  private int numberCheck;
  private Vector3 spawnPos = new Vector3(-20f, 0.75f, 0f);
  private Animator animator;

  public Vector3 SpawnPos {
    get => spawnPos;
    set => spawnPos = value;
  }

  void Start() {
    numberCheck = 1;
    animator = GetComponent<Animator>();
  }

  private void OnTriggerEnter2D(Collider2D coll) {
    if (coll.gameObject.tag == "Check") {
      if (numberCheck != Convert.ToInt16(coll.gameObject.name.Substring(gameObject.name.Length - 1))) {
        SpawnPos = coll.transform.position;
        numberCheck = Convert.ToInt16(coll.gameObject.name.Substring(gameObject.name.Length - 1));
      }
    }
  }

}