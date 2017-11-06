using UnityEngine;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class TemporalGollemController : MonoBehaviour {
            private DontWalkToThePeak _behaviour;
            private Base _base;

            void Start () {
                _behaviour = GetComponent<DontWalkToThePeak>();
                _behaviour.EnterState();
                _base = GetComponent<Base>();
                _base.OnSpotTaken += WaitForAttack;
            }

            public void WaitForAttack (Spot spot, Surrounder attacker) {
                attacker.GetComponent<Attacker>().OnStartAttacking += StopMoving;
            }

            public void StopMoving () {
                _behaviour.ExitState();
            }
        }
    }
}