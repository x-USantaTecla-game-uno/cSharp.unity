using Uno.Runtime.Application;

namespace Uno.Runtime.Infrastructure
{
    public class BeginViewImplementation : BeginView
    {
        private readonly Console console;
        
        #region Constructor
        public BeginViewImplementation(Console console)
        {
            this.console = console;
        }
        #endregion
        
        public void NoticeWantNumberOfPlayers(NotificationViewModel viewModel)
        {
            console.Write(viewModel.Message);
        }

        public void NoticeWantNumberOfHumans(NotificationViewModel viewModel)
        {
            console.Write(viewModel.Message);
        }
    }
}
