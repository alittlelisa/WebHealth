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

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            this.greetCount++;
            if (greetCount == 1)
            {
                await context.PostAsync("Hello.");
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

        [LuisIntent("Exit")]
	   public virtual async Task Exit(IDialogContext context, LuisResult result)
        {
		  await context.PostAsync("Are you sure you want to exit the application?");
		  PromptDialog.Confirm(
			 context,
			 ExitApp,
			 "Yes/No",
			 "Sorry, I didn't understand that!",
			 promptStyle: PromptStyle.None
		  );
	   }

	   public async Task ExitApp(IDialogContext context, IAwaitable<bool> result)
	   {
		  var confirm = await result;
		  if (confirm)
		  {
			 context.Done(1);
		  }
		  else
		  {
			 await context.PostAsync("Did not exit.");
			 context.Wait(MessageReceived);
		  }
	   }

        [LuisIntent("DifferentialDiagnosis")]
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
    }
}