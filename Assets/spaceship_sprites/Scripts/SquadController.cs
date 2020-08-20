using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    //THIS CLASS WILL SPAM THE DIFFERENT SQUADS
    public GameObject []  avalaibleEnemies;
    public GameObject enemy1;
    public GameObject enemy2;
    Transform parent;
    public List <Transform>  avalaibleCircleSquadsSpawnPositions;
    public static SquadController sharedInstance;
    
    public float yPos;
    float screenHeight;
    float screenWidth;
    float halfScreenWidth;
    void Awake()
    {
        yPos = 0;
        sharedInstance = this;

        
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

    void GenerateBasicEnemiesWave(float separationBetweenEnemies){ 
        Debug.Log("generando enemigos");
        Debug.Log(screenWidth);

        GameObject parent = new GameObject(); 
        parent.transform.position = new Vector3(-screenWidth/2, screenHeight-3f,0);
        parent.name = "Enemy1 - Wave";

        //ascending half of the squad
        for(float xPos = 1.5f; xPos<halfScreenWidth; xPos+=separationBetweenEnemies){
            yPos+=0.5f;
            GameObject newEnemy = Instantiate (enemy1, Vector3.zero, Quaternion.identity);
            Transform newEnemyTransform = newEnemy.GetComponent<Transform>();
            newEnemyTransform.SetParent(parent.transform);
            newEnemyTransform.localPosition = new Vector3(xPos, yPos, 0);
        } 
        //descending half of the squad
        for(float xPos = halfScreenWidth; xPos<screenWidth-1.5f; xPos+=separationBetweenEnemies){
            GameObject newEnemy = Instantiate (enemy1, Vector3.zero, Quaternion.identity);
            Transform newEnemyTransform = newEnemy.GetComponent<Transform>();
            newEnemyTransform.SetParent(parent.transform);
            newEnemyTransform.localPosition = new Vector3(xPos, yPos, 0);
            yPos-=0.5f;
        }
    }

    void GenerateCircularEnemiesWave(float separationBetweenEnemies, float reductionRadioCoefficient, Vector3 WavePosition){ 
    float radio = halfScreenWidth/reductionRadioCoefficient;

       //We generate a partner object to the enemies of the wave
        GameObject parentCircularWave = new GameObject();
        parentCircularWave.transform.position = WavePosition;
        parent = parentCircularWave.transform;


        for(float xPos = -radio; xPos<-0.5; xPos+=separationBetweenEnemies){
            //create two GO's: 1 for a positive X position, and another 1 for his opposite one
            GameObject newTopEnemy = Instantiate (enemy2, Vector3.zero, Quaternion.identity);
            GameObject newTopEnemyMirror = Instantiate (enemy2, Vector3.zero, Quaternion.identity);
            GameObject newBotEnemy = Instantiate (enemy2, Vector3.zero, Quaternion.identity);
            GameObject newBotEnemyMirror = Instantiate (enemy2, Vector3.zero, Quaternion.identity);


            Transform newTopEnemyTransform = newTopEnemy.GetComponent<Transform>();
            newTopEnemyTransform.SetParent(parent);
            newTopEnemyTransform.localPosition = new Vector3(xPos, GetYposForCircularFormation(xPos, radio), 0);

            Transform newTopEnemyMirrorTransform = newTopEnemyMirror.GetComponent<Transform>();
            newTopEnemyMirrorTransform.SetParent(parent);
            newTopEnemyMirrorTransform.localPosition = new Vector3(-xPos, GetYposForCircularFormation(xPos, radio), 0);

            Transform newBotEnemyTransform = newBotEnemy.GetComponent<Transform>();
            newBotEnemyTransform.SetParent(parent);
            newBotEnemyTransform.localPosition = new Vector3(xPos, -GetYposForCircularFormation(xPos, radio), 0);
            
            Transform newBotEnemyMirrorTransform = newBotEnemyMirror.GetComponent<Transform>();
            newBotEnemyMirrorTransform.SetParent(parent);
            newBotEnemyMirrorTransform.localPosition = new Vector3(-xPos, -GetYposForCircularFormation(xPos, radio), 0);
            
        }



    }


    float GetYposForCircularFormation(float xPos, float radio)
    {
       float squaredY = radio*radio - xPos*xPos;
       float yPos = Mathf.Sqrt(squaredY);
       Debug.Log("Ypos = "+yPos);
       return yPos;

    }

    IEnumerator CoRutineGenerateWaves() {
    //3 first enemy1-waves
     for(int i = 0; i <3; i++) {
         float sep = Random.Range(1.5f, 4f);
          GenerateBasicEnemiesWave(sep);
          yield return new WaitForSeconds(4);
      }
    //1st  3x-shot wave
    GenerateCircularEnemiesWave(0.9f,4f, GetCircleSquadPosition(4f));
     yield return new WaitForSeconds(6);
    //2nd 3x-shot hard wave
    GenerateCircularEnemiesWave(0.9f,4f, GetCircleSquadPosition(4f));
    GenerateCircularEnemiesWave(0.9f,2f, GetCircleSquadPosition(2f));

 }
    public  float getScreenWidth()
    {
        return this.screenWidth;
    }

    //higher reductionRadioCoeff, less size, so it's spawned nearer to the screen
    Vector3 GetCircleSquadPosition(float reductionRadioCoefficient)
    {
        int randomIndex = Random.Range(0, avalaibleCircleSquadsSpawnPositions.Count);
        Debug.Log("randomInd = "+randomIndex);
        Vector3 squadPosition = avalaibleCircleSquadsSpawnPositions[randomIndex].position+new Vector3(0, 16f/reductionRadioCoefficient,0);
        return squadPosition;
    }


}
