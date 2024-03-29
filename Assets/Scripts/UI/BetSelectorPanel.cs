using Myorudo.Datas;
using Myorudo.FSM;
using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Myorudo.UI
{
    public class BetSelectorPanel : MonoBehaviour
    {
        public enum SelectorType
        {
            Value,
            Face
        }

        [SerializeField]
        private GameObject _selectorPanel;
        [SerializeField]
        private Image _imageDice;
        [SerializeField]
        private List<Sprite> _sprites;
        [Header("Value Selector")]
        [SerializeField]
        private TMP_Text _selectorValueFieldText;
        [SerializeField]
        private Button _increaseValueButton;
        [SerializeField]
        private Button _decreaseValueButton;
        [Header("Face Selector")]
        [SerializeField]
        private TMP_Text _selectorFaceFieldText;
        [SerializeField]
        private Button _increaseFaceButton;
        [SerializeField]
        private Button _decreaseFaceButton;
        [SerializeField]
        private TMP_Text _previousBidText;
        [SerializeField]
        private Button _betButton;
        [SerializeField]
        private TMP_Text _probabilityText;

        [SerializeField]
        HumanPlayerFSM _playerFSM;
        [SerializeField]
        private GameRulesData _gameRulesData;
        [SerializeField]
        private StatCalculator _statCalculator;
        

        private Bid _initialBid;
        private bool _isFirstBidOfRound = false;

        private int _value;
        private int _face;

        private void OnEnable()
        {
            
            _playerFSM.OnActiveTurn += ActivePanel;
        }
        private void OnDisable()
        {
            _playerFSM.OnActiveTurn -= ActivePanel;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _selectorPanel.SetActive(false);   
        }

        public void ActivePanel()
        {
            _selectorPanel.SetActive(true);
            _initialBid = _playerFSM.PreviousBid();
            if (_initialBid == null)
            {
                //first round
                _initialBid = new Bid(1, 1);
                _isFirstBidOfRound = true;
            }
            else
            {
                _isFirstBidOfRound = false;
            }
            _value = _initialBid.Value;
            _face = _initialBid.Face;
            _selectorValueFieldText.text = _value.ToString();
            _selectorFaceFieldText.text = _face.ToString();
            _imageDice.sprite = _sprites[_face - 1];
            _previousBidText.text = $"{_initialBid.Value} - {_initialBid.Face}";
            _decreaseValueButton.interactable = true;
            _increaseValueButton.interactable = true;
            _decreaseFaceButton.interactable = true;
            _increaseFaceButton.interactable = true;

            
            _decreaseFaceButton.GetComponentInChildren<TMP_Text>().text = "-";
            

            CheckValidBid();
            DisplayStat();
        }

        public void IncreaseValue()
        {
            ModifyValue(1);
        }
        public void DecreaseValue()
        {
            ModifyValue(-1);
        }
        public void IncreaseFace()
        {
            ModifyFace(1);
        }
        public void DecreaseFace()
        {
            ModifyFace(-1);
        }

        private void ModifyValue (int valueToAdd)
        {
            _value = (_value + valueToAdd);
            if (_value < _initialBid.Value)
            {
                _value = _gameRulesData.NumberOfStartingDices * _gameRulesData.NumberOfPlayer;
            }
            else if (_value > _gameRulesData.NumberOfStartingDices * _gameRulesData.NumberOfPlayer)
            {
                _value = _initialBid.Value;
            }
                
            _selectorValueFieldText.text = _value.ToString();

            if (_value != _initialBid.Value)
            {
                EnableOrDisableSelector(SelectorType.Value, true);
                EnableOrDisableSelector(SelectorType.Face, false);
            }
            else
            {
                EnableOrDisableSelector(SelectorType.Value, true);
                EnableOrDisableSelector(SelectorType.Face, true);
            }
            CheckValidBid();
            DisplayStat();
        }
        private void ModifyFace (int faceToAdd)
        {
            if (_initialBid.Face == 1)
            {
                _face = (_face + faceToAdd);

                if (_face == 1)
                {
                    _decreaseValueButton.interactable = true;
                    _increaseValueButton.interactable = true;
                    _value = _initialBid.Value;
                }
                else
                {
                    _decreaseValueButton.interactable = false;
                    _increaseValueButton.interactable = false;
                    _value = (_initialBid.Value * 2) + 1;
                }

                if (_face <= 0)
                {
                    _face = _gameRulesData.NumberOfFace; ;
                }
                else if (_face > _gameRulesData.NumberOfFace)
                {
                    _face = 1;
                }

                if (_face > _initialBid.Face || _face == 1)
                {
                    _decreaseFaceButton.GetComponentInChildren<TMP_Text>().text = "-";
                }
                if (_face == _initialBid.Face)
                {
                    _decreaseFaceButton.GetComponentInChildren<TMP_Text>().text = "Paco";
                }
                _selectorValueFieldText.text = _value.ToString();
                _selectorFaceFieldText.text = _face.ToString();
                _imageDice.sprite = _sprites[_face - 1];

            }
            else
            {
                if (_face == 1)
                {
                    //   _face = _initialBid.Face;
                    //   _value = _initialBid.Value;
                    _face = (_face + faceToAdd);
                    if (_face <= 0)
                    {
                        _face = _gameRulesData.NumberOfFace;
                    }
                    _value = _initialBid.Value;
                    _selectorValueFieldText.text = _value.ToString();
                    _decreaseFaceButton.GetComponentInChildren<TMP_Text>().text = "-";
                    _decreaseFaceButton.interactable = true;
                }
                else
                {
                    _face = (_face + faceToAdd);
                }

                if (_face <= 0)
                {
                    _face = _gameRulesData.NumberOfFace;
                }
                else if (_face > _gameRulesData.NumberOfFace)
                {
                    _face = 1;
                }


                if (_face > _initialBid.Face)
                {
                    _decreaseFaceButton.GetComponentInChildren<TMP_Text>().text = "-";
                }
                if (_face == _initialBid.Face)
                {
                    _decreaseFaceButton.GetComponentInChildren<TMP_Text>().text = "Paco";
                }
                if (_face > 1 && _face < _initialBid.Face)
                {
                    if (faceToAdd <0)
                    {
                        _face = 1;
                        _value = (_value / 2);
                    }
                    else
                    {
                        _face = _initialBid.Face;
                        _value = _initialBid.Value;
                    }
                    
                    _selectorValueFieldText.text = _value.ToString();
                }
                else if (_face == 1)
                {
                    _value = (_value / 2);
                    _selectorValueFieldText.text = _value.ToString();
                }
               


                _selectorFaceFieldText.text = _face.ToString();
                _imageDice.sprite = _sprites[_face - 1];
               
            }
            if (_face != _initialBid.Face)
            {
                EnableOrDisableSelector(SelectorType.Face, true);
                EnableOrDisableSelector(SelectorType.Value, false);
            }
            else
            {
                EnableOrDisableSelector(SelectorType.Face, true);
                EnableOrDisableSelector(SelectorType.Value, true);
            }
            CheckValidBid();
            DisplayStat();
        }
        private void EnableOrDisableSelector(SelectorType type, bool isEnable)
        {
            if (type == SelectorType.Value)
            {
                _increaseValueButton.interactable = isEnable;
                _decreaseValueButton.interactable= isEnable;
            }
            else
            {
                _increaseFaceButton.interactable = isEnable;
                _decreaseFaceButton.interactable = isEnable;
            }
        }

        public void Bet()
        {
            if (_isFirstBidOfRound || _value != _initialBid.Value || _face != _initialBid.Face)
            {

            Debug.Log($"Player bet : {_value} - {_face}");
            _playerFSM.Bet(new Bid(_value, _face));
            _selectorPanel.SetActive(false);
            }
        }
        public void Dudo()
        {
            if (!_isFirstBidOfRound)
            {
                Debug.Log("Player yells Dudo");
                _playerFSM.Dudo();
                _selectorPanel.SetActive(false);
            }
        }
        private void CheckValidBid()
        {
            if (_value == _initialBid.Value && _face ==  _initialBid.Face && !_isFirstBidOfRound) 
            {
                _betButton.interactable = false;
            }
            else
            {
                _betButton.interactable = true;
            }
        }
        public void DisplayStat()
        {
            float stat = _statCalculator.GetStatFromBid(new Bid(_value, _face));
            _probabilityText.text = $"{stat*100}% ({stat})";
        }
    }
}