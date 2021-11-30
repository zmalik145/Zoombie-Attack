using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator enemyAnim;
    private float speed = 1f;
    private GameObject player;

    private AudioSource audioSource;
    [SerializeField] private AudioClip hordeSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private ParticleSystem bulletImpact;
    [SerializeField] private ParticleSystem flameParticle;
    private bool isDead = false;
    private PlayerController playerController;
    private BoxCollider boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Mutant");
        playerController = GameObject.Find("Mutant").GetComponent<PlayerController>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackOnPlayer();
    }

    void AttackOnPlayer()
    {
        if (!playerController.isGameOver && !isDead)
        {
            Vector3 lookDirection = (transform.position - player.transform.position).normalized;
            transform.Translate(lookDirection * speed * Time.deltaTime);
            transform.LookAt(player.transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            bulletImpact.Play();
            enemyAnim.SetBool("Death", true);
            Destroy(boxCollider);
            StartCoroutine(DeathParticle());
            Destroy(gameObject, 2f);
            isDead = true;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            enemyAnim.SetBool("Attack", true);
            audioSource.PlayOneShot(attackSound, 1f);
            Destroy(gameObject, 4);
        }
    }

    IEnumerator DeathParticle()
    {
        yield return new WaitForSeconds(0.5f);
        flameParticle.Play();
    }
}
