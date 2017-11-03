using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class Charge : MonoBehaviour, StateMachineBehaviour {
            public Attacker Owner;

            private NavMeshAgent _agent;

            void Start () {
                _agent = GetComponent<NavMeshAgent>();
            }

            public void EnterState () {
                _agent.SetDestination(Owner.CurrentSpot.Position);
            }

            public void ExitState () {
                _agent.ResetPath();
            }
        }
    }
}
