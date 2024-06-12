using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    private bool flipFlag = false;
    [SerializeField]
    private float speed;
    private Animator animator;
    private SpriteRenderer render;
    private bool isPlay = true;

    public bool IsPlay { get => isPlay; set => isPlay = value; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        // Initially enable the animator if IsPlay is true
        if (isPlay)
        {
            animator.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            // Ensure animator is enabled if not already
            if (!animator.enabled)
            {
                animator.enabled = true;
                animator.StartPlayback(); // This line is needed to actually play the animation
            }
            transform.Translate(new Vector3(flipFlag ? speed : -speed, 0, 0) *Time.deltaTime); ;
        }
        else
        {
            // Ensure animator is disabled
            if (animator.enabled)
            {
                animator.StopPlayback();
                animator.enabled = false;
            }
        }
    }

    private void Flip()
    {
        render.flipX = flipFlag;
        flipFlag = !flipFlag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Flip();
    }
}