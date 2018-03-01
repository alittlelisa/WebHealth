using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace ProjectBot2.Dialogs
{
    [LuisModel("5f07ece2-da5b-474e-9056-00b25f95ec36", "389b90d438fe479ca57a8a881ffaceb6")]
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