using UnityEngine;
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

            public void EnterState () {
                if (ChargeBehaviour == null) 
                    ChargeBehaviour = GetComponent<Charge>();

                // StartCoroutine(Attack());
                ChargeBehaviour.Owner = this;
                ChargeBehaviour.EnterState();
            }

            public void ExitState () {
                ChargeBehaviour.ExitState();
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
