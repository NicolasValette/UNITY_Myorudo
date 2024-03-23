using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _gameWinPanel;

    private void OnEnable()
    {
        NextTurn.OnGameEnd += GameFinish;
    }
    private void OnDisable()
    {
        NextTurn.OnGameEnd -= GameFinish;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameOverPanel.SetActive(false);
        _gameWinPanel.SetActive(false);
    }

    public void GameFinish(bool isWin)
    {
        if (isWin)
        {
            _gameWinPanel.SetActive(true);
        }
        else
        {
            _gameOverPanel.SetActive(true);
        }
    }
}
