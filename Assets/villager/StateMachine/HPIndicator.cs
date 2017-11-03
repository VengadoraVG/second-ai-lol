using UnityEngine;
using System.Collections;

namespace Villager {
    public class HPIndicator : MonoBehaviour {
        private TextMesh _textIndicator;
        private HPCounter _counter;

        void Start () {
            _counter = transform.parent.GetComponent<HPCounter>();
            _textIndicator = GetComponent<TextMesh>();
        }

        void Update () {
            _textIndicator.text = _counter.HPLeft + "";
        }
    }
}
