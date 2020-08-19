using RTMtool.Class;
using RTMtool.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMtool
{
    class Program
    {
        static void Main(string[] args)
        {
            Tool tool = new Tool();
            System.Console.Write("Enter excel files directory path (C:\\xxx\\xxx\\):\n");
            String dirPath = System.Console.ReadLine();
            XLFile file = new XLFile(@dirPath);
            file.loadFile();
            System.Console.Write("\nEnter HTML base template path (C:\\xxx\\xxx\\file.html):\n");
            String htmlPath = System.Console.ReadLine();
            HTMLFile htmlFile = new HTMLFile(@htmlPath);
            if(tool.validate(htmlFile, file))
                System.Console.WriteLine("\nFiles validated\n");
            else
            {
                System.Console.Write("\n ID error do you want to continue?(y/n)\n");
                System.Console.ReadLine();
            }
            System.Console.Write("\n Enter destination path (C:\\xxx\\xxx\\): \n");
            String destPath = System.Console.ReadLine();
            tool.generateHTML(htmlFile, file, destPath);
            System.Console.WriteLine("File Generated\n");

            //htmlFile.findPlaceHolder();

            System.Console.ReadLine();

        }
    }
}
