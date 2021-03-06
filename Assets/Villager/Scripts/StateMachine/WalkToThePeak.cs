using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class WalkToThePeak : MonoBehaviour, StateMachineBehaviour {
            private GameObject _peak;
            private NavMeshAgent _agent;

            private void Start () {
                _peak = GameObject.FindWithTag(Tags.Peak);
                _agent = GetComponent<NavMeshAgent>();
            }

            public void EnterState () {
                Start();
                _agent.SetDestination(_peak.transform.position);
            }

            public void ExitState () {
                _agent.ResetPath();
            }
        }
    }
}
