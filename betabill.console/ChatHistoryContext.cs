using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace betabill.console
{
    public class ChatHistoryContext
    {
        private readonly Collection<string> _messages = [];
        public void AddUserMessage(string message) => _messages.Add($"User: {message}");
        public void AddAssistantMessage(string message) => _messages.Add($"Assistant: {message}");
        public string GetHistoryAsContext(int maxMessages = 10) => string.Join("\n", _messages.TakeLast(maxMessages));
    }
}
