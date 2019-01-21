using UnityEngine.UI;
using UnityEngine;
using System;

/// <summary>
/// Handles showing the end screen and score
/// </summary>
public class UIEndScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject _winScreen, _loseScreen;
    [SerializeField]
    private Text _winScreenScore, _loseScreenScore;
    // Start is called before the first frame update
    void Start()
    {
        //clear out old Controller event subs
        GameController.Instance.OnGameWin = null;
        GameController.Instance.OnPlayerDeath = null;
        //get new subs
        GameController.Instance.OnGameWin += ShowWinScreen;
        GameController.Instance.OnPlayerDeath += ShowLossScreen;
    }

    public void ShowLossScreen(int finalScore) {
        _loseScreenScore.text = finalScore.ToString() + "pts";
        _loseScreen.SetActive(true);
    }

    public void ShowWinScreen(int finalScore) {
        _winScreenScore.text = finalScore.ToString() + "pts";
        _winScreen.SetActive(true);
    }
}
