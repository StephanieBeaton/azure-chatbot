using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Threading;
#pragma warning disable 649

namespace Bot_Application1
{
    public enum ListOfAnswers { Answer1, Answer2 }

    [Serializable]
    public class TestForm
    {
        public ListOfAnswers? Question;

        public static IForm<TestForm> BuildForm()
        {
            OnCompletionAsyncDelegate<TestForm> processTestForm = async (context, state) =>
            {
                await context.PostAsync("We are currently processing the Test Form output.");
            };

            return new FormBuilder<TestForm>()
                       .Field(nameof(Question))
                       .OnCompletion(processTestForm)
                       .Build();
        }
    }
}