using Myorudo.Interfaces.Dice;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Dice
{
    public class RandomRoll : MonoBehaviour, IRollDice
    {
        [SerializeField]
        private AudioClip _throwSoundEffect;
        public event Action<List<int>> OnRollResult;
        private List<int> _diceResult;
        private PlayAudioEffect _audioEffectPlayer;
        // Start is called before the first frame update
        void Start()
        {
            _diceResult = new List<int>();
            _audioEffectPlayer = GetComponent<PlayAudioEffect>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Implement IRollDice
        /// <summary>
        /// Generate nbOfDices random number between 1 & 6
        /// </summary>
        /// <param name="nbOfDices"></param>
        /// <returns>list of generated values</returns>
        public void RollDice(int nbOfDices)
        {
            _diceResult.Clear();
            for (int  i = 0; i < nbOfDices; i++)
            {
                _diceResult.Add(UnityEngine.Random.Range(1, 7));
            }
            //return _diceResult;
            if (_audioEffectPlayer != null)
            {
                _audioEffectPlayer.PlayEffect(_throwSoundEffect);
            }
            OnRollResult?.Invoke(_diceResult);
        }
        #endregion
    }
}