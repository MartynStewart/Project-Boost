using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int lives = 3;
    public float fuel = 200f;
    public bool isEngineActive = true;

    private int initialLives;
    private float initialFuel;


    void Start(){
        initialLives = lives;
        initialFuel = fuel;
    }

    //Singleton Pattern
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public void NewGame() {
        lives = initialLives;
        fuel = initialFuel;
        isEngineActive = true;
    }

    public void DestroyShip() {
        if(lives > 0) {
            lives--;
            MoveLevel(SceneManager.GetActiveScene().buildIndex);
        } else {
            ChangeLevel("GameOver");
        }
    }

    public void BurnFuel(float burn) {
        fuel -= burn;
        if (fuel < 0) {
            fuel = 0;
            isEngineActive = false;
        }
    }

    public void AddFuel(float increase) {
        fuel += increase;
    }

    public void ChangeLevel() {
        MoveLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeLevel(string levelname) {
        if(levelname == "GameMenu") {
            MoveLevel(2);
        } else if (levelname == "GameWin") {
            MoveLevel(0);
        } else if (levelname == "GameOver") {
            MoveLevel(1);
        } else {
            MoveLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void MoveLevel(int levelno) {

        Debug.Log("Loading level No: " + levelno);

        if(levelno > SceneManager.sceneCountInBuildSettings-1 || levelno < 0) {
            Debug.LogError("Attempted to call non existant scene - Called: " + levelno + " Expected up to: " + SceneManager.sceneCountInBuildSettings);
            return;
        }

      SceneManager.LoadScene(levelno);
    }

}
