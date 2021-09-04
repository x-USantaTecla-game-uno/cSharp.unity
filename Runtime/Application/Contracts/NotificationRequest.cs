namespace Uno.Runtime.Application
{
    public class NotificationRequest
    {
        public string Message
        {
            get;
        }
        
        #region Constructors
        public NotificationRequest(string message)
        {
            Message = message;
        }
        #endregion
    }
}
