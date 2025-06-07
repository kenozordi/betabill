using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using betabill.console;
using Microsoft.KernelMemory.AI.Ollama;
using Microsoft.KernelMemory.Configuration;
using Microsoft.KernelMemory;


var ollamaConfig = new OllamaConfig()
{
    TextModel = new OllamaModelConfig("mistral:latest") { MaxTokenTotal = 125000, Seed = 42, TopK = 7 },
    EmbeddingModel = new OllamaModelConfig("nomic-embed-text:latest") { MaxTokenTotal = 2048 },
    Endpoint = "http://localhost:11434/"
};

var memoryBuilder = new KernelMemoryBuilder()
    .WithOllamaTextGeneration(ollamaConfig)
    .WithOllamaTextEmbeddingGeneration(ollamaConfig)
    .WithSearchClientConfig(new SearchClientConfig() { AnswerTokens = 4096 })
    .WithCustomTextPartitioningOptions(new TextPartitioningOptions() { MaxTokensPerParagraph = 20, OverlappingTokens = 10 });
var memory = memoryBuilder.Build();

var index = "ragwithollama";
var document = new Document().AddFiles(["C:/Users/user/Downloads/CONFIDENTIALITY AND NON-DISCLOSURE AGREEMENT (Kene).docx"]);
var documentID = await memory.ImportDocumentAsync(document, index: index);
var chatHistory = new ChatHistoryContext();

var userInput = "who are the parties involved in this document?";

var fullQuery = chatHistory.GetHistoryAsContext() + "\nUser: " + userInput;
var answer = await memory.AskAsync(fullQuery, index, MemoryFilters.ByDocument(documentID), minRelevance: .6f);

chatHistory.AddUserMessage(userInput);
chatHistory.AddAssistantMessage(answer.Result);
Console.WriteLine(answer.Result);
Console.WriteLine("End");