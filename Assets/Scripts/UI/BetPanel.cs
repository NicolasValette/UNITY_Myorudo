using Myorudo.Datas;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BetPanel : MonoBehaviour
{
    [SerializeField]
    private GameRulesData _gameRulesData;
    [SerializeField]
    private TMP_Dropdown _numberOfDice;
    [SerializeField]
    private TMP_Dropdown _faceNumber;
    [SerializeField]
    private GameObject _betPanel;

    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _betPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Temp()
    {
        DisplayBetPanel(new Bid(2, 2));
    }
    public void DisplayBetPanel(Bid previousBid)
    {
        _betPanel.SetActive(true);

        _numberOfDice.ClearOptions();
        List<string> _numberList = new List<string>();
        for (int i = previousBid.Value; i < _gameRulesData.NumberOfStartingDices * _gameRulesData.NumberOfPlayer; i++)
        {
            _numberList.Add(i.ToString());
        }
        _numberOfDice.AddOptions(_numberList);

        List<string> _valueList = new List<string>();
        _faceNumber.ClearOptions();
        for (int i = previousBid.Value; i < 7; i++)
        {
            _valueList.Add(i.ToString());
        }
        _faceNumber.AddOptions(_valueList);
        
    }
}
