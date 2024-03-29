using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameEndPanel;
    [SerializeField]
    private TMP_Text _gameEndText;
 

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
        _gameEndPanel.SetActive(false);
    }

    public void GameFinish(bool isWin)
    {
        _gameEndPanel.SetActive(true);
        if (isWin)
        {
            GameWin();
        }
        else
        {
            GameOver();
        }
    }

    private void GameWin()
    {
        _gameEndText.text = "You win ! You're a legendary pirate !";
    }
    private void GameOver()
    {
        _gameEndText.text = "You loose, you scallywag !";
    }
}
