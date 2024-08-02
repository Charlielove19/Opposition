using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    /// <summary>
    /// WW
    /// </summary>
    public void ShowInstructions() {
        // Show instructions canvas
        GameObject.Find("InstructionsCanvas").GetComponent<Canvas>().sortingOrder = 1;
    }


    public void HideInstructions() {
        // Hide instructions canvas
        GameObject.Find("InstructionsCanvas").GetComponent<Canvas>().sortingOrder = -1;
    }

    public void OnPlayButton() {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnMainMenuButton() {
        SceneManager.LoadScene("MainMenu");
    }

    public void HideWinScreen() {
        GameObject.Find("YouWinScreen").SetActive(false);
    }

    public void OnQuitButton() {
        Application.Quit();
    }
}
