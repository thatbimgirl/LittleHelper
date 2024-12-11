
______________________________________________________________________________________________________________________________________________________________
**Revit ChatGPT Assistant**

This add-in integrates ChatGPT into Revit, allowing you to interact with the model via natural language. The assistant will respond with helpful Revit advice.
______________________________________________________________________________________________________________________________________________________________

**Prerequisites**

Revit: Ensure that Revit is installed on your machine.
Visual Studio: Required for building the solution.
.NET Development Tools: Make sure you have the necessary .NET frameworks and SDKs installed as required by your solution.
ChatGPT API Key:
You will need a ChatGPT API key from OpenAI. If you don’t have one yet, sign up at https://platform.openai.com/ and create a secret key.
Store this key securely and do not commit it to your repository.

**Getting Started**

1) Clone or Download the Repository:
git clone https://github.com/YourUsername/YourRepoName.git
If you downloaded a ZIP, extract it to a preferred location.

2) Open the Solution in Visual Studio:
Launch Visual Studio.
Go to File > Open > Project/Solution and select the .sln file in the project's root directory.

3) Set Your ChatGPT API Key:
Under ChatGPTService.cs you will need to enter your API key.
Find this line:
  private static readonly string apiKey = "YOUR CHATGPT API KEY GOES HERE";
Make sure you do not commit your API key into source control.

4) Restore NuGet Packages:
Visual Studio typically does this automatically, but if not, go to Project > Manage NuGet Packages and restore them.

5) Build the Solution:
Set the desired configuration (most likely Debug x64).
Build the solution by clicking Build > Build Solution or pressing Ctrl+Shift+B.
If the build is successful, you’ll find the compiled LittleHelper.dll files in the bin/ directory inside your project's folder.

**Installing the Add-In to Revit** 

1) Locate the Revit Add-Ins Folder:
Typically, add-ins are placed in:
%ProgramData%\Autodesk\Revit\Addins\<RevitVersion>\
For example, if you’re using Revit 2024:
C:\ProgramData\Autodesk\Revit\Addins\2024\

From your project’s bin/Release folder, copy the .dll and .addin files.
Paste them into the Revit Add-Ins folder mentioned above.
Ensure that the .addin file references the correct path to your .dll.

2) Load the Add-In in Revit:
Launch Revit.
If all is set up correctly, you should see a new tab or commands related to the ChatGPT assistant in Revit’s interface.

**Usage**

Open Revit and find the LittleHelper button in the AddIns Ribbon tab.
Interact with the assistant by typing queries and pressing send

**Troubleshooting**

No Response or Context Memory: Ensure that your ChatGPT API key is valid and properly configured.
Add-In Not Showing in Revit: Double-check that .addin and .dll files are in the correct Revit add-in folder.
Build Errors: Make sure all NuGet packages are restored and you have the correct .NET SDK.

