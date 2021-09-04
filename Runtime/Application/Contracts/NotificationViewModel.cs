namespace Uno.Runtime.Application
{
    public class NotificationViewModel
    {
        public string Message
        {
            get;
        }
        
        #region Constructors
        public NotificationViewModel(string message)
        {
            Message = message;
        }
        #endregion
    }
}
