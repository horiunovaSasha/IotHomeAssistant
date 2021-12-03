using IoTHomeAssistant.Domain.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Syncfusion.Blazor.Notifications;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected SfToast ToastObj;
        protected string ToastContent = "";
        protected string cssClass = "";
        protected string notificationType = "";

        protected override async Task OnInitializedAsync()
        {
            SubscribeOnEvent();
        }

        public async Task SubscribeOnEvent()
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/event-publisher"))
                .Build();

            hubConnection.On<NotificationTypeEnum, string>("notification", (type, message) =>
            {
                notificationType = type.ToString();
                switch (type)
                {
                    case NotificationTypeEnum.Success:
                        cssClass = "e-toast-success";
                        break;
                    case NotificationTypeEnum.Warning:
                        cssClass = "e-toast-warning";
                        break;
                    case NotificationTypeEnum.Danger:
                        cssClass = "e-toast-danger";
                        break;
                    default:
                        cssClass = "e-toast-info";
                        break;
                }

                ToastContent = message;
                StateHasChanged();

                ToastObj.ShowAsync();
            });

            await hubConnection.StartAsync();
        }
    }
}
