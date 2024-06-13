using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float powerJump;
    private bool flipFlag = true;
    private bool canJump = false;
    private bool isPlay = true;

    private Rigidbody2D rb;
    private SpriteRenderer render;
    private AudioSource jump;

    public bool CanJump { get => canJump; set => canJump = value; }
    public bool FlipFlag { get => flipFlag; set => flipFlag = value; }
    public bool IsPlay { get => isPlay; set => isPlay = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        jump = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            {

            }

            if (canJump && Input.GetKey(KeyCode.W))
            {
                rb.AddForce(powerJump * Vector2.up, ForceMode2D.Impulse);
                canJump = false;
                jump.Play();
            }

            if ((flipFlag && Input.GetAxis("Horizontal") < 0) || (!flipFlag && Input.GetAxis("Horizontal") > 0))
            {
                Flip();
            }

            transform.Translate(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0));
        } 
    }

    private void Flip()
    {
        render.flipX = flipFlag;
        flipFlag = !flipFlag;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }

   
}