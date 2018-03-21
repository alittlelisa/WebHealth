using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace ProjectBot2.Dialogs
{

    [LuisModel("5f07ece2-da5b-474e-9056-00b25f95ec36", "389b90d438fe479ca57a8a881ffaceb6")]
    [Serializable]
    public class MenuDialog : LuisDialog<object>
    {
        private int greetCount = 0;

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I do not understand");
            context.Wait(MessageReceived);
        }

        [LuisIntent("greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            this.greetCount++;
            if (greetCount == 1)
            {
                await context.PostAsync("Hello, welcome to the Differential Diagnosis Decision Tree Bot.");
                context.Wait(MessageReceived);
            }
            else if (greetCount == 2)
            {
                await context.PostAsync("Hi... again.");
                context.Wait(MessageReceived);
            }
            else if (greetCount == 3)
            {
                await context.PostAsync("Didn't we already meet?");
                context.Wait(MessageReceived);
            }
            else if (greetCount > 3)
            {
                await context.PostAsync("Hmm... I think we should start a neuropsychological assessment.");
                context.Wait(MessageReceived);
            }
        }

        [LuisIntent("exit")]
        public async Task Exit(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("You want to exit");
            context.Wait(MessageReceived);
        }

        [LuisIntent("back")]
        public async Task Back(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("You want to return to the last question");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Restart")]
        public async Task Restart(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("You want to restart the diagnostic test");
            context.Wait(MessageReceived);
        }

        [LuisIntent("confirm")]
        public async Task Confirm(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("You want to confirm/say yes");
            context.Wait(MessageReceived);
        }

        [LuisIntent("deny")]
        public async Task Deny(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("You want to cancel/say no");
            context.Wait(MessageReceived);
        }

        [LuisIntent("differentialDiagnosis")]
        public async Task DifferentialDiagnosis(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Starting the Differential Diagnosis menu...");
            context.Call(new DiagnosisDialog(), Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Returned to the main menu.");
            context.Wait(MessageReceived);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            await context.PostAsync("Can all symptoms be accounted for by Schizo-affective Disorder?");
            context.Wait(MessageReceived);
            var response = await argument;
            if (response.Text == "Yes")
            {
                await context.PostAsync("Has the criteria been met for one manic episode?");
            }
        }
    }
}