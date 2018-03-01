using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace ProjectBot2.Dialogs
{
    [LuisModel("8a4fdce8-e29d-4acd-9c3e-eb2918b6558b", "1477d4114445442fb6ec1021ca89f5ec")]
    [Serializable]
    public class MoodDialog : LuisDialog<object>
    {
	   [LuisIntent("Main Menu")]
	   public async Task MainMenu(IDialogContext context, LuisResult result)
	   {
		  await context.PostAsync("You want to return to the main menu");
		  context.Wait(MessageReceived);
		  return;
	   }

	   [LuisIntent("Back")]
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

	   [LuisIntent("Confirm")]
	   public async Task Confirm(IDialogContext context, LuisResult result)
	   {
		  await context.PostAsync("You want to confirm/say yes");
		  context.Wait(MessageReceived);
	   }

	   [LuisIntent("Deny")]
	   public async Task Deny(IDialogContext context, LuisResult result)
	   {
		  await context.PostAsync("You want to cancel/say no");
		  context.Wait(MessageReceived);
	   }
    }
}