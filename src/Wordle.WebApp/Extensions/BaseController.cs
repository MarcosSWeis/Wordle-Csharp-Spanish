using Microsoft.AspNetCore.Mvc;

namespace Wordle.WebApp.Extensions
{
    public enum NotificationType
    {
        Success,
        Error,
        Info,
        Warning
    }
    public class BaseController :Controller
    {
        public void BasicNotification(string message,NotificationType notification,string title)
        {
            TempData["notification"] = $"Swal.fire({title},{message},{notification.ToString().ToLower()})";
        }
    }
}
