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

            public delegate void StartAttackingDelegate ();
            public event StartAttackingDelegate OnStartAttacking;

            private NavMeshAgent _agent;
            private Coroutine _attackingCoroutine;
            private bool _isActive = false;

            void Start () {
                _agent = GetComponent<NavMeshAgent>();
                _attackingCoroutine = null;
            }

            void Update () {
                if (_isActive) {
                    if (_attackingCoroutine == null &&
                        CurrentTarget != null &&
                        Util.Distance(_agent.path) < 2) {
                        StartAttacking();
                        GetComponent<TestIndicator>().Indicate(Color.red, true);
                    } else if (_attackingCoroutine != null &&
                               Util.Distance(_agent.path) >= 2) {
                        GetComponent<TestIndicator>().Indicate(Color.blue, true);
                        StopCoroutine(_attackingCoroutine);
                        _attackingCoroutine = null;
                    }
                }
            }

            public void EnterState () {
                if (ChargeBehaviour == null) 
                    ChargeBehaviour = GetComponent<Charge>();

                ChargeBehaviour.Owner = this;
                ChargeBehaviour.EnterState();
                _isActive = true;
            }

            public void ExitState () {
                ChargeBehaviour.ExitState();
                CurrentSpot.Owner.ReleaseSpot(CurrentSpot); // :'v
                StopCoroutine(_attackingCoroutine);
                _isActive = false;
            }

            public void StartAttacking () {
                if (OnStartAttacking != null) OnStartAttacking();
                _attackingCoroutine = StartCoroutine(Attack());
            }

            public void SetTarget (Spot target) {
                CurrentSpot = target;
                CurrentTarget = target.Owner.GetComponent<HPCounter>();
            }

            public IEnumerator Attack () {
                yield return new WaitForSeconds(AttackCooldown);
                CurrentTarget.TakeDamage(DamagePerHit);
                _attackingCoroutine = StartCoroutine(Attack());
            }
        }
    }
}
