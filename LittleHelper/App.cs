using Autodesk.Revit.UI;

using System;

using System.IO;

using System.Windows.Media.Imaging;


namespace LittleHelper

{

    public class App : IExternalApplication

    {

        public Result OnStartup(UIControlledApplication application)

        {

            // Create a Ribbon Panel

            RibbonPanel ribbonPanel = application.CreateRibbonPanel("Little Helper");



            // Create a Push Button

            string thisAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            PushButtonData buttonData = new PushButtonData(

                "LittleHelperApp",

                "LittleHelper",

                thisAssemblyPath,

                "LittleHelper.Command");



            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;


            // Add an icon
            //Make dynamic
            string imagePath = @"C:\Users\jrohr\source\repos\LittleHelper\LittleHelper\CuteRobo_32x32.png";
      

            if (File.Exists(imagePath))
            {
                using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    BitmapImage largeImage = new BitmapImage();
                    largeImage.BeginInit();
                    largeImage.StreamSource = stream;
                    largeImage.CacheOption = BitmapCacheOption.OnLoad;
                    largeImage.EndInit();

                    // Assign the image to the button
                    pushButton.LargeImage = largeImage;
                    pushButton.Image = largeImage;
                }
            }
            else
            {
                TaskDialog.Show("Error", $"Image file not found at {imagePath}");
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)

        {

            // Cleanup
            return Result.Succeeded;

        }

    }

}
