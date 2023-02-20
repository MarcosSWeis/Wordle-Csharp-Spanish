using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Service.Enums;

namespace Wordle.Service.Interface
{
    public interface INotification
    {
        public Task SwalFireAsync(string title,string message,NotificationType icon);
        public Task SwalFireAsync(string title,string message,NotificationType icon,PositionSweetAlert position);


    }
}
