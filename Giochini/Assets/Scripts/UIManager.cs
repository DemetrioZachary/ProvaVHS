using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public RawImage P1_HappyBar, P2_HappyBar;
    public GameObject pauseMenu;

    private bool paused = false;

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

    public void SetHappyBar(int playerIndex, float valuePercent) {
        if (playerIndex == 1) {
            P1_HappyBar.rectTransform.localScale = new Vector3(valuePercent / 100f, 1, 1);
        }
        else if (playerIndex == 2) {
            P2_HappyBar.rectTransform.localScale = new Vector3(valuePercent / 100f, 1, 1);
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Esc")) {
            if (!pauseMenu.activeSelf) {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                pauseMenu.GetComponentInChildren<Button>().Select();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    // MENU BUTTONS
    public void Resume() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
