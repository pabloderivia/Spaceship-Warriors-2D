using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    Rigidbody2D bulletRigidbody;
    public float velocity=1f;

    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();        
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
        bulletRigidbody.velocity = new Vector2(0f, -1f*velocity);

    }

    void CheckDestruction(){
        if(transform.position.y<-7.5)
            Destroy(this.gameObject);
    }


}
