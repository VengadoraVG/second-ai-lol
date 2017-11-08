using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class Die : MonoBehaviour, StateMachineBehaviour {
            private NavMeshAgent _agent;

            void Start () {
                _agent = GetComponent<NavMeshAgent>();
            }

            public void EnterState () {
                _agent.ResetPath();
                GetComponent<TestIndicator>().Indicate(Color.black, true);
            }

            public void ExitState () {
                _agent.enabled = false;
            }
        }
    }
}
