using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class WalkToThePeak : MonoBehaviour, StateMachineBehaviour {
            protected GameObject _peak;
            protected NavMeshAgent _agent;

            protected void Start () {
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
