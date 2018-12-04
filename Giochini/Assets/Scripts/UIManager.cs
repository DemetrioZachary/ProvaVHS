using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public RawImage P1_HappyBar, P2_HappyBar;
    public Image[] humorIcons;
    public Sprite[] humorSprites;
    public GameObject pauseMenu;
    public GameObject endGameMenu;
    public Image endGameBackground;
    public Sprite p1Win, p2Win, draw;

    private bool paused = false;
    private bool endGame = false;

    // -----------------------------------------------------
    private static UIManager s_Instance = null;

    public static UIManager instance {
        get {
            if (s_Instance == null) {
                Debug.LogError("Missing UI Manager");
            }
            return s_Instance;
        }
    }

    private void Awake() {
        s_Instance = FindObjectOfType<UIManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // ----------------------------------------------------

    public void SetHappyBar(int playerIndex, float happiness) {
        if (playerIndex == 1) {
            P1_HappyBar.rectTransform.localScale = new Vector3(happiness / 100f, 1, 1);
        }
        else if (playerIndex == 2) {
            P2_HappyBar.rectTransform.localScale = new Vector3(happiness / 100f, 1, 1);
        }
        UpdateHumor(playerIndex, happiness);
    }

    // YEE -------------------------------------------------------
    public void UpdateHumor(int playerIndex, float happiness) {
        if (happiness > 75f) {
            humorIcons[playerIndex - 1].sprite = humorSprites[0];
        }
        else if (happiness <= 75f && happiness > 50f) {
            humorIcons[playerIndex - 1].sprite = humorSprites[1];
        }
        else if (happiness <= 50f && happiness > 25f) {
            humorIcons[playerIndex - 1].sprite = humorSprites[2];
        }
        else {
            humorIcons[playerIndex - 1].sprite = humorSprites[3];
        }
    }
    // -----------------------------------------------------------

    private void Update() {
        if (Input.GetButtonDown("Esc") && !endGame) {
            if (!pauseMenu.activeSelf) {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                pauseMenu.GetComponentInChildren<Button>().Select();
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
            }
            else {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                //Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = false;
            }
        }
    }

    public void EndGame(int playerIndex) {
        endGame = true;
        Time.timeScale = 0;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        if (playerIndex == 1) {
            endGameBackground.sprite = p2Win;
        }
        else if (playerIndex == 2) {
            endGameBackground.sprite = p1Win;
        }
        endGameMenu.SetActive(true);
        endGameMenu.GetComponentInChildren<Button>().Select();
        pauseMenu.SetActive(false);
    }

    // MENU BUTTONS
    public void Resume() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void GotToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
