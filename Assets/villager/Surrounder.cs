using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

namespace Villager {
    public class Surrounder : MonoBehaviour {
        public delegate void SpotTakenDelegate (Spot taken, Spot oldSpot);
        public event SpotTakenDelegate OnSpotTaken;

        public static int Radius = 1;

        public Spot CurrentSpot;

        public Spot TakeSpot (Base b) {
            Spot taken = b.AssignSpot(this);
            Spot oldSpot = CurrentSpot;
            CurrentSpot = taken;

            if (oldSpot != CurrentSpot && OnSpotTaken != null) {
                OnSpotTaken(CurrentSpot, oldSpot);
            }

            return taken;
        }

        public void Activate (List<GameObject> sortedEnemiesInRange) {
            for (int i=0; i<sortedEnemiesInRange.Count; i++) {
                Spot spot = TakeSpot(sortedEnemiesInRange[i].GetComponent<Base>());
                if (spot != null) {
                    break;
                }
            }
        }
    }
}
