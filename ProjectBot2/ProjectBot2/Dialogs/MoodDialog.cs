using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;


namespace ProjectBot2.Dialogs
{
    [Serializable]
    public class MoodDialog : IDialog<object>
    {
	   private int currentQuestion;

	   public int CurrentQuestion { get => currentQuestion; set => currentQuestion = value; }

	   public async Task StartAsync(IDialogContext context)
	   {
		  await context.PostAsync("Please say begin when you are ready to start.");
		  context.Wait(MessageReceivedAsync);
        }

	   public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  var response = await argument;
		  if (response.Text.ToLower().Contains("begin"))
		  {
			 await context.PostAsync("Have there been any clinically significan mood symptoms?");
			 context.Wait(D0);
		  }
		  else
		  {
			 await context.PostAsync("Sorry, I didn't quite understand that.");
			 context.Wait(MessageReceivedAsync);
		  }
        }

	   public virtual async Task D0(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  this.CurrentQuestion = 0;
		  var response = await argument;
		  if (response.Text.ToLower().Contains("yes"))
		  {
			 await context.PostAsync("Can all symptoms be accounted for by a diagnosis of Schizoaffective Disorder?");
			 context.Wait(D1);
		  }
		  else if (response.Text.ToLower().Contains("no"))
		  {
			 await context.PostAsync("The Substance Use Disorder Tree is required to continue.");
			 context.Done(0);
		  }
		  else
		  {
			 await context.PostAsync("Sorry, I didn't quite understand that.");
			 context.Wait(D0);
		  }
	   }

	   public virtual async Task D1(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  this.CurrentQuestion = 1;
		  var response = await argument;
		  if (response.Text.ToLower().Contains("yes"))
		  {
			 await context.PostAsync("The Substance Use Disorder Tree is required to continue.");
			 context.Done(0);
		  }
		  else if (response.Text.ToLower().Contains("no"))
		  {
			 await context.PostAsync("Let's test for Bipolar Type One Disorder?");
			 await context.PostAsync("Has the criteria been met for at least one Manic Episode?");
			 context.Wait(D2);
		  }
		  else
		  {
			 await context.PostAsync("Sorry, I didn't quite understand that.");
			 context.Wait(D1);
		  }
	   }

	   public virtual async Task D2(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  if(this.CurrentQuestion == 5)
		  {
			 this.CurrentQuestion = 2;
			 await context.PostAsync("Is the occurence of this episode better explained by Schizophrenia or a Schizoaffective, Schizophreniform, Delusional, or other Psychotic Disorder?");
			 context.Wait(D3);
		  }
		  else
		  {
			 this.CurrentQuestion = 2;
			 var response = await argument;
			 if (response.Text.ToLower().Contains("yes"))
			 {
				await context.PostAsync("Is the occurence of this episode better explained by Schizophrenia or a Schizoaffective, Schizophreniform, Delusional, or other Psychotic Disorder?");
				context.Wait(D3);
			 }
			 else if (response.Text.ToLower().Contains("no"))
			 {
				await context.PostAsync("Has the critera been met for at least on Hypomanic Episode and at least one Major Depressive Episode?");
				context.Wait(D4);
			 }
			 else
			 {
				await context.PostAsync("Sorry, I didn't quite understand that.");
				context.Wait(D2);
			 }
		  }
		  
	   }

	   public virtual async Task D3(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  this.CurrentQuestion = 3;
		  var response = await argument;
		  if (response.Text.ToLower().Contains("yes"))
		  {
			 await context.PostAsync("This is Bipolar Type 1 Disorder, indicate the type of the most recent episode(manic, major depressive, hypomanic, or unspecified) and we will continue with the chronology.");
			 //context.Wait(D17);
			 context.Done(0);
		  }
		  else if (response.Text.ToLower().Contains("no"))
		  {
			 await context.PostAsync("Has the critera been met for at least on Hypomanic Episode and at least one Major Depressive Episode?");
			 context.Wait(D4);
		  }
		  else
		  {
			 await context.PostAsync("Sorry, I didn't quite understand that.");
			 context.Wait(D3);
		  }
	   }

	   public virtual async Task D4(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  this.CurrentQuestion = 4;
		  var response = await argument;
		  if (response.Text.ToLower().Contains("yes"))
		  {
			 await context.PostAsync("Has there never been a Manic Episode?");
			 context.Wait(D5);
		  }
		  else if (response.Text.ToLower().Contains("no"))
		  {
			 await context.PostAsync("I am aparently missing this question.");
			 //context.Wait(D8);
			 context.Done(0);
		  }
		  else
		  {
			 await context.PostAsync("Sorry, I didn't quite understand that.");
			 context.Wait(D4);
		  }
	   }

	   public virtual async Task D5(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  this.CurrentQuestion = 5;
		  var response = await argument;
		  if (response.Text.ToLower().Contains("yes"))
		  {
			 await context.PostAsync("Is the occurence of this episode better explained by Schizophrenia or a Schizoaffective, Schizophreniform, Delusional, or other Psychotic Disorder?");
			 //context.Wait(D6);
			 context.Done(0);
		  }
		  else if (response.Text.ToLower().Contains("no"))
		  {
			 await context.PostAsync("The criteria has been met for at least one Manic Episode.");
			 await D2(context, argument);
		  }
		  else
		  {
			 await context.PostAsync("Sorry, I didn't quite understand that.");
			 context.Wait(D4);
		  }
	   }
    }
}