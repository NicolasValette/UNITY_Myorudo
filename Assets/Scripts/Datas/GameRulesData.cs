using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Myorudo.Datas
{
    [CreateAssetMenu(menuName = "Data/New Game Rules")]
    public class GameRulesData : ScriptableObject
    {
        [Header("Game Rules")]
        [SerializeField]
        private int _numberOfPlayer = 4;
        [Header("Dices")]
        [SerializeField]
        private int _numberOfStartingDices = 5;
        [SerializeField]
        private int _numberOfDiceLost = 1;
        [SerializeField]
        private int _numberOfFace = 6;



        public int NumberOfStartingDices { get => _numberOfStartingDices; }
        public int NumberOfDiceLost { get => _numberOfDiceLost; }
        public int NumberOfFace { get => _numberOfFace; }
        public int NumberOfPlayer { get => _numberOfPlayer; }
    }
}