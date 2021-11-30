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
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        canvasUI = GameObject.Find("Canvas").GetComponent<MainMenuUI>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        Rotation();
        AimFireBall();
        BoundPosition();
    }

    void MoveForward()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !isGameOver)
        {
            //translate the player forward
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            playerAnim.SetBool("Walk", true);
        }
        else
        {
            playerAnim.SetBool("Walk", false);
        }
    }

    void Rotation()
    {
        hrInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * hrInput * Time.deltaTime * rotationSpeed);
    }

    void AimFireBall()
    {
        if (Input.GetMouseButtonDown(0) && !isGameOver)
        {
            //start Gunplay animation on mouse left click
            playerAnim.SetBool("Gunplay", true);
            audioSource.PlayOneShot(fireSound, 1f);

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
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerAnim.SetBool("Death", true);
            isGameOver = true;
            StartCoroutine(GameOver());
        }
        else if (other.gameObject.CompareTag("Fline"))
        {
            playerAnim.SetBool("Victory", true);
            StartCoroutine(LevelComplete());
            audioSource.PlayOneShot(roarSound, 1f);
        }
    }
    IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(0.5f);
        isGameOver = true;
        canvasUI.levelCompleted.gameObject.SetActive(true);
        canvasUI.restartButton.gameObject.SetActive(true);
        canvasUI.pauseButton.gameObject.SetActive(false);
        winParticle1.Play();
        winParticle2.Play();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        canvasUI.gameOver.gameObject.SetActive(true);
        canvasUI.restartButton.gameObject.SetActive(true);
        canvasUI.pauseButton.gameObject.SetActive(false);
    }


}
