using UnityEngine;
using System.Collections;

namespace Villager {
    namespace StateMachine {

        #pragma warning disable 0252
        public class Controller : Machine {
            public Surrounder ThisSurrounder;
            public EnemyDetector ThisEnemyDetector;

            public WalkToThePeak NormalBehaviour;
            public Attacker AttackGolemsBehaviour;
            public Attacker AttackDemonBehaviour;

            void Start () {
                ThisEnemyDetector.OnEnemyDetected += EnemyDetectedHandler;
                SwitchToState(NormalBehaviour);
            }

            public void EnemyLostHandler (GameObject lost) {
                lost.GetComponent<Base>().OnSpotFreed -= SpotFreedHandler;
            }

            public void EnemyDetectedHandler (GameObject detected) {
                // expecting a reference comparisson
                if (_currentBehaviour == NormalBehaviour ||
                    _currentBehaviour == AttackDemonBehaviour) {

                    Base b = detected.GetComponent<Base>();
                    Spot spot = b.AssignSpot(ThisSurrounder);

                    if (spot == null) {
                        b.OnSpotFreed += SpotFreedHandler;
                    } else {
                        AttackGolemsBehaviour.SetTarget(spot);
                        SwitchToState(AttackGolemsBehaviour);
                    }
                }
            }

            public void SpotFreedHandler (Spot spot, Surrounder released) {
                if (!spot.IsOccupied && (_currentBehaviour == NormalBehaviour ||
                                         _currentBehaviour == AttackDemonBehaviour)) {
                    AttackGolemsBehaviour.CurrentTarget =
                        spot.Owner.GetComponent<HPCounter>();
                    SwitchToState(AttackGolemsBehaviour);
                }
            }
        }
    }
}
