using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

namespace Villager {
    public class EnemyDetector : MonoBehaviour {
        public delegate void EnemyDetectedDelegate (GameObject detected);
        public event EnemyDetectedDelegate OnEnemyDetected;

        public delegate void EnemyLostDelegate (GameObject lost);
        public event EnemyLostDelegate OnEnemyLost;

        public Dictionary<int, GameObject> EnemiesInRange =
            new Dictionary<int, GameObject>();

        void OnTriggerEnter (Collider c) {
            EnemiesInRange[c.GetInstanceID()] = c.gameObject;
            if (OnEnemyDetected != null) OnEnemyDetected(c.gameObject);
        }

        void OnTriggerExit (Collider c) {
            EnemiesInRange.Remove(c.GetInstanceID());
            if (OnEnemyLost != null) OnEnemyLost(c.gameObject);
        }

        public List<GameObject> SortedEnemies () {
            List<GameObject> sorted = new List<GameObject>();

            foreach (KeyValuePair<int, GameObject> entry in EnemiesInRange) {
                sorted.Add(entry.Value);
            }

            sorted.Sort((GameObject a, GameObject b) =>
                        ((a.transform.position - transform.position).magnitude -
                         (b.transform.position - transform.position).magnitude) < 0? -1: 1);

            return sorted;
        }
    }
}
