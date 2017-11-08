using UnityEngine;
using System.Collections;

namespace Villager {
    namespace StateMachine {

        #pragma warning disable 0252
        public class TemporalGollemController : Machine {
            private FleeFromThePeak _move;
            private TemporalGollemAttack _attacker;

            private Base _base;
            public int _attackers;

            void Start () {
                _move = GetComponent<FleeFromThePeak>();
                _attacker = GetComponent<TemporalGollemAttack>();

                _base = GetComponent<Base>();
                _base.OnSpotTaken += WaitForAttack;
                _base.OnSpotFreed += SpotReleasedHandler;

                SwitchToState(_move);
            }

            public void WaitForAttack (Spot spot, Surrounder attacker) {
                attacker.GetComponent<Attacker>().OnStartAttacking += StopMoving;
            }

            public void StopMoving () {
                _attackers++;
                if (_currentBehaviour != _attacker) {
                    SwitchToState(_attacker);
                }
            }

            public void SpotReleasedHandler (Spot spot, Surrounder released) {
                _attackers--;
                if (_attackers <= 0) {
                    KeepMoving();
                }
            }

            public void KeepMoving () {
                SwitchToState(_move);
            }
        }
    }
}
