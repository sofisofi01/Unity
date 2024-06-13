using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PatrolController: MonoBehaviour {
  private bool flipFlag = false;
  [SerializeField]
  private float speed;
  private Animator animator;
  private SpriteRenderer render;
  private bool isPlay = true;

  public bool IsPlay {
    get => isPlay;
    set => isPlay = value;
  }

  void Start() {
    animator = GetComponent<Animator>();
    render = GetComponent<SpriteRenderer>();
    if (isPlay) {
      animator.enabled = true;
    }
  }

  void Update() {
    if (isPlay) {

      if (!animator.enabled) {
        animator.enabled = true;
        animator.StartPlayback();
      }
      transform.Translate(new Vector3(flipFlag ? speed : -speed, 0, 0) * Time.deltaTime);;
    } else {

      if (animator.enabled) {
        animator.StopPlayback();
        animator.enabled = false;
      }
    }
  }

  private void Flip() {
    render.flipX = flipFlag;
    flipFlag = !flipFlag;
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    Flip();
  }
}