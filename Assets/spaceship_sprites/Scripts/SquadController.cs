using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    //THIS CLASS WILL SPAM THE DIFFERENT SQUADS
    public GameObject []  avalaibleEnemies;
    public GameObject enemy1;
    public Transform parent;
    
    public float yPos;
    float screenHeight;
    float screenWidth;
    float halfScreenWidth;
    void Awake()
    {
        yPos = 0;

        
    }
    // Start is called before the first frame update
    void Start()
    {
        //we get screen sizes
         screenHeight = Camera.main.orthographicSize * 2.0f;
         screenWidth = screenHeight * Camera.main.aspect;
         halfScreenWidth = screenWidth/2;
        StartCoroutine("CoRutineGenerateWaves");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateEnemiesWave(float separationBetweenEnemies){ 
        Debug.Log("generando enemigos");
        Debug.Log(screenWidth);

        //ascending half of the squad
        for(float i = 1.5f; i<halfScreenWidth; i+=separationBetweenEnemies){
            yPos+=0.5f;
            GameObject newEnemy = Instantiate (enemy1, Vector3.zero, Quaternion.identity);
            Transform newEnemyTransform = newEnemy.GetComponent<Transform>();
            newEnemyTransform.SetParent(parent);
            newEnemyTransform.localPosition = new Vector3(i, yPos, 0);
        } 
        //descending half of the squad
        for(float i = halfScreenWidth; i<screenWidth-1.5f; i+=separationBetweenEnemies){
            GameObject newEnemy = Instantiate (enemy1, Vector3.zero, Quaternion.identity);
            Transform newEnemyTransform = newEnemy.GetComponent<Transform>();
            newEnemyTransform.SetParent(parent);
            newEnemyTransform.localPosition = new Vector3(i, yPos, 0);
            yPos-=0.5f;
        }
    }

    IEnumerator CoRutineGenerateWaves() {
     for(int i = 0; i <5; i++) {
         float separation = Random.Range(1.5f, 4f);
         GenerateEnemiesWave(separation);
         yield return new WaitForSeconds(7);
     }
 }

}
