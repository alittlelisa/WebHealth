using System;
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
	   public override async Task StartAsync(IDialogContext context)
	   {
		  await context.PostAsync("Hello, welcome to the Differential Diagnosis Decision Tree Bot.");
		  context.Wait(MessageReceived);
	   }

	   [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I do not understand");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Exit")]
        public async Task Quit(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("You want to exit");
            context.Wait(MessageReceived);
        }

	   [LuisIntent("DifferentialDiagnosis")]
	   public async Task DiagnosticList(IDialogContext context, LuisResult result)
	   {
		  context.Call(new DiagnosisDialog(), Callback);
	   }

	   private async Task Callback(IDialogContext context, IAwaitable<object> result)
	   {
		  context.Wait(MessageReceived);
	   }
    }
}