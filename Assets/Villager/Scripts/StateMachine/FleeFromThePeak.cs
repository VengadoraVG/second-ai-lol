using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class FleeFromThePeak : MonoBehaviour, StateMachineBehaviour {
            private GameObject _peak;
            private NavMeshAgent _agent;

            private void Start () {
                _peak = GameObject.FindWithTag(Tags.Peak);
                _agent = GetComponent<NavMeshAgent>();
            }

            public void EnterState () {
                Start();
                NavMeshHit hit;
                NavMesh.Raycast(transform.position, transform.position +
                                (transform.position - _peak.transform.position).normalized * 100, out hit, NavMesh.AllAreas);
                _agent.SetDestination(hit.position);
            }

            public void ExitState () {
                _agent.ResetPath();
            }
        }
    }
}
