using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Dice
{
    public enum DiceFace
    {
        Invalid,
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }
    [RequireComponent(typeof(Rigidbody))]
    public class DiceBehaviour : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        [SerializeField]
        private float _waitingTime=0.2f;
        [SerializeField]
        private float _rollingTime = 5f;
        [SerializeField]
        private float _flickForce = 2f;

        public bool IsLaunched { get; private set; } = false;
        private Vector3 axis;
        private float _actualRolling;
        private DiceFace _face;

        public event Action<DiceFace> OnDiceStop;
        // Start is called before the first frame update
        void Start()
        {
            axis = UnityEngine.Random.insideUnitSphere;
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
           if (IsLaunched == false)
                transform.Rotate(axis, 100f * Time.deltaTime);
        }


        public void Launch()
        {
            IsLaunched = true;
            //gameObject.transform.SetParent(transform.parent.parent);
            //gameObject.tag = "Untagged";
            _actualRolling = 0;
            //_isSpinning = false;
            StartCoroutine(WaitingRollingTime());
        }

        public void StartSpinnig()
        {
            //axis = UnityEngine.Random.insideUnitSphere;
            //_isSpinning = true;
        }
        public IEnumerator WaitingRollingTime()
        {
            yield return new WaitForSeconds(_waitingTime);
            while (_actualRolling < _rollingTime)
            {
                _actualRolling += Time.deltaTime;
                if (_actualRolling < _rollingTime && _rigidbody.velocity.sqrMagnitude <= 0.01f && _rigidbody.angularVelocity.sqrMagnitude <= 0.01f)
                {
                    DiceFace face = GetDiceNumber();

                    if (face == DiceFace.Invalid)
                    {
                        FlickDice();
                    }
                    else
                    {
                        //OnDiceRollFinished?.Invoke(face);
                        OnDiceStop?.Invoke(face);
                        Debug.Log($"face : {face}");
                        break;
                    }
                }
                yield return null;
            }
            //OnRollInvalid?.Invoke();
        }

        private void FlickDice()
        {
            _rigidbody.AddForce(Vector3.up * _flickForce, ForceMode.Impulse);
            _rigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * _flickForce, ForceMode.Impulse);
        }
        private DiceFace GetDiceNumber()
        {
            if (Vector3.Dot(transform.forward, Vector3.up) > 0.9f)
            {
                _face = DiceFace.One;
            }
            else if (Vector3.Dot(transform.up, Vector3.up) > 0.9f)
            {
                _face = DiceFace.Two;
            }
            else if (Vector3.Dot(-transform.right, Vector3.up) > 0.9f)
            {
                _face = DiceFace.Three;
            }
            else if (Vector3.Dot(transform.right, Vector3.up) > 0.9f)
            {
                _face = DiceFace.Four;
            }
            else if (Vector3.Dot(-transform.up, Vector3.up) > 0.9f)
            {
                _face = DiceFace.Five;
            }
            else if (Vector3.Dot(-transform.forward, Vector3.up) > 0.9f)
            {
                _face = DiceFace.Six;
            }
            else
            {
                _face = DiceFace.Invalid;
            }
            return _face;
        }

    }
}