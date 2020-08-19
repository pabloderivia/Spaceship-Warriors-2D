using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager sharedInstance;
    enum GameState 
    {
        InGame, 
        Pause,
        GameOver
    }

    GameState currentGameState;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        currentGameState = GameState.InGame;
    }

    void  SetGameOver(){
        currentGameState = GameState.GameOver;
    }


}
