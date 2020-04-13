using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    enum MenuOption { Start, Quit };

    private GameManager gameManager;
    public GameObject highlight;
    public GameObject start;
    public GameObject quit;

    [SerializeField] private MenuOption menuOption = MenuOption.Start;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null) Debug.LogError("No Game Manager found");
    }

    // Update is called once per frame
    void Update(){
        MoveCursor();
        SelectOption();
    }

    void MoveCursor() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if(menuOption == MenuOption.Quit) {
                menuOption = MenuOption.Start;
                ChangeSelection();
            }
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            if (menuOption == MenuOption.Start) {
                menuOption = MenuOption.Quit;
                ChangeSelection();
            }
        }
    }

    void SelectOption() {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            if (menuOption == MenuOption.Start) {


                gameManager.NewGame();
                gameManager.ChangeLevel();
            } else if (menuOption == MenuOption.Quit) {
                Debug.Log("Quitting");
                Application.Quit();
            } else {
                Debug.LogError("Menu error - menuOption in unknown state: " + menuOption);
            }
        }
    }

    void ChangeSelection() {
        if (menuOption == MenuOption.Start) {
            highlight.transform.position = start.transform.position;
        } else {
            highlight.transform.position = quit.transform.position;
        }
    }

}
