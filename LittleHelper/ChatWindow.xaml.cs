using Autodesk.Revit.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;



namespace LittleHelper

{

    public partial class ChatWindow : Window

    {

        private UIApplication _uiapp;

        private UIDocument _uidoc;

        private Autodesk.Revit.DB.Document _doc;

        private List<ChatMessage> _conversationHistory = new List<ChatMessage>();


        public ChatWindow(UIApplication uiapp)

        {

            InitializeComponent();

            _uiapp = uiapp;

            _uidoc = uiapp.ActiveUIDocument;

            _doc = _uidoc.Document;

        }



        private async void SendButton_Click(object sender, RoutedEventArgs e)

        {

            string userInput = UserInput.Text;

            // Clear the input box
            UserInput.Text = string.Empty;


            if (!string.IsNullOrEmpty(userInput))

            {

                // Display user's message
                ChatHistory.AppendText("User: " + userInput + "\n");
                _conversationHistory.Add(new ChatMessage { role = "user", content = userInput });

                try
                {

                    // Call ChatGPT API
                    string response = await ChatGPTService.GetResponse(_conversationHistory);

                    // Add assistant's response to the conversation history
                    _conversationHistory.Add(new ChatMessage { role = "assistant", content = response });

                    // Display ChatGPT's response
                    ChatHistory.AppendText("Assistant: " + response + "\n");

                }
                catch (Exception ex)
                {
                    // Display error message
                    ChatHistory.AppendText("Assistant: Sorry, an error occurred: " + ex.Message + "\n");
                    Console.WriteLine("Exception: " + ex.ToString());
                }
            }


        }

    }

}