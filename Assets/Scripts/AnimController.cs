using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController: MonoBehaviour {
  private Animator animator;
  private MoveController moveController;
  private bool isPlay = true;

  public bool IsPlay {
    get => isPlay;
    set => isPlay = value;
  }
  void Start() {
    moveController = GetComponent<MoveController>();
    animator = GetComponent<Animator>();
  }
  void Update() {
    if (isPlay) {
      if (animator.enabled == false) {
        animator.enabled = true;
        animator.StartPlayback();
      }
      if (!moveController.CanJump) {
        animator.SetTrigger("JumpTrigger");
      } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
        animator.SetTrigger("RunTrigger");
      } else {
        animator.SetTrigger("IdleTrigger");
      }
    } else {
      animator.StopPlayback();
      animator.enabled = false;
    }
  }
}