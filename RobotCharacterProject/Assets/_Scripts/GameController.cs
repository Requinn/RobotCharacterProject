using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles state of the game
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField]
    private Character _playerRef;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Character GetPlayerReference() {
        return _playerRef;
    }
}
