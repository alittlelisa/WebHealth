using System;
using System.Net.Http;
using System.Threading;
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
            var reply = "Please say begin when you are ready to start.";
		  await context.SayAsync(text: reply, speak: reply);
		  context.Wait(MessageReceivedToStart);
        }

	   public virtual async Task MessageReceivedToStart(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  var response = await argument;
		  if (response.Text.ToLower().Contains("begin"))
		  {
			 var reply = "Have there been any clinically significant mood symptoms?";
			 await context.SayAsync(text: reply, speak: reply);
			 context.Wait(D0);
		  }
		  else
		  {
                var reply = "Sorry, I didn't quite understand that.";
			 await context.SayAsync(text: reply, speak: reply);
			 context.Wait(MessageReceivedToStart);
		  }
        }

	   public virtual async Task D0(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  this.CurrentQuestion = 0;
		  var response = await argument;
		  if (response.Text.ToLower().Contains("yes"))
		  {
			 var reply = "Can all symptoms be accounted for by a diagnosis of Schizoaffective Disorder?";
			 await context.SayAsync(text: reply, speak: reply);
			 context.Wait(D1);
		  }
		  else if (response.Text.ToLower().Contains("no"))
		  {
                var reply = "The Substance Use Disorder Tree is required to continue.";
                await context.SayAsync(text: reply, speak: reply);
                context.Done(0);
		  }
		  else
		  {
                var reply = "Sorry, I didn't quite understand that.";
                await context.SayAsync(text: reply, speak: reply);
                context.Wait(D0);
		  }
	   }

	   public virtual async Task D1(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  this.CurrentQuestion = 1;
		  var response = await argument;
		  if (response.Text.ToLower().Contains("yes"))
		  {
                var reply = "The Substance Use Disorder Tree is required to continue.";
                await context.SayAsync(text: reply, speak: reply);
                context.Done(0);
		  }
		  else if (response.Text.ToLower().Contains("no"))
		  {
                var reply = "Let's test for Bipolar Type One Disorder?";
                await context.SayAsync(text: reply, speak: reply);
                reply = "Has the criteria been met for at least one Manic Episode?";
                await context.PostAsync("Has the criteria been met for at least one Manic Episode?");
                await context.SayAsync(text: reply, speak: reply);
                context.Wait(D2);
		  }
		  else
		  {
                var reply = "Sorry, I didn't quite understand that.";
                await context.SayAsync(text: reply, speak: reply);
			 context.Wait(D1);
		  }
	   }

	   public virtual async Task D2(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  if(this.CurrentQuestion == 5)
		  {
			 this.CurrentQuestion = 2;
                var reply = "Is the occurence of this episode better explained by Schizophrenia or a Schizoaffective, Schizophreniform, Delusional, or other Psychotic Disorder?";
                await context.SayAsync(text: reply, speak: reply);
                context.Wait(D3);
		  }
		  else
		  {
			 this.CurrentQuestion = 2;
			 var response = await argument;
			 if (response.Text.ToLower().Contains("yes"))
			 {
                    var reply = "Is the occurence of this episode better explained by Schizophrenia or a Schizoaffective, Schizophreniform, Delusional, or other Psychotic Disorder?";
                    await context.SayAsync(text: reply, speak: reply);
                    context.Wait(D3);
			 }
			 else if (response.Text.ToLower().Contains("no"))
			 {
                    var reply = "Has the critera been met for at least on Hypomanic Episode and at least one Major Depressive Episode?";
                    await context.SayAsync(text: reply, speak: reply);
                    context.Wait(D4);
			 }
			 else
			 {
                    var reply = "Sorry, I didn't quite understand that.";
                    await context.SayAsync(text: reply, speak: reply);
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
                var reply = "This is Bipolar Type 1 Disorder, indicate the type of the most recent episode(manic, major depressive, hypomanic, or unspecified) and we will continue with the chronology.";
                await context.SayAsync(text: reply, speak: reply);
			 //context.Wait(D17);
			 context.Done(0);
		  }
		  else if (response.Text.ToLower().Contains("no"))
		  {
                var reply = "Has the critera been met for at least on Hypomanic Episode and at least one Major Depressive Episode?";
                await context.SayAsync(text: reply, speak: reply);
			 context.Wait(D4);
		  }
		  else
		  {
                var reply = "Sorry, I didn't quite understand that.";
                await context.SayAsync(text: reply, speak: reply);
			 context.Wait(D3);
		  }
	   }

	   public virtual async Task D4(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  this.CurrentQuestion = 4;
		  var response = await argument;
		  if (response.Text.ToLower().Contains("yes"))
		  {
                var reply = "Has there never been a Manic Episode?";
                await context.SayAsync(text: reply, speak: reply);
			 context.Wait(D5);
		  }
		  else if (response.Text.ToLower().Contains("no"))
		  {
                var reply = "I am apparently missing the question.";
                await context.SayAsync(text: reply, speak: reply);
			 //context.Wait(D8);
			 context.Done(0);
		  }
		  else
		  {
                var reply = "Sorry, I didn't quite understand that.";
                await context.SayAsync(text: reply, speak: reply);
			 context.Wait(D4);
		  }
	   }

	   public virtual async Task D5(IDialogContext context, IAwaitable<IMessageActivity> argument)
	   {
		  this.CurrentQuestion = 5;
		  var response = await argument;
		  if (response.Text.ToLower().Contains("yes"))
		  {
                var reply = "Is the occurence of this episode better explained by Schizophrenia or a Schizoaffective, Schizophreniform, Delusional, or other Psychotic Disorder?";
                await context.SayAsync(text: reply, speak: reply);
			 //context.Wait(D6);
			 context.Done(0);
		  }
		  else if (response.Text.ToLower().Contains("no"))
		  {
                var reply = "The criteria has been met for at least one Manic Episode";
                await context.SayAsync(text: reply, speak: reply);
			 await D2(context, argument);
		  }
		  else
		  {
                var reply = "Sorry, I didn't quite understand that.";
                await context.SayAsync(text: reply, speak: reply);
			 context.Wait(D4);
		  }
	   }
    }
}