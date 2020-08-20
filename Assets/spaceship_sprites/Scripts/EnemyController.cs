using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject bulletPrefab;
    Vector3 shipCurrentPos;
    Rigidbody2D enemyRigidbody;
    public float velocity = 0.1f;
    bool isShooting = false;
    Animator enemyAnimator;
    Vector2 moveDirection;
    public bool isTripleShooter;


    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();       
        enemyAnimator = GetComponent<Animator>();   
        moveDirection = new Vector2(0f, -1f);     
 
    }

    // Start is called before the first frame update
    void Start()
    {
            StartCoroutine("CoroutineMovement");
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        CheckDestruction();
    }

    void FixedUpdate()
    {
        Move();
        CheckStartToShootAndMove();
    }

    void Shoot(){
        shipCurrentPos = transform.position;
        if(!isTripleShooter)
        {
            GameObject bulletMid = Instantiate(bulletPrefab, new Vector3 (shipCurrentPos.x, shipCurrentPos.y-1.1f, shipCurrentPos.z), Quaternion.identity);
            bulletMid.GetComponent<EnemyBulletController>().SetFireDirection(Vector2.down);            
            
        }
        else
        {
            GameObject bulletMid = Instantiate(bulletPrefab, new Vector3 (shipCurrentPos.x, shipCurrentPos.y-1.1f, shipCurrentPos.z), Quaternion.identity);
            bulletMid.GetComponent<EnemyBulletController>().SetFireDirection(Vector2.down);            

            GameObject bulletLeft = Instantiate(bulletPrefab, new Vector3 (shipCurrentPos.x-1.1f, shipCurrentPos.y, shipCurrentPos.z), Quaternion.identity);
            bulletLeft.GetComponent<EnemyBulletController>().SetFireDirection(Vector2.left);
            bulletLeft.GetComponent<EnemyBulletController>().SetFireRotation(-90f);

            GameObject bulletRight = Instantiate(bulletPrefab, new Vector3 (shipCurrentPos.x+1.1f, shipCurrentPos.y, shipCurrentPos.z), Quaternion.identity); 
            bulletRight.GetComponent<EnemyBulletController>().SetFireDirection(Vector2.right);
            bulletRight.GetComponent<EnemyBulletController>().SetFireRotation(90f);
            

        }
    }

    void Move(){
        enemyRigidbody.velocity = moveDirection*velocity;

    }

    void CheckStartToShootAndMove(){
        shipCurrentPos = transform.position;        
        if((shipCurrentPos.y <=5) && isShooting==false){
            InvokeRepeating("Shoot", 0.2f, 1.5f);
            isShooting = true;
        }
    }

        void CheckDestruction(){
        if(transform.position.y<-7.5)
            Destroy(this.gameObject);
    }


        public void Explode()
        {
            StartCoroutine("CoroutineExplode");
        }
            IEnumerator CoroutineExplode()
         {
            for(int i = 0; i <1; i++) {
            enemyAnimator.SetBool("isAlive", false);
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
         }

    }

    IEnumerator CoroutineMovement()
    {
        while(true)
        {
            moveDirection = new Vector2(-1, -1);
            yield return new WaitForSeconds(1f);
            moveDirection = new Vector2(1, -1);
            yield return new WaitForSeconds(1f);
            moveDirection = new Vector2(-1, -0.5f);
            yield return new WaitForSeconds(1f);
            moveDirection = new Vector2(1, -0.5f);
            yield return new WaitForSeconds(1f);
            moveDirection = new Vector2(0, -2f);
        }
    }
}
