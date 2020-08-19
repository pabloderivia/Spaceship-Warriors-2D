using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    Rigidbody2D bulletRigidbody;
    public float velocity=7f;
    public Animator bulletAnimator;

    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();   
        //bulletAnimator = GetComponent<Animator>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        CheckDestruction();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move(){

        bulletRigidbody.velocity = new Vector2(0f, 1f*velocity);

    }

    void CheckDestruction(){
        if(transform.position.y>7.5)
            Destroy(this.gameObject);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().Explode();
            Destroy(this.gameObject);
        }
        else if(other.tag=="EnemyBullet")
        {
            bulletAnimator.SetBool("isAlive", false);
            Destroy(other.gameObject);
            Explode();
        }
    }
            public void Explode()
        {
            StartCoroutine("CoroutineExplode");
        }
            IEnumerator CoroutineExplode()
         {
             //we will deactivate the bullet's rigidbody to prevent extra-hits
            Destroy(bulletRigidbody);
            for(int i = 0; i <1; i++) {
            bulletAnimator.SetBool("isAlive", false);
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
         }
    }
}

