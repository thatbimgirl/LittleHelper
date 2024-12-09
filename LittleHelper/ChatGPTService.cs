using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace LittleHelper
{
    public static class ChatGPTService
    {
        private static readonly string apiKey = "YOUR CHATGPT API KEY GOES HERE";

        public static async Task<string> GetResponse(List<ChatMessage> conversationHistory)
        {
            // Build the messages list for the API call
            var messages = conversationHistory.Select(msg => new ChatMessage
            {
                role = msg.role,
                content = msg.content
            }).ToList();


            string systemPrompt = @"
                You are an assistant integrated into a Revit add-in. Please help the user with useful Revit tips and advice. 
                Your tone is friendly and a little quirky.
                ";

            // Add the system prompt if necessary
            if (!messages.Any(m => m.role == "system"))
            {
                messages.Insert(0, new ChatMessage
                {
                    role = "system",
                    content = systemPrompt
                });
            }


            // Handle token limits
            const int maxTokens = 3500;
            int currentTokenCount = EstimateTokenCount(messages);

            while (currentTokenCount > maxTokens)
            {
                // Remove the second message (after system prompt)
                messages.RemoveAt(1);
                currentTokenCount = EstimateTokenCount(messages);
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = messages
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");


                try
                {
                    var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        // Log the error response for debugging
                        Console.WriteLine($"API Error: {response.StatusCode}");
                        Console.WriteLine($"Error Details: {responseString}");
                        throw new Exception($"API call failed with status code {response.StatusCode}");
                    }

                    var responseObject = JsonConvert.DeserializeObject<ChatGPTResponse>(responseString);
                    string result = responseObject.choices[0].message.content.Trim();

                    return result;
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., network errors, deserialization errors)
                    Console.WriteLine($"Exception occurred: {ex.Message}");
                    throw;
                }
            }
        }

        private static int EstimateTokenCount(List<ChatMessage> messages)
        {
            int tokenCount = 0;
            foreach (var msg in messages)
            {
                // Simple estimation: 1 token per 4 characters
                tokenCount += msg.content.Length / 4;
            }
            return tokenCount;
        }

    }



    // Strongly-typed classes for deserialization
    public class ChatGPTResponse
    {
        public List<Choice> choices { get; set; }
    }

    public class Choice
    {
        public ChatMessage message { get; set; }
    }

    public class ChatMessage
    {
        public string role { get; set; }
        public string content { get; set; }
    }



}