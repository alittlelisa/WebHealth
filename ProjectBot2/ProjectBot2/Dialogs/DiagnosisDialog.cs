using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace ProjectBot2.Dialogs
{
    [LuisModel("5f07ece2-da5b-474e-9056-00b25f95ec36", "389b90d438fe479ca57a8a881ffaceb6")]
    [Serializable]
    public class DiagnosisDialog : LuisDialog<object>
    {
	   public override async Task StartAsync(IDialogContext context)
	   {
		  await context.PostAsync("Please select a diagnosis to begin a decision tree.");
		  context.Wait(this.MessageReceived);
	   }

	   [LuisIntent("MainMenu")]
	   public async Task MainMenu(IDialogContext context, LuisResult result)
	   {
		  await context.PostAsync("You want to return to the main menu");
		  context.Wait(this.MessageReceived);
		  return;
	   }

	   [LuisIntent("DiagnosisOption")]
	   public async Task DiagnosisOption(IDialogContext context, LuisResult result)
	   {
		  foreach (var entity in result.Entities.Where(Entity => Entity.Type == "Diagnosis"))
		  {
			 var value = entity.Entity.ToLower();
			 if (value == "mood")
			 {
				await context.PostAsync("Starting the Mood Disorder Diagnosis Tree...");
				context.Call(new MoodDialog(), Callback);
			 }
			 else if (value == "anxiety" || value == "trauma")
			 {
				await context.PostAsync("Sorry, that Diagnosis Tree is not available yet.");
				context.Wait(this.MessageReceived);
			 }
			 else
			 {
				await context.PostAsync("Sorry, I don't recognize that Diagnosis Tree.");
				context.Wait(this.MessageReceived);
			 }
		  }
	   }

	   //PromptDialog.PromptConfirm();

	   private async Task Callback(IDialogContext context, IAwaitable<object> result)
	   {
		  context.Wait(this.MessageReceived);
	   }
    }
}