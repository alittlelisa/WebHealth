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
    public class RootDialog : LuisDialog<object>
    {
	   [LuisIntent("")]
	   public async Task None(IDialogContext context, LuisResult result)
	   {
		  await context.PostAsync("I do not understand");
		  context.Wait(MessageReceived);
	   }

	   [LuisIntent("greeting")]
	   public async Task Greeting(IDialogContext context, LuisResult result)
	   {
		  await context.PostAsync("Hello, welcome to the Differential Diagnosis Decision Tree Bot.");
		  context.Wait(MessageReceived);
	   }

        [LuisIntent("exit")]
        public async Task Exit(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("You want to exit");
            context.Wait(MessageReceived);
        }

	   [LuisIntent("differentialDiagnosis")]
	   public async Task DifferentialDiagnosis(IDialogContext context, LuisResult result)
	   {
		  await context.PostAsync("Starting the Differential Diagnosis menu...");
		  await context.Forward(new DiagnosisDialog(), Callback, "", CancellationToken.None);
	   }

	   private async Task Callback(IDialogContext context, IAwaitable<object> result)
	   {
		  await context.PostAsync("Returned to the main menu.");
		  context.Wait(MessageReceived);
	   }
    }
}