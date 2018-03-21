using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;


namespace ProjectBot2.Dialogs
{
    [Serializable]
    public class MoodDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }


        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            await context.PostAsync("Can all symptoms be accounted for by Schizo-affective Disorder?");
            context.Wait(MessageReceivedAsync);
            var response = await argument;
            if (response.Text == "Yes")
            {
                await context.PostAsync("Has the criteria been met for one manic episode?");
            }
        }
    }
}