using UnityEngine;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public abstract class StateMachineBehaviour : MonoBehaviour {
            public abstract void EnterState ();
            public abstract void ExitState ();
        }
    }
}
