using UnityEngine.UI;
using UnityEngine;

public class UIColorCycle : MonoBehaviour
{
    [SerializeField]
    private Image _mainColorObj, _leftColorObj, _rightColorObj;

    // Start is called before the first frame update
    void Start()
    {
        //subscribe to player's color swap function  
        GameController.Instance.GetPlayerReference().OnColorCycle += CycleUIColor;
    }

    void CycleUIColor(int direction) {
        //swap the size and positions of the three objects to whichever direction we cycle in
        if(direction == 1) {
            Color temp = _mainColorObj.color;
            _mainColorObj.color = _rightColorObj.color;
            _rightColorObj.color = _leftColorObj.color;
            _leftColorObj.color = temp;
        }
        else if (direction == -1) {
            Color temp = _mainColorObj.color;
            _mainColorObj.color = _leftColorObj.color;
            _leftColorObj.color = _rightColorObj.color;
            _rightColorObj.color = temp;

        }
    }
}
