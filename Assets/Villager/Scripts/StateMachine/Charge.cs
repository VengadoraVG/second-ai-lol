using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class Charge : MonoBehaviour, StateMachineBehaviour {
            public Attacker Owner;

            private NavMeshAgent _agent;
            private bool _isActive = false;

            void Start () {
                _agent = GetComponent<NavMeshAgent>();
            }

            void Update () {
                if (_isActive) {
                    _agent.SetDestination(Owner.CurrentSpot.Position);
                    transform.forward =
                        Owner.CurrentSpot.Owner.transform.position -
                        transform.position;
                }
            }

            public void EnterState () {
                if (Owner.CurrentSpot != null) {
                    _agent.SetDestination(Owner.CurrentSpot.Position);
                    _isActive = true;
                } else {
                    Owner.StopAllCoroutines();
                    ExitState();
                }
            }

            public void ExitState () {
                _agent.ResetPath();
                _isActive = false;
            }
        }
    }
}
