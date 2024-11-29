using BannerProjekt.Models;

namespace BannerProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Spreadsheet table = new Spreadsheet(6,10);
            table[1, 2] = "Alma van a fa alatt!";
            table[5, 4] = "1245.34";
            table[5, 9] = "A!";
            table.FillRow(2,"Péntek");
            Console.WriteLine(table.ToString(10));

            // Delete
            //table.DeleteRow(1);
            //Console.WriteLine(table.ToString(10));
            //table.DeleteColumn(3);
            //Console.WriteLine(table.ToString(10));

            // GetType
            //table[0, 0] = new DateTime().ToString();
            //Console.WriteLine(table.GetType(0, 0));
            //table[0, 0] = true.ToString();
            //Console.WriteLine(table.GetType(0, 0));
            //table[0, 0] = (123123.123).ToString();
            //Console.WriteLine(table.GetType(0, 0));
            //table[0, 0] = "asd";
            //Console.WriteLine(table.GetType(0, 0));

            // Row műveletek
            //table.FillRow(0, "10");
            //table.FillColumn(1, "asd");
            //table.FillColumn(3, "20");
            //table.FillColumn(4, "20");
            //Console.WriteLine(table.ToString(10));
            //Console.WriteLine(table.SumRow(0));
            //Console.WriteLine(table.AverageRow(0));

            // Column műveletek
            //table.FillColumn(0, "10");
            //table.FillRow(1, "asd");
            //table.FillRow(3, "20");
            //table.FillRow(4, "20");
            //Console.WriteLine(table.ToString(10));
            //Console.WriteLine(table.SumColumn(0));
            //Console.WriteLine(table.AverageColumn(0));

            // ClearRow
            //table.FillRow(2, "asd");
            //table.ClearRow(2);
            //Console.WriteLine(table.ToString(10));

            // ClearColumn
            //table.FillColumn(3, "asd");
            //Console.WriteLine(table.ToString(10));
            //table.ClearColumn(3);
            //Console.WriteLine(table.ToString(10));

            // SwapRows
            //table.FillRow(2, "test1");
            //table.FillRow(5, "test2");
            //Console.WriteLine(table.ToString(10));
            //table.SwapRows(5, 2);
            //Console.WriteLine(table.ToString(10));

            // SwapColumns
            //table.FillColumn(3, "test1");
            //table.FillColumn(5, "test2");
            //Console.WriteLine(table.ToString(10));
            //table.SwapColumns(3, 5);
            //Console.WriteLine(table.ToString(10));

            //File operations
            //table.SaveToCsv("test.csv");
            //Console.WriteLine(table.ToString(10));
            //table.ResetTable();
            //Console.WriteLine(table.ToString(10));
            //table.LoadFromCsv("test.csv");
            //Console.WriteLine(table.ToString(10));

            // Compare
            //table.SaveToCsv("test1.csv");
            //table.SaveToCsv("test2.csv");
            //Console.WriteLine(table.CompareStructure("test1.csv", "test2.csv"));
            //Console.WriteLine(table.CompareContent("test1.csv", "test2.csv"));
        }
    }
}
