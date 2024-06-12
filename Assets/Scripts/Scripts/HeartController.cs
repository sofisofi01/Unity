using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] hearts;
    [SerializeField] private int health;
    [SerializeField] private GameObject prefabHeart;
    private Animator animator;
    private int currentHealth;
    void Start()
    {
        animator = GetComponent<Animator>();
        hearts = new GameObject[health];
        currentHealth = health;
        ResetHealth(currentHealth);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5f)
        {
            gameObject.transform.position = GetComponent<SpawnController>().SpawnPos;

        }

    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Trap" || coll.gameObject.tag == "Enemy")
        {
            if (currentHealth > 0)
            {
                currentHealth -= 1;
                GetComponents<AudioSource>()[2].Play();
                Destroy(hearts[currentHealth]);
            }
            if (currentHealth <= 0)
            {
                GetComponents<AudioSource>()[3].Play();
                currentHealth = health;
                StartCoroutine(DieDelay());
            }
        }
    }

    IEnumerator DieDelay()
    {
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("IdleTrigger");
        gameObject.transform.position = GetComponent<SpawnController>().SpawnPos;
        ResetHealth(currentHealth);

    }

    private void ResetHealth(int health)
    {
        float sizeHeartWithOffset = 1.0f;
        float size = health + health * 0.1f;
        for (int i = 0; i < hearts.Length; i++)
            hearts[i] = null;
        for (int i = 0; i < health; i++)
        {
            hearts[i] = Instantiate(prefabHeart, Vector3.zero, Quaternion.identity);
            hearts[i].transform.SetParent(transform, false);
            hearts[i].transform.position = new Vector3(transform.position.x + i * sizeHeartWithOffset - 0.3f, transform.position.y + 1.0f, transform.position.z);
        }
    }
}
