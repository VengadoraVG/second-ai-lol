using UnityEngine;
using System.Collections;

namespace Villager {
    public class Spot {
        public bool IsOccupied { get { return Occupier != null; } }

        public Surrounder Occupier;
        public Base Owner;
        public Vector3 Position {
            get {
                return Owner.transform.position + _relativePosition;
            }
        }

        private Vector3 _relativePosition;

        public Spot (int index, Base owner) {
            float angle = (index / (float) owner.Spots.Length) * (Mathf.PI * 2);
            this.Owner = owner;
            _relativePosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * owner.Radius;
        }

        public void SetOccupier (Surrounder occupier) {
            if (Occupier != null) {
                Debug.LogWarning("spot was not empty");
            } else {
                Occupier = occupier;
            }
        }

        public void GetReleased () {
            Occupier = null;
        }
    }
}
