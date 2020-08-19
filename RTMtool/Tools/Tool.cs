using HtmlAgilityPack;
using RTMtool.Class;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMtool.Tools
{
    class Tool
    {
        public Boolean validate(HTMLFile htmlFile, XLFile xlfile)
        {
            try
            {
                CellRange[] cells = xlfile.CellsFirstCol;
                Boolean check = true;
                for (int i = 1; i < cells.Length; i++)
                {
                    if (!htmlFile.checkID(cells[i].Value.ToString()))
                    {
                        System.Console.Write("ID '" + cells[i].Value.ToString() + "' is missing\n");
                        check = false;
                    }
                    htmlFile.NodeList.Add(htmlFile.Htmldoc.GetElementbyId(cells[i].Value.ToString()));
                }
                return check;
            }
            catch(Exception exc)
            {
                System.Console.Write(exc.StackTrace);
            }
            return false;
        }
        public void generateHTML(HTMLFile htmlfile, XLFile xlfile, String dirPath)
        {
            CellRange[] cells = xlfile.CellsFirstCol;
            CellRange[] cellsValues = xlfile.CellsValCol;
            HtmlNode node = htmlfile.Htmldoc.GetElementbyId(cells[1].Value.ToString());
            string value = null;
            for (int i = 0, j = 1; j < cellsValues.Length; i++, j++)
            {
                if (cellsValues[j].Value.ToString() != "")
                {
                    System.Console.Write("\nprocessing row" + j + "...\n");
                    /*if (j == 3 || j == 4 || j == 23)
                    {*/
                        htmlfile.updateNode(i, cells[j].Value.ToString(), cellsValues[j].Value.ToString(), "%");
                    /*}
                    else
                    {
                        value = null;
                        value = cellsValues[j].Value.ToString();
                        htmlfile.NodeList.ElementAt(i).InnerHtml = value;
                    }*/
                }
            }
            String[] splitPath = xlfile.Path.Split('\\');
            htmlfile.Htmldoc.Save(@dirPath+splitPath[splitPath.Length-1].Split('.')[0]+".html");
            //System.Console.Write("\nC:/Users/trakotom/Desktop/local/RTM/Generated-file/" + splitPath[splitPath.Length - 1].Split('.')[0] + ".html\n");
        }

    }
}
