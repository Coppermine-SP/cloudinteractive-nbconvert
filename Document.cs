using System;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace cloudinteractive.nbconvert
{
    public static class Document
    {

        public static void HtmltoPdf(string fileLocation, string saveLocation, string fileName)
        {
            var driverOptions = new ChromeOptions();
            driverOptions.AddArgument("headless");
            driverOptions.AddArgument("no-sandbox");

            Console.WriteLine($"Converting {fileLocation} to PDF document...");
            using (var driver = new ChromeDriver(driverOptions))
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                driver.Navigate().GoToUrl($"file://{fileLocation}");
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                Thread.Sleep(3000);

                var printOptions = new PrintOptions();
                printOptions.OutputBackgroundImages = true;
                var output = driver.Print(printOptions);
                output.SaveAsFile($"{saveLocation}/{fileName}.pdf");
            }
        }

        public static void NotebooktoHtml(string location, string fileName)
        {
            Helper.Run($"jupyter nbconvert {location} --to html --output {fileName}");
        }

        public static void MergePDF(string directory, string source_tag, string output)
        {
            Console.WriteLine($"Merging all *{source_tag}.pdf files..");
            string[] files = Directory.GetFiles(directory, $"*{source_tag}.pdf");

            PdfDocument outputDocument = new PdfDocument();
            Console.WriteLine();
            files = files.OrderBy(x => x.Split('/')[x.Count(y => y == '/')][0]).ToArray();
            foreach (string file in files)
            {
                Console.WriteLine(file);
                var document = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                for(int i = 0; i < document.PageCount; i++)
                {
                    outputDocument.AddPage(document.Pages[i]);
                }

                //Windows 1252 encoding support.
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var enc1252 = Encoding.GetEncoding(1252);

                outputDocument.Save($"{directory}/{output}.pdf");
            }
            Console.WriteLine($"Export Complete: {output}.pdf");
        }
    }

    public static class Helper
    {
        public static void Run(string command)
        {
            Console.WriteLine(command);
            var startInfo = new ProcessStartInfo(){ FileName="/bin/bash", Arguments = $"-c \"{command}\"", RedirectStandardOutput = true};
            using (var process = Process.Start(startInfo))
            {
                process.OutputDataReceived += (sender, args) => {Console.WriteLine(args);} ;
                process.WaitForExit();
            }
        }
    }
}
