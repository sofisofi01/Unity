using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isTake;
    public bool IsTake { get => isTake; set { if (!value) isTake = value; } }
    private Animator animatorFlag;
    private AudioSource open;
    private bool isPlay=true;

    public Animator AnimatorFlag { get => animatorFlag; }
    public bool IsPlay { get => isPlay; set => isPlay = value; }

    void Start()
    {
        IsTake = false;
        animatorFlag = GetComponent<Animator>();
        open = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            if (!animatorFlag.enabled)
            {
                animatorFlag.enabled = true;
                animatorFlag.StartPlayback();
            }
        }
        else
        {
            if (animatorFlag.enabled)
            {
                animatorFlag.enabled = false;
                animatorFlag.StopPlayback();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
           
            isTake = true;
            open.Play();
            CheckController[] massChecks = GameObject.Find("CPoints").GetComponentsInChildren<CheckController>();
            foreach (CheckController script in massChecks)
            {
                script.IsTake = false;
                script.AnimatorFlag.SetBool("isActive", false);
            }
            animatorFlag.SetBool("isActive", true);
        }
    }
}