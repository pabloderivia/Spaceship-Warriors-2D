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


    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();       
        enemyAnimator = GetComponent<Animator>();        
 
    }

    // Start is called before the first frame update
    void Start()
    {
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
        CheckStartToShoot();
    }

    void Shoot(){
        shipCurrentPos = transform.position;
        Instantiate(bulletPrefab, new Vector3 (shipCurrentPos.x, shipCurrentPos.y-1.1f, shipCurrentPos.z), Quaternion.identity);
    }

    void Move(){
        enemyRigidbody.velocity = new Vector2(0f, -1f*velocity);

    }

    void CheckStartToShoot(){
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
}
