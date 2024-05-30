using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    [Range(0, 5f)]
    private float bulletSpeed;
    private bool bulletRight;
    private Camera playerCamera;
    private bool isPlay = true;

    public bool IsPlay { get => isPlay; set => isPlay = value; }

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        bulletRight = GameObject.Find("Player").GetComponent<MoveController>().FlipFlag;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            transform.Translate((bulletRight ? Vector3.right : Vector3.left) * bulletSpeed * Time.deltaTime);
            if (playerCamera != null)
            {
                if (playerCamera.WorldToViewportPoint(transform.position).x < 0 || playerCamera.WorldToViewportPoint(transform.position).x > 1)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }


}