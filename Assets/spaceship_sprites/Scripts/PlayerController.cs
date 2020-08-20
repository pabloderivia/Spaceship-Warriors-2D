using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D playerRigidbody;
    public float speed = 200;
    public Animator playerAnimator;
    public GameObject playerBullet;
    SpriteRenderer playerSR;
    Vector3 playerCurrentPos;
    public int lifes = 3;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerSR = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
    void Move(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float horSpeed = horizontal*speed*Time.deltaTime;
        float verSpeed = vertical*speed*Time.deltaTime;

        Debug.Log("horSpeed = "+ horSpeed);
        Debug.Log("verSpeed = "+ verSpeed);

        playerRigidbody.velocity = new Vector2(horSpeed, verSpeed);


    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyBullet" || other.tag == "Enemy"){
            lifes--;
            if(lifes>0)
                StartCoroutine("CoroutineShowDamage");
            
            if(lifes<=0)
            {
                ExplodePlayer();
            }

            if(other.tag=="Enemy")
                 other.gameObject.GetComponent<EnemyController>().Explode();}         
            if(other.tag=="EnemyBullet")
                 Destroy (other.gameObject);           

        }

    public void Shoot()
    {
        playerCurrentPos = transform.position;
        Instantiate(playerBullet, new Vector3 (playerCurrentPos.x, playerCurrentPos.y+0.5f, playerCurrentPos.z), Quaternion.identity);

    }

    public void ExplodePlayer()
        {
           StartCoroutine("CoroutineExplode");
        }


    IEnumerator CoroutineExplode()
        {
        for(int i = 0; i <1; i++) 
            {
            playerAnimator.SetBool("isAlive", false);
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
            }
        }
    IEnumerator CoroutineShowDamage()
        {
            playerSR.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            playerSR.color = Color.white;
            
        }
                
    }




