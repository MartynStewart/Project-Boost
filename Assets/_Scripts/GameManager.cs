using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start(){
        
    }

    public void ChangeLevel() {
        Debug.Log("Change type 1");
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
            Debug.Log("Change type 2");
            MoveLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void MoveLevel(int levelno) {

        Debug.Log("Loading level No: " + levelno);

        if(levelno > SceneManager.sceneCountInBuildSettings || levelno < 0) {
            Debug.LogError("Attempted to call non existant scene - Called: " + levelno + " Expected up to: " + SceneManager.sceneCountInBuildSettings);
            return;
        }

        SceneManager.LoadScene(levelno);
    }

}
