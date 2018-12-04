using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Start() {
        GetComponentInChildren<Button>().Select();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
