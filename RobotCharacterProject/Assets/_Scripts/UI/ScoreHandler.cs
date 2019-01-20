using UnityEngine.UI;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField]
    private Text _scoreField;
    private int _score = 0;
    private int _scoreOnPerfect = 300, _scoreOnNormal = 100;

    public int Score { get { return _score; } }

    public void UpdateScore(bool isPerfect) {
        if (isPerfect) {
            _score += _scoreOnPerfect;
        }
        else {
            _score += _scoreOnNormal;
        }
        _scoreField.text = _score.ToString();
    }
}
