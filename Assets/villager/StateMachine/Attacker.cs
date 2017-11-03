using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        [RequireComponent(typeof(Charge))]
        public class Attacker : Machine, StateMachineBehaviour {
            public float AttackCooldown = 1;
            public float DamagePerHit = 1;

            public Spot CurrentSpot;
            public HPCounter CurrentTarget;
            
            public Charge ChargeBehaviour;

            private NavMeshAgent _agent;
            private Coroutine _attackingCoroutine;

            void Start () {
                _agent = GetComponent<NavMeshAgent>();
            }

            void Update () {
                if (_attackingCoroutine == null &&
                    Util.Distance(_agent.path) < 0.5f) {
                    StartAttacking();
                } else if (_attackingCoroutine != null &&
                           Util.Distance(_agent.path) > 1) {
                    StopCoroutine(_attackingCoroutine);
                }
            }

            public void EnterState () {
                if (ChargeBehaviour == null) 
                    ChargeBehaviour = GetComponent<Charge>();

                ChargeBehaviour.Owner = this;
                ChargeBehaviour.EnterState();
            }

            public void ExitState () {
                ChargeBehaviour.ExitState();
                StopCoroutine(_attackingCoroutine);
            }

            public void StartAttacking () {
                _attackingCoroutine = StartCoroutine(Attack());
            }

            public void SetTarget (Spot target) {
                CurrentSpot = target;
                CurrentTarget = target.Owner.GetComponent<HPCounter>();
            }

            public IEnumerator Attack () {
                yield return new WaitForSeconds(AttackCooldown);
                CurrentTarget.TakeDamage(DamagePerHit);
                StartCoroutine(Attack());
            }
        }
    }
}
