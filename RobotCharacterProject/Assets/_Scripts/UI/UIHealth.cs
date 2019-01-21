using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Updates healthbars
/// </summary>
public class UIHealth : MonoBehaviour
{
    [SerializeField]
    private Image _healthMeter;

    public void UpdateHealthUI(float percent) {
        _healthMeter.fillAmount = percent;
    }

}
