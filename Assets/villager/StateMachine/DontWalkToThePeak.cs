using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class DontWalkToThePeak : WalkToThePeak {
            void Update () {
            }

            public void EnterState () {
                Start();
                NavMeshHit hit;
                NavMesh.Raycast(transform.position, transform.position +
                                (transform.position - _peak.transform.position).normalized * 100, out hit, NavMesh.AllAreas);
                _agent.SetDestination(hit.position);
            }
        }
    }
}
