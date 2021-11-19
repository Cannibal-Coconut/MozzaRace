using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Header("Orlando Positions")]
    [SerializeField] private GameObject _menuPos;
    [SerializeField] private GameObject _gamePos;
    [SerializeField] private GameObject _RedMenuBackground;

    [Header("Game GOs")]
    
    [SerializeField] private PlayerMovementInterface _playerInfo;
    

    private void Start() {
        
        SetMenuPos();

    }

    public void SetMenuPos(){

        _playerInfo.transform.position = _menuPos.transform.position;
        _RedMenuBackground.SetActive(true);
    }
    

    public void SetGamePos(){

        _playerInfo.transform.position = _gamePos.transform.position;
        _RedMenuBackground.SetActive(false);

    }


}
