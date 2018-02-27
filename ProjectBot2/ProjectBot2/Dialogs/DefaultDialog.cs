using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace ProjectBot2.Dialogs
{
    [LuisModel("{8a4fdce8-e29d-4acd-9c3e-eb2918b6558b}", "{1477d4114445442fb6ec1021ca89f5ec}")]
    [Serializable]
    public class DefaultDialog : LuisDialog<object>
    {
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
       
    }
}