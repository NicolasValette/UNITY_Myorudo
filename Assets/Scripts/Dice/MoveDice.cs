using Myorudo.Datas;
using Myorudo.Interfaces.Dice;
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

        private List <GameObject> _dicesGameObjectList;
        private GameObject _currentDice = null;
        // Start is called before the first frame update
        void Start()
        {
            _dicesGameObjectList = new List <GameObject>();
        }


        // Update is called once per frame
        void Update()
        {

        }
        public void SpawnDice(int number, Vector3 point, float radius)
        {
            for (int i = 0; i < number; i++)
            {

                /* Distance around the circle */
                var radians = 2 * Mathf.PI / number * i;

                /* Get the vector direction */
                var vertical = Mathf.Sin(radians);
                var horizontal = Mathf.Cos(radians);

                var spawnDir = new Vector3(horizontal, 0, vertical);

                /* Get the spawn position */
                var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

                /* Now spawn */
                var dice = Instantiate(_dicePrefabs, spawnPos, Quaternion.identity);
                dice.GetComponent<Rigidbody>().useGravity = false;
                dice.transform.parent = _currentDice.transform;
                _dicesGameObjectList.Add(dice);
            }

        }
        public List<GameObject> TakeDice(Vector3 position, int numberOfDices)
        {
            Cursor.visible = false;
            _dicesGameObjectList.Clear();
            _currentDice = new GameObject("DicePool");
            SpawnDice(numberOfDices, position, 5f);
            return _dicesGameObjectList;
        }
        public void MoveHeldDice(Vector3 position)
        {
            _currentDice.transform.position = position + (Vector3.up * _offset);
        }
        public void Roll(Vector2 deltaCursorDirection)
        {

            Vector3 dir = new Vector3(deltaCursorDirection.x, 0f, deltaCursorDirection.y);
            //_listOfDice[i].GetComponentInChildren<Collider>().enabled = true;
            //_listOfDice[i].GetComponent<Rigidbody>().useGravity = true;
            //_listOfDice[i].GetComponent<Rigidbody>().isKinematic = false;
            for (int i=0; i<_dicesGameObjectList.Count; i++)
            {
                var rb = _dicesGameObjectList[i].GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.AddForce(dir.normalized * _dicePhysicsData.LaunchForce, ForceMode.Impulse);
                rb.AddTorque(UnityEngine.Random.insideUnitSphere * _dicePhysicsData.TorqueForce, ForceMode.Impulse);
                _dicesGameObjectList[i].GetComponent<DiceBehaviour>().Launch();
            }
            Cursor.visible = true;
        }

    }
}