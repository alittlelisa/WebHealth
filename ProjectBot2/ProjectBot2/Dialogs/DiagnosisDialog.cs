using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace ProjectBot2.Dialogs
{
    [LuisModel("8a4fdce8-e29d-4acd-9c3e-eb2918b6558b", "1477d4114445442fb6ec1021ca89f5ec")]
    [Serializable]
    public class DiagnosisDialog : LuisDialog<object>
    {
	   [LuisIntent("Main Menu")]
	   public async Task MainMenu(IDialogContext context, LuisResult result)
	   {
		  await context.PostAsync("You want to return to the main menu");
		  context.Wait(MessageReceived);
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
				context.Wait(MessageReceived);
			 }
			 else
			 {
				await context.PostAsync("Sorry, I don't recognize that Diagnosis Tree.");
				context.Wait(MessageReceived);
			 }
		  }
	   }

	   private async Task Callback(IDialogContext context, IAwaitable<object> result)
	   {
		  context.Wait(MessageReceived);
	   }
    }
}