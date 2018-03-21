using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ProjectBot2.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {

            await context.PostAsync("Starting the main menu");

            context.Call(new MenuDialog(), Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Returned from the main menu.");
            context.Wait(MessageReceivedAsync);
        }
    }
}