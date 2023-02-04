using CurrieTechnologies.Razor.SweetAlert2;
namespace Wordle.UI.Helpers
{
    public class NotificationAlert
    {


        private SweetAlertService _swl;
        public NotificationAlert(SweetAlertService swl)
        {
            _swl = swl;
        }

        public async Task<SweetAlertResult> BasicNotificationAsync(string title,string message,SweetAlertIcon notificationType)
        {
            return await _swl.FireAsync(title,message,notificationType);
        }

    }
}
