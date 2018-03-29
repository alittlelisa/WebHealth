using System;
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
	   private int greetCount = 0;

	   public override async Task StartAsync(IDialogContext context)
	   {
		  var write = "Welcome to the Differential Diagnosis Decision Tree Bot.  Say \"Help\" for a list of options.";
		  var say = "Please select a diagnosis to begin a decision tree.";
		  await context.SayAsync(text: write, speak: say);
		  context.Wait(this.MessageReceived);
	   }

        [LuisIntent ("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.SayAsync("Sorry, I do not understand.", "Sorry, I didn't quite understand that.");
            context.Wait(MessageReceived);
        }

	   [LuisIntent("Greeting")]
	   public async Task Greeting(IDialogContext context, LuisResult result)
	   {
		  this.greetCount++;
		  if (greetCount == 1)
		  {
			 await context.SayAsync("Hello.", "Hello.");
			 context.Wait(MessageReceived);
		  }
		  else if (greetCount == 2)
		  {
			 await context.SayAsync("Hello... again.", "Hi");
			 context.Wait(MessageReceived);
		  }
		  else if (greetCount == 3)
		  {
			 await context.SayAsync("Didn't we do this already?", "Didn't we do this already?");
			 context.Wait(MessageReceived);
		  }
		  else if (greetCount > 3)
		  {
			 await context.SayAsync("Try saying help for options.", "Hmm... I think we've done this enough.");
			 context.Wait(MessageReceived);
		  }
	   }

	   [[LuisIntent("Help")]]
	   public async Task Help(IDialogContext context, LuisResult result)
	   {
		  await context.PostAsync("You can say: Hello, Help, Exit, Mood, Anxiety, Trauma.");
		  context.Wait(MessageReceived);
	   }


	   [LuisIntent("Exit")]
	   public virtual async Task Exit(IDialogContext context, LuisResult result)
	   {
		  await context.SayAsync("Exit?", "Are you sure you want to exit the application?");
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

	   [LuisIntent("DiagnosisOption")]
	   public async Task DiagnosisOption(IDialogContext context, LuisResult result)
	   {
            foreach (var entity in result.Entities)
		  {
                var value = entity.Entity.ToLower();
			 if (value == "mood")
			 {
				await context.SayAsync("Starting the Mood Disorder Diagnosis Tree...", "Selecting Mood Disorder.");
				context.Call(new MoodDialog(), Callback);
			 }
			 else if (value == "anxiety" || value == "trauma")
			 {
				await context.SayAsync("Sorry, that Diagnosis Tree is not available yet.", "Sorry, that Diagnosis Tree is not available yet.");
				context.Wait(this.MessageReceived);
			 }
			 else
			 {
				await context.SayAsync("Sorry, I don't recognize that Diagnosis Tree.");
				context.Wait(this.MessageReceived);
			 }
		  }
	   }

	   private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Returned to the diagnosis menu.");
            context.Wait(MessageReceived);
        }
    }
}