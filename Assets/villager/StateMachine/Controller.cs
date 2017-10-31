using UnityEngine;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public class StateMachine : MonoBehaviour {
            public Surrounder ThisSurrounder;
            public EnemyDetector ThisEnemyDetector;

            public WalkToThePeak NormalBehaviour;
            public Attacker AttackGolemsBehaviour;
            public Attacker AttackDemonBehaviour;

            void Start () {
                
            }
        }
    }
}
