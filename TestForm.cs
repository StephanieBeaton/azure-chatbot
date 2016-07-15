using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Json;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using Microsoft.Bot.Builder.Dialogs;
using System.Web.Http.Description;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Web;

namespace AzureTSBot
{
    public class TestForm
    {
        public static IForm<JObject> BuildJsonForm()
        {
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AzureTSBot.AzureTroubleshootingForm.json"))
            {
                var schema = JObject.Parse(new StreamReader(stream).ReadToEnd());
                return new FormBuilderJson(schema)
                    .AddRemainingFields()
                    .Build();
            }
        }
    }
}