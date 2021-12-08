using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private GameObject aimPoint;
    [SerializeField] private ParticleSystem winParticle1;
    [SerializeField] private ParticleSystem winParticle2;

    private MainMenuUI canvasUI;

    private AudioSource audioSource;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip roarSound;

    private float hrInput;
    private Animator playerAnim;
    private Rigidbody playerRb;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>(); //get player animator component reference
        playerRb = GetComponent<Rigidbody>();   //get the player rigidbody component reference
        audioSource = GetComponent<AudioSource>();  //get the player audiosour component reference
        canvasUI = GameObject.Find("Canvas").GetComponent<MainMenuUI>(); //CanvasUI referece by Canvas gameobject in hierarchy
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward(); //player move forward function
        Rotation();     // player rotation function
        AimFireBall();  // player aim and fire function
        BoundPosition(); //bound the player in fix position
    }

    void MoveForward()
    {
        //check if uparrow key is pressed and game is not over
        if (Input.GetKey(KeyCode.UpArrow) && !isGameOver)
        {
            //translate the player forward
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

            playerAnim.SetBool("Walk", true);   //set walk animation true on key pressed
        }
        else
        {
            playerAnim.SetBool("Walk", false); // if key is not pressed, set idle animation
        }
    }

    void Rotation()
    {
        hrInput = Input.GetAxis("Horizontal"); //taking input from left right arrow keys
        transform.Rotate(Vector3.up * hrInput * Time.deltaTime * rotationSpeed);
    }

    void AimFireBall()
    {
        //check if mouse left button is pressed and game is not over
        if (Input.GetMouseButtonDown(0) && !isGameOver)
        {
            //start Gunplay animation on mouse left click
            playerAnim.SetBool("Gunplay", true);

            audioSource.PlayOneShot(fireSound, 1f);        //playe shot sound on fired

            //aim the projectile to enemy
            SpawnFireBall();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //end the Gunplay animation when mouse left button ups
            playerAnim.SetBool("Gunplay", false);
        }
        
    }

    void SpawnFireBall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //draw a raycast from aimpoint to hit point
            //Debug.DrawLine(aimPoint.transform.position, hit.point);

            //get the projectile from object pooler's static class
            GameObject fireBall = ObjectPooler.ShareInstance.GetPooledObjects();
            if (fireBall != null)
            {
                fireBall.SetActive(true); // set the fire active
                fireBall.transform.position = aimPoint.transform.position; // set fireball transform to aimpoint transform
            }
            // turn the projectile to hit.point
            fireBall.transform.LookAt(hit.point);

            // accelerate it
            fireBall.GetComponent<Rigidbody>().velocity = fireBall.transform.forward * fireBallSpeed;
        }
    }

    void BoundPosition()
    {
        float zBound = -59f;
        if(transform.position.z < zBound)
        {
            //if the player goes out the z-axis, bound the player in new position
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //check if player trigger with enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerAnim.SetBool("Death", true);      //play death animation on trigger
            isGameOver = true;                      //Game will be over
            StartCoroutine(GameOver());             //Game over text will appear after 1 sec
        }
        else if (other.gameObject.CompareTag("Fline")) //if player trigger with finish line
        {
            playerAnim.SetBool("Victory", true);        //play the victory animation
            StartCoroutine(LevelComplete());            // Level completed text will show after 1 sec
            audioSource.PlayOneShot(roarSound, 1f);     //play the roar sound after trigger
        }
    }
    IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(0.5f);                  //wait for 0.5 sec
        isGameOver = true;                                      //game will be over
        canvasUI.levelCompleted.gameObject.SetActive(true);     //level complete text will be activated
        canvasUI.restartButton.gameObject.SetActive(true);      //restart button will be activated
        canvasUI.pauseButton.gameObject.SetActive(false);       //pause button will be deactivated
        winParticle1.Play();                                    //win particles will play
        winParticle2.Play();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        canvasUI.gameOver.gameObject.SetActive(true);           //Gameover text will be activated
        canvasUI.restartButton.gameObject.SetActive(true);      //restart button will be activated
        canvasUI.pauseButton.gameObject.SetActive(false);       // Pause button will be deactivated
    }


}
