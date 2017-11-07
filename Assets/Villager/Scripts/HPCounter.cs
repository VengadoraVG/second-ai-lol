using UnityEngine;
using System.Collections;

namespace Villager {
    public class HPCounter : MonoBehaviour {
        public delegate void DeathDelegate (HPCounter caller);
        public event DeathDelegate OnDeath;

        public float TotalHP;
        public float DamageTaken;

        public float HPLeft {
            get {
                return TotalHP - DamageTaken;
            }
        }

        public bool IsDeath {
            get {
                return TotalHP <= DamageTaken;
            }
        }

        public void TakeDamage (float amount) {
            DamageTaken += amount;

            if (IsDeath && OnDeath != null) {
                OnDeath(this);
            }
        }
    }
}
