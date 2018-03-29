using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

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
		  await context.PostAsync("Starting the main menu...");
		  context.Call(new DiagnosisDialog(), Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Thank you for using the Differnetial Diagnosis Decision Tree Bot. Have a nice day.");
		  context.Done(1);
        }
    }
}