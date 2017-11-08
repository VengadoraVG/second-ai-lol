using UnityEngine;
using System.Collections;

namespace Villager {
    public class Base : MonoBehaviour {
        public float Radius = 3;
        public Spot[] Spots;

        public int OccupiedSpots {
            get {
                int c = 0;
                for (int i=0; i<Spots.Length; i++) {
                    if (Spots[i].IsOccupied)
                        c++;
                }

                return c;
            }
        }

        public delegate void SpotFreedDelegate (Spot spot, Surrounder released);
        public event SpotFreedDelegate OnSpotFreed;

        public delegate void SpotTakenDelegate (Spot spot, Surrounder taker);
        public event SpotTakenDelegate OnSpotTaken;

        void Start () {
            Spots = new Spot[(int) ((Mathf.PI * Radius) / Surrounder.Radius)];
            for (int i=0; i<Spots.Length; i++) {
                Spots[i] = new Spot(i, this);
            }
        }

        void OnDrawGizmos () {
            if (Application.isPlaying) {
                for (int i=0; i<Spots.Length; i++) {
                    if (Spots[i].IsOccupied)
                        Gizmos.color = new Color(0, 1, 0, 0.5f);
                    else
                        Gizmos.color = new Color(1, 0, 0, 0.5f);
                    Gizmos.DrawSphere(Spots[i].Position, 0.2f + (0.8f * (i+1) / Spots.Length));
                }
            }
        }

        public void ReleaseSpot (Spot spot) {
            Surrounder released = spot.Occupier;
            spot.GetReleased();
            if (released != null && OnSpotFreed != null) OnSpotFreed(spot, released);
        }

        public Spot AssignSpot (Surrounder surrounder) {
            Vector3 arrivalPosition = surrounder.transform.position - transform.position;
            float angle = Util.FullAngle(new Vector2(arrivalPosition.x, arrivalPosition.z));
            int closest = (int) Mathf.Round((angle / (Mathf.PI * 2)) * Spots.Length) % Spots.Length;

            for (int i=0; i<=Spots.Length; i++) {
                int index = (closest + i) % Spots.Length;
                if (!Spots[index].IsOccupied) {
                    Spots[index].SetOccupier(surrounder);
                    if (OnSpotTaken != null) OnSpotTaken(Spots[index], surrounder);
                    return Spots[index];
                }

                index = ((closest -i) + Spots.Length) % Spots.Length;
                if (!Spots[index].IsOccupied) {
                    Spots[index].SetOccupier(surrounder);
                    if (OnSpotTaken != null) OnSpotTaken(Spots[index], surrounder);
                    return Spots[index];
                }
            }

            return null;
        }
    }
}
