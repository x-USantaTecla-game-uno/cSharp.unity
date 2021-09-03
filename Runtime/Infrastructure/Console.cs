using System.Collections;
using UnityEngine;

namespace Uno.Runtime.Infrastructure
{
    public class Console: MonoBehaviour
    {
        public string CharacterRead { get; private set; }
        
        #region Constructors
        public Console()
        {
            CharacterRead = "";
        }
        #endregion
        
        public void Write(string message)
        {
            Debug.Log(message);    
        }

        public IEnumerator Read()
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            CharacterRead = Input.inputString;
        }
    }
}
