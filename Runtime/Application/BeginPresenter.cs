using Uno.Runtime.Infrastructure;

namespace Uno.Runtime.Application
{
    public class BeginPresenter: BeginOutputPort
    {
        readonly BeginView view; 
        
        #region Constructor
        public BeginPresenter(BeginView view)
        {
            this.view = view;
        }
        #endregion
        
        public void NoticeWantNumberOfPlayers()
        {
            var request = new NotificationRequest("How many players?");
            var viewModel = CreateNotificationViewModel(request);
            this.view.NoticeWantNumberOfPlayers(viewModel);
        }

        public void NoticeWantNumberOfHumans()
        {
            var request = new NotificationRequest("How many human players?");
            var viewModel = CreateNotificationViewModel(request);
            this.view.NoticeWantNumberOfHumans(viewModel);
        }

        #region SupportMethods
        NotificationViewModel CreateNotificationViewModel(NotificationRequest request)
        {
            return new NotificationViewModel(request.Message);
        } 
        #endregion
    }
}
