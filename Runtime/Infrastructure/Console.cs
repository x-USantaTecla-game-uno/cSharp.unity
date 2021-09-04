using System.Threading.Tasks;
using UnityEngine;

namespace Uno.Runtime.Infrastructure
{
    public class Console: MonoBehaviour
    {
        #region Constructors
        public Console()
        {
        }
        #endregion
        
        public void Write(string message)
        {
            Debug.Log(message);    
        }

        public async Task<int> ReadInteger()
        {
            await WaitUntilInput();
            return int.Parse(Input.inputString);
        }
        
        public async Task<string> Read()
        {
            await WaitUntilInput();
            return Input.inputString;
        }

        private async Task WaitUntilInput()
        {
            while (!Input.anyKeyDown) 
            {
                await Task.Delay(1);
            }
        }
    }
}
