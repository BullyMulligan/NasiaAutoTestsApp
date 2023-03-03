using System;
using System.Diagnostics;
using System.IO;

namespace NasiaAutoTestsApp
{
    public class Mjpeg
    {
        public void JpgToMpeg(string path)
        {
            //назначаем адрес картинки, сохраняемого видеопотока
            string imagePath = path;
            string outputVideoPath = "C:/Users/ipopov/RiderProjects/NasiaAutoTestsApp/NasiaAutoTestsApp/bin/Debug/stream.mjpeg";
            
            //назначаем фреймрейт, ширину и высоту видеопотока
            int frameRate = 30;
            int width = 1920;
            int height = 1080;

            //задаем аргументы для видеопотока
            string arguments = $"-loop 1 -i {imagePath} -r {frameRate} -s {width}x{height} -t 5 -c:v mjpeg {outputVideoPath}";
            
            if (File.Exists(outputVideoPath))
            {
                // Если файл существует, то удаляем его, иначе он не сможет создать его
                File.Delete(outputVideoPath);
            }

            //создаем инфо процесса, задаем его параметры
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            //создаем процесс и запускаем его
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;

                process.ErrorDataReceived += (sender, e) =>
                {
                    Console.WriteLine(e.Data);
                };

                process.Start();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Close();
            }
            
        }
    }
}