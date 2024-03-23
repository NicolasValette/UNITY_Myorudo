using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Datas
{
    [CreateAssetMenu(menuName = "Data/New IA Player")]
    public class IAPlayerData : ScriptableObject
    {
        [SerializeField]
        private float _waitingTime = 1f;

        public float WaitingTime { get => _waitingTime; }
    }
}