using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Datas
{
    [CreateAssetMenu(menuName = "Data/New Dice")]
    public class DiceData : ScriptableObject
    {
        [SerializeField]
        private int _numberOfFace;
        [Tooltip("Sprite of each face, in ascendent order")]
        [SerializeField]
        private List<Sprite> _spriteFace;


        public int NumberOfFace { get => _numberOfFace; }
        public Sprite this[int index] { get => _spriteFace[index - 1]; }

    }
}