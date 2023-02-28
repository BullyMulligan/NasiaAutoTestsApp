using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace NasiaAutoTestsApp
{
    public class Screen
    {
        public List<Image> screens;
        public Image CreateScreenshot(string name,VendorTests test)
        {
            string parentPath = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "Auth";
            string path = Path.Combine(parentPath,folderName);
            
            //проверяем наличие папки, если ее не существует, то создаем ее
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            test.CreateFolder(name,path);
            byte[] screenshotBytes = Convert.FromBase64String(test.screenshot.AsBase64EncodedString);

            // Создаем MemoryStream на основе массива байтов
            MemoryStream ms = new MemoryStream(screenshotBytes);

            // Создаем объект типа System.Drawing.Image на основе MemoryStream
            Image image = Image.FromStream(ms);
            return image;
        }
    }
}