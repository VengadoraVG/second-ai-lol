using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class WalkToThePeak : MonoBehaviour, StateMachineBehaviour {
            public GameObject Peak;

            private NavMeshAgent _agent {
                get {
                    if (_cachedAgent == null)
                        _cachedAgent = GetComponent<NavMeshAgent>();
                    return _cachedAgent;
                }
            }
            private NavMeshAgent _cachedAgent;

            public void EnterState () {
                _agent.SetDestination(Peak.transform.position);
            }

            public void ExitState () {
                _agent.ResetPath();
            }
        }
    }
}
