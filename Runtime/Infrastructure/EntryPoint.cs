using UnityEngine;
using Uno.Runtime.Application;

namespace Uno.Runtime.Infrastructure
{
    public class EntryPoint: MonoBehaviour
    {
        void Awake()
        {
            var console = gameObject.AddComponent<Console>();
            var beginView = new BeginViewImplementation(console);
            var beginOutputPort = new BeginPresenter(beginView);
            var beginInputPort = new BeginInteractor(beginOutputPort);
            var beginController = new BeginController(beginInputPort, console);
            beginController.Begin();
        }
    }
}
