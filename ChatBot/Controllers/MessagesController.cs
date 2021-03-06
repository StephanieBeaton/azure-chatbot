﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Bot.Builder.Dialogs;

namespace ChatBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            
            if (activity.Type == ActivityTypes.Message)
            {
                if (activity.Text == "help")
                {
                    Activity reply = activity.CreateReply("You may type commands such as back, human, or error code if you have a code to paste in. To return, select an answer from the previous message.'");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (activity.Text == "back")
                {
                    //Code to query the DB for the parent node
                }
                else if (activity.Text == "human")
                {
                    Activity reply = activity.CreateReply("I'm sorry I couldn't help you solve your problem. You will be connected to a human momentarily.");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (activity.Text == "error code")
                {
                    Activity reply = activity.CreateReply("It looks like you want to paste your error code. Please do that now.");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else
                {
                    Task<supportLUIS> luisAnswer = LUISTypeParser.ParseUserInput(activity.Text);
                    await Conversation.SendAsync(activity, () => new NodeChatBot());
                }
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

       
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing that the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
                //Handle ping
            }

            return null;
        }

    }
}