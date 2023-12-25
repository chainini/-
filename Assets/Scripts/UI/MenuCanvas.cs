using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    public Button startBtn;
    public Button continueBtn;
    public Button quitBtn;

    private void Awake()
    {
        startBtn.onClick.AddListener(StartNewGame);
        continueBtn.onClick.AddListener(Continue);
        quitBtn.onClick.AddListener(Quit);
    }
    private void StartNewGame()
    {
        EventHandle.CallStartNewGame();
    }

    private void Continue()
    {

    }

    private void Quit()
    {
        Application.Quit();
    }
}
