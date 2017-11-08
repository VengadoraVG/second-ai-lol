using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Villager {
    namespace StateMachine {
        public class TemporalGollemAttack : MonoBehaviour, StateMachineBehaviour {
            public int Cooldown = 2;
            public int Damage = 3;

            private Base _base;
            private Coroutine _attackCoroutine;

            void Start () {
                _base = GetComponent<Base>();
            }

            public void EnterState () {

                _attackCoroutine = StartCoroutine(Attack());
            }

            public void ExitState () {
                StopCoroutine(_attackCoroutine);
            }

            public IEnumerator Attack () {
                yield return new WaitForSeconds(Cooldown);

                List<int> available = new List<int>();
                for (int i=0; i<_base.Spots.Length; i++) {
                    if (_base.Spots[i].IsOccupied)
                        available.Add(i);
                }

                if (available.Count > 0) {
                    HPCounter choosen =
                        _base.Spots[available[Random.Range(0, available.Count)]]
                        .Occupier.GetComponent<HPCounter>();
                    choosen.TakeDamage(Damage);

                    _attackCoroutine = StartCoroutine(Attack());
                }
            }
        }
    }
}
