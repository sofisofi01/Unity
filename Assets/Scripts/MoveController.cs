using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController: MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private float powerJump;
    private bool isGrounded;
    private bool flipFlag = true;
    private bool canJump = false;
    private bool isPlay = true;
    private Rigidbody2D rb;
    private SpriteRenderer render;
    private AudioSource jump;

    public bool CanJump {
      get => canJump;
      set => canJump = value;
    }
    public bool FlipFlag {
      get => flipFlag;
      set => flipFlag = value;
    }
    public bool IsPlay {
      get => isPlay;
      set => isPlay = value;
    }
    public float PowerJump {
      get => powerJump;
      set => powerJump = value;
    }
    public bool IsGrounded {
      get => isGrounded;
      set => isGrounded = value;
    }

    void Start() {
      rb = GetComponent<Rigidbody2D>();
      render = GetComponent<SpriteRenderer>();
      jump = GetComponents<AudioSource>()[0];
      IsGrounded = true;
    }

    void Update() {
      if (isPlay) {

        if (canJump && Input.GetKey(KeyCode.W)) {
          rb.AddForce(PowerJump * Vector2.up, ForceMode2D.Impulse);
          canJump = false;
          jump.Play();
        }

        if ((flipFlag && Input.GetAxis("Horizontal") < 0) || (!flipFlag && Input.GetAxis("Horizontal") > 0)) {
          Flip();
        }

        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0));

      }
      if (transform.position.y > 0.75f || transform.position.y < 0f) {
        IsGrounded = false;
      } else {
        IsGrounded = true;
      }
    }

    private void Flip() {
      render.flipX = flipFlag;
      flipFlag = !flipFlag;
    }

    private void OnCollisionEnter2D(Collision2D coll) {

      if (!IsGrounded) {
        canJump = false;
      } else {
        canJump = true;
      }

    }


}