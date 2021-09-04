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
            var result = await GetResult();
            return int.Parse(result);
        }
        
        public async Task<string> Read()
        {
            await WaitUntilInput();
            var result = await GetResult();
            return result;
        }

        #region SupportMethods
        private async Task WaitUntilInput()
        {
            while (!Input.anyKeyDown) 
            {
                await Task.Delay(1);
            }
        }
        
        private async Task<string> GetResult()
        {
            var result = Input.inputString;
            await Task.Delay(1);
            return result;
        }
        #endregion
    }
}
