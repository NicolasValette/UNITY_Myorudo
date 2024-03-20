using Myorudo.Datas;
using Myorudo.FSM;
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
        private Button _button;

        [SerializeField]
        PlayerSFM _playerFSM;
        [SerializeField]
        private GameRulesData _gameRulesData;
        

        private Bid _initialBid;

        private int _value;
        private int _face;
        // Start is called before the first frame update
        private void Start()
        {
            _value = 3;
            _face = 1;

            
            _initialBid = new Bid(_value, _face);
            _selectorValueFieldText.text = _value.ToString();
            _selectorFaceFieldText.text = _face.ToString();
            _previousBidText.text = $"{_initialBid.Value} - {_initialBid.Face}";
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
            if (_value <= _initialBid.Value)
            {
                _value = _gameRulesData.NumberOfStartingDices;
            }
            else if (_value > _gameRulesData.NumberOfStartingDices)
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
                    _face = 1;
                }
                else if (_face > _gameRulesData.NumberOfFace)
                {
                    _face = _gameRulesData.NumberOfFace;
                }
                if (_face > _initialBid.Face)
                {
                    _decreaseFaceButton.GetComponentInChildren<TMP_Text>().text = "-";
                }
                if (_face == _initialBid.Face)
                {
                    _decreaseFaceButton.GetComponentInChildren<TMP_Text>().text = "Paco";
                }
                _selectorValueFieldText.text = _value.ToString();
                _selectorFaceFieldText.text = _face.ToString();

            }
            else
            {
                if (_face == 1)
                {
                    _face = _initialBid.Face;
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
                if (_face < _initialBid.Face)
                {
                    _face = 1;
                    _decreaseFaceButton.interactable = false;
                    _value = (_value / 2);
                    _selectorValueFieldText.text = _value.ToString();
                }

                _selectorFaceFieldText.text = _face.ToString();
            }
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
            Debug.Log($"Player bet : {_value} - {_face}");
        }
    }
}