using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class WalkToThePeak : StateMachineBehaviour {
            public GameObject Peak;

            private NavMeshAgent _agent {
                get {
                    if (_cachedAgent == null)
                        _cachedAgent = GetComponent<NavMeshAgent>();
                    return _cachedAgent;
                }
            }
            private NavMeshAgent _cachedAgent;

            public override void EnterState () {
                _agent.SetDestination(Peak.transform.position);
            }

            public override void ExitState () {
                _agent.ResetPath();
            }
        }
    }
}
