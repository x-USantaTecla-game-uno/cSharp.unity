using System.Collections;
using UnityEngine;

namespace Uno.Runtime.Infrastructure
{
    public class Console: MonoBehaviour
    {
        string characterRead;

        #region Properties
        public string CharacterRead
        {
            get => this.characterRead;
        }
        #endregion
        
        #region Constructors
        public Console()
        {
            this.characterRead = "";
        }
        #endregion
        
        public void Write(string message)
        {
            Debug.Log(message);    
        }

        public IEnumerator Read()
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            this.characterRead = Input.inputString;
        }
    }
}
