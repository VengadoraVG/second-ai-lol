using UnityEngine;
using System.Collections;

namespace Villager {
    namespace StateMachine {

        public class Machine : MonoBehaviour {
            protected StateMachineBehaviour _currentBehaviour;
            public string current;

            public void SwitchToState<T> (T behaviour) where T: StateMachineBehaviour {
                if (_currentBehaviour != null) {
                    _currentBehaviour.ExitState();
                }
                behaviour.EnterState();
                _currentBehaviour = behaviour;
            }
        }

    }
}
