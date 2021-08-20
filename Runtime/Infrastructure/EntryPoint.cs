using UnityEngine;
using Uno.Runtime.Application;

namespace Uno.Runtime.Infrastructure
{
    public class EntryPoint: MonoBehaviour
    {
        private void Awake()
        {
            var beginInputPort = new BeginInteractor();
            var beginController = new BeginController(beginInputPort);
            this.StartCoroutine(beginController.Begin());
        }
    }
}
