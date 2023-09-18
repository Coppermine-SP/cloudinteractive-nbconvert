
//cloudinteractive.nbconvert++
//Copyright(C) 2023 CloudInteractive.

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;

namespace cloudinteractive.nbconvert
{
    internal class Program
    {
        private static string? _fileLocation = null;
        private static string? _fileName = null;
        private static string? _workingPath = null;
        private static string? _coverTemplateLocation = null;
        private static string? _title = null;
        private static string _guid = Guid.NewGuid().ToString().Substring(0, 8);

        static void Main(string[] args)
        {
            Console.WriteLine($"nbconvert++ {Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version}\nCopyright (C) 2023 CloudInteractive Inc.\n");
            if (args.Length == 0 || (args[0] ?? String.Empty) == "--help")
            {
                Console.WriteLine("Usage: nbconvert++ <File location> <Options>\n" +
                                  "Example: nbconvert++ file.ipynb --cover-template=\"template.html\"\n\n" +
                                  "Options:\n" +
                                  "--cover-template=\"<Template location>\"\n" +
                                  "     Location of the cover(first page) template file to use.\n" +
                                  "     If is not specified, there will be out without cover.\n\n" +
                                  "--title=\"<Title>\"\n" +
                                  "    Title of the document.\n" +
                                  "    It will be included on the cover.");
                return;
            }

            if (args[0].Substring(args[0].Length - 6) != ".ipynb")
            {
                Console.WriteLine($"[ERROR] {args[0]} is not valid .ipynb file.");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"[ERROR] {args[0]} does not exists.");
                return;
            }
            _fileLocation = args[0];
            var fi = new FileInfo(_fileLocation);
            _workingPath = fi.DirectoryName;
            _fileName = fi.Name;

            //load options.
            for (int i = 1; i < args.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(args[i]) || args[i].Substring(0, 2) != "--") continue;

                string[] arg = args[i].Split('=');
                if (arg.Length != 2 || args.All(x => String.IsNullOrWhiteSpace(x)))
                {
                    Console.WriteLine($"[WARNING] option '{args[i]}' is invalid. ignoring.");
                    Console.WriteLine("$To show help string, run with --help argument.");
                    continue;
                }

                string key = arg[0].Substring(2);
                string value = arg[1];

                if (key == "cover-template" && _coverTemplateLocation == null)
                {
                    if (value.Substring(value.Length - 5) != ".html")
                    {
                        Console.WriteLine($"[WARNING] (cover-template): {args[0]} is not valid .html file. ignoring.");
                        continue;
                    }

                    if (!File.Exists(value))
                    {
                        Console.WriteLine($"[WARNING] (cover-template): {value} does not exists. ignoring.");
                        continue;
                    }

                    Console.WriteLine($"cover-template = {value}");
                    _coverTemplateLocation = value;
                }
                else if (key == "title" && _title == null)
                {
                    Console.WriteLine($"title = {value}");
                    _title = value;
                }
                else
                {
                    Console.WriteLine($"[WARNING] option '{args[i]}' is invalid, ignoring.");
                    Console.WriteLine("To show help string, run with --help argument.");
                }
            }

            try
            {
                Console.WriteLine($"Converting ipynb file to HTML...");
                Document.NotebooktoHtml(_fileLocation, _guid);
                Document.HtmltoPdf($"{_workingPath}/{_guid}.html", _workingPath,
                    _coverTemplateLocation == null ? _fileName : '1' + _guid);

                if (_coverTemplateLocation != null)
                {
                    Console.WriteLine($"Converting cover template to PDF..");
                    Document.HtmltoPdf($"{_coverTemplateLocation}?title={_title ?? _fileName}", _workingPath,
                        '0' + _guid);
                    Document.MergePDF(_workingPath, _guid, $"{_fileName}_out");
                }
                else
                {
                    Console.WriteLine($"Converting complete.");
                }

                Console.WriteLine("Deleting all temp files..");

                DirectoryInfo di = new DirectoryInfo(_workingPath);
                FileInfo[] files = di.GetFiles($"*{_guid}*");
                foreach (FileInfo file in files)
                {
                    file.Delete();
                }
                Console.WriteLine("Complete!");

            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR]: Exception Occurred.");
                Console.WriteLine(e.ToString());
                return;
            }
        }
    }
}