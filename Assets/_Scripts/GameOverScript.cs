using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{

    private Text gameoverText;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start(){
        gameoverText = GetComponentInChildren<Text>();
        gameManager = FindObjectOfType<GameManager>();
        Invoke("TimeOut", 3);
    }

    void TimeOut() {
        gameManager.ChangeLevel("GameMenu");
    }

    // Update is called once per frame
    void Update(){
        


    }



}
