using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    Rigidbody2D bulletRigidbody;
    public float velocity=1f;
    Vector2 direction;
    

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
        //bulletRigidbody.velocity = new Vector2(0f, -1f*velocity);
        bulletRigidbody.velocity = this.direction*velocity;

    }

    //We will destroy the GOs when they go out the camera range
    void CheckDestruction(){
        if(transform.position.y<-7.5 ||
        transform.position.x>SquadController.sharedInstance.getScreenWidth() ||
        transform.position.x<SquadController.sharedInstance.getScreenWidth()*-1 )
            Destroy(this.gameObject);
    }

    public void SetFireDirection(Vector2 newDirection){

        this.direction = newDirection;
    }
    public void SetFireRotation(float rotationZAxis){

        this.transform.Rotate(new Vector3 (0, 0, rotationZAxis), Space.Self);
    }




}
