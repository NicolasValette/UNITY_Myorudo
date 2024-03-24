using Myorudo.Datas;
using Myorudo.Interfaces.Dice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Dice
{
    public class MoveDice : MonoBehaviour, IMoveDice
    {
        #region Serialized Fields
        [SerializeField]
        private GameObject _dicePrefabs;
        [SerializeField]
        private float _offset = 5f;
        [SerializeField]
        private DicePhysicsData _dicePhysicsData;
        #endregion

        private GameObject _currentDice = null;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void TakeDice(Vector3 position)
        {
            _currentDice = Instantiate(_dicePrefabs, position, Quaternion.identity);
        }
        public void MoveHeldDice (Vector3 position)
        {
            _currentDice.transform.position = position + (Vector3.up * _offset);
        }
        public void Roll(Vector2 deltaCursorDirection)
        {
            
            Vector3 dir = new Vector3(deltaCursorDirection.x, 0f, deltaCursorDirection.y);
            //_listOfDice[i].GetComponentInChildren<Collider>().enabled = true;
            //_listOfDice[i].GetComponent<Rigidbody>().useGravity = true;
            //_listOfDice[i].GetComponent<Rigidbody>().isKinematic = false;
            var rb = _currentDice.AddComponent<Rigidbody>();
            rb.AddForce(dir.normalized * _dicePhysicsData.LaunchForce, ForceMode.Impulse);
            rb.AddTorque(UnityEngine.Random.insideUnitSphere * _dicePhysicsData.TorqueForce, ForceMode.Impulse);
        }

    }
}