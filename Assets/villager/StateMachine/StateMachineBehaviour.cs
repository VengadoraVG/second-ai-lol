using UnityEngine;
using System.Collections;

namespace Villager {
    namespace StateMachine {
        public interface StateMachineBehaviour {
            void EnterState ();
            void ExitState ();
        }
    }
}
