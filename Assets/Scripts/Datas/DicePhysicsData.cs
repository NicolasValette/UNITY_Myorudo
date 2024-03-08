using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Datas
{
    [CreateAssetMenu(menuName = "Data/New Dice Physics")]
    public class DicePhysicsData : ScriptableObject
    {
        [SerializeField]
        private float _launchForce = 5f;
        [SerializeField]
        private float _torqueForce = 5f;

        public float LaunchForce => _launchForce;
        public float TorqueForce => _torqueForce;
    }
}