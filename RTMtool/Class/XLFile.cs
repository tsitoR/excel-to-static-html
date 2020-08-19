using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Spire.Xls;

namespace RTMtool.Class
{
    class XLFile
    {
        private String path;
        private CellRange[] cellsFirstCol;
        private CellRange[] cellsValCol;

        public CellRange[] CellsValCol
        {
            get { return cellsValCol; }
            set { cellsValCol = value; }
        }

        public CellRange[] CellsFirstCol
        {
            get { return cellsFirstCol; }
            set { cellsFirstCol = value; }
        }

        public String Path
        {
            get { return path; }
            set { path = value; }
        }
        public XLFile(String path)
        {
            this.path = path;
        }
        public void loadFile()
        {
            try
            {
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(path);
                Worksheet worksheet = workbook.Worksheets[0];
                System.Console.Write("number of cell on worksheet 1: " + worksheet.Columns[0].CellsCount + "\n");
                this.cellsFirstCol = worksheet.Columns[0].Cells;
                this.cellsValCol = worksheet.Columns[1].Cells;
            }
            catch(System.IO.IOException exception)
            {
                System.Console.Write("Error: the file " + path + " is open\n");
            }
            catch(Exception exc)
            {
                System.Console.Write(exc.StackTrace);
            }
        }
    }
}
