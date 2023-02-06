using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.JSInterop;
using Wordle.Service.Enums;
using Wordle.Service.Interface;

namespace Wordle.Service
{
    public class Notification :INotification
    {
        private readonly IJSRuntime _js;

        public Notification(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SwalFireAsync(string title,string message,NotificationType icon)
        {
            await _js.InvokeVoidAsync("Swal.fire",new { title = title,text = message,icon = icon.ToString().ToLower() });
        }



    }
}