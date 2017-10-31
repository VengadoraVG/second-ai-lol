using UnityEngine;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class Attacker : StateMachineBehaviour {
            public HPCounter CurrentTarget;
            public float AttackCooldown = 1;
            public float DamagePerHit = 1;

            public void SetTarget (HPCounter target) {
                CurrentTarget = target;
            }

            public override void EnterState () {
                StartCoroutine(Attack());
            }

            public override void ExitState () {
                StopAllCoroutines();
            }

            public IEnumerator Attack () {
                yield return new WaitForSeconds(AttackCooldown);
                CurrentTarget.TakeDamage(DamagePerHit);
                StartCoroutine(Attack());
            }
        }
    }
}
