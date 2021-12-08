using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator enemyAnim;
    private float speed = 0.7f;
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
        //get enemy Animator component reference
        enemyAnim = GetComponent<Animator>();

        //get audio source component reference
        audioSource = GetComponent<AudioSource>();

        //get reference of player in hierarchy
        player = GameObject.Find("Mutant");

        //get player controller stript referece from player in hierarchy
        playerController = GameObject.Find("Mutant").GetComponent<PlayerController>();
        
        //get boxcollider component reference
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackOnPlayer(); //attack on player method
    }

    void AttackOnPlayer()
    {
        //check if gameover and isDead is not true then execute the function
        if (!playerController.isGameOver && !isDead)
        {
            //get the player direction by substracting player position from enemy position
            Vector3 lookDirection = (transform.position - player.transform.position).normalized;

            //Translate the enemy towards player
            transform.Translate(lookDirection * speed * Time.deltaTime);

            //lookAt the player
            transform.LookAt(player.transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //check if trigger with Fireball
        if (other.gameObject.CompareTag("Fireball"))
        {
            bulletImpact.Play();                    //Play bullet impact on triggerEnter
            enemyAnim.SetBool("Death", true);       //Set Death Animation on triggerEnter
            Destroy(boxCollider);                   //Remove the boxcollider on triggerEnter
            StartCoroutine(DeathParticle());        //Death particle after one second
            Destroy(gameObject, 2f);                //Destroy the enemy after 2 seconds
            isDead = true;                          //Set isDead true
            other.gameObject.SetActive(false);      //Deactivate the bullet in object pooling
        }
        else if (other.gameObject.CompareTag("Player")) //check if trigger with player
        {
            enemyAnim.SetBool("Attack", true);          //Attack animation on trigger with player
            audioSource.PlayOneShot(attackSound, 1f);   //Play attack animation
            Destroy(gameObject, 4);                     
        }
    }

    IEnumerator DeathParticle()
    {
        yield return new WaitForSeconds(0.5f); //Wait for 1 second and then play flame particle
        flameParticle.Play();
    }
}
