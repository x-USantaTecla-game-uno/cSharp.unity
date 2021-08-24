using UnityEngine;
using Uno.Runtime.Application;

namespace Uno.Runtime.Infrastructure
{
    public class EntryPoint: MonoBehaviour
    {
        void Awake()
        {
            var beginInputPort = new BeginInteractor();
            var beginController = new BeginController(beginInputPort, gameObject.AddComponent<Console>());
            StartCoroutine(beginController.Begin());
        }
    }
}
