using Myorudo.Interfaces.Dice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Dice
{
    public class RandomRoll : MonoBehaviour, IRollDice
    {

        // Start is called before the first frame update
        void Start()
        {
            
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
        public List<int> RollDice(int nbOfDices)
        {
            List<int> resultList = new List<int>();
            for (int  i = 0; i < nbOfDices; i++)
            {
                resultList.Add(Random.Range(1, 7));
            }
            return resultList;
        }
        #endregion
    }
}