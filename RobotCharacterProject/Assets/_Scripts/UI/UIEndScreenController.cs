using UnityEngine.UI;
using UnityEngine;
using System;

/// <summary>
/// Handles showing the end screen
/// </summary>
public class UIEndScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject _winScreen, _loseScreen;

    // Start is called before the first frame update
    void Start()
    {
        //For some reason GameController was holding onto an old version of this script and calling it on the event
        //GameController.Instance.OnGameWin += ShowWinScreen;
        //GameController.Instance.OnPlayerDeath += ShowLossScreen;
    }

    public void ShowLossScreen() {
        _loseScreen.SetActive(true);
    }

    public void ShowWinScreen() {
        _winScreen.SetActive(true);
    }
}
