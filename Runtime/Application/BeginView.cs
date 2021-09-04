using Uno.Runtime.Application;

namespace Uno.Runtime.Infrastructure
{
    public interface BeginView
    {
        void NoticeWantNumberOfPlayers(NotificationViewModel viewModel);
        void NoticeWantNumberOfHumans(NotificationViewModel viewModel);
    }
}
