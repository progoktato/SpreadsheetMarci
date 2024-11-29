using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerProjekt.Models
{
    internal class Spreadsheet : SpreadsheetBase, ITableManipulator, IStorageHandler
    {
        public Spreadsheet(int numberOfRows, int numberOfColumns) : base(numberOfRows, numberOfColumns) { }
        public Spreadsheet() : base() { }

        public double AverageColumn(int columnIndex)
        {
            return NumbersInColumn(columnIndex).Average();
        }

        public double AverageRow(int rowIndex)
        {
            return NumbersInRow(rowIndex).Average();
        }

        public override void ClearColumn(int column)
        {
            FillColumn(column, "");
        }

        public override void ClearRow(int row)
        {
            FillRow(row, "");
        }

        public bool CompareContent(string file1, string file2)
        {
            if (!CompareStructure(file1, file2))
            {
                throw new InvalidOperationException();
            }

            List<List<string>> lines1 = ReadCSV(file1);
            List<List<string>> lines2 = ReadCSV(file2);

            for (int i = 0; i < lines1.Count; i++)
            {
                for (int j = 0; j < lines1[0].Count; j++)
                {
                    if (lines1[i][j] != lines2[i][j]) 
                        return false;
                }
            }

            return true;
        }

        public bool CompareStructure(string file1, string file2)
        {
            try
            {
                List<List<string>> lines1 = ReadCSV(file1);
                List<List<string>> lines2 = ReadCSV(file2);

                return lines1.Count == lines2.Count && lines1[0].Count == lines2[0].Count;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public void FillColumn(int colIndex, string value)
        {
            if (!IsValidIndexForColumn(colIndex))
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = 0; i < RowCount; i++)
            {
                this[i, colIndex] = value;
            }
        }

        public void FillRow(int rowIndex, string value)
        {
            if (!IsValidIndexForRow(rowIndex))
            {
                throw new IndexOutOfRangeException();
            }

            for (int j = 0; j < ColumnCount; j++)
            {
                this[rowIndex, j] = value;
            }
        }

        public override bool IsValidCell(int rowIndex, int columnIndex)
        {
            return IsValidIndexForRow(rowIndex) && IsValidIndexForColumn(columnIndex);
        }

        public override bool IsValidIndexForColumn(int columnIndex)
        {
            return columnIndex >= 0 && columnIndex < ColumnCount;
        }

        public override bool IsValidIndexForRow(int rowIndex)
        {
            return rowIndex >= 0 && rowIndex < RowCount;
        }

        public void LoadFromCsv(string fileName)
        {
            List<List<string>> lines = ReadCSV(fileName);

            if (lines.Count != RowCount || lines[0].Count != ColumnCount)
            {
                throw new FormatException("Nem egyező táblázat méret");
            }

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    this[i, j] = lines[i][j];
                }
            }
        }

        public void SaveToCsv(string fileName)
        {
            List<string> lines = [];

            for (int i = 0; i < RowCount; i++)
            {
                List<string> columns = [];
                for (int j = 0; j < ColumnCount; j++)
                {
                    columns.Add(this[i, j]);
                }
                lines.Add(String.Join(IStorageHandler.CSV_SEPARATOR, columns));
            }

            File.WriteAllLines(fileName, lines);
        }

        public double SumColumn(int columnIndex)
        {
            return NumbersInColumn(columnIndex).Sum();
        }

        public double SumRow(int rowIndex)
        {
            return NumbersInRow(rowIndex).Sum();
        }

        public void SwapColumns(int column1, int column2)
        {
            if (!IsValidIndexForColumn(column1) || !IsValidIndexForColumn(column2))
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = 0; i < RowCount; i++)
            {
                string temp = this[i, column1];
                this[i, column1] = this[i, column2];
                this[i, column2] = temp;
            }
        }

        public void SwapRows(int row1, int row2)
        {
            if (!IsValidIndexForRow(row1) || !IsValidIndexForRow(row2))
            {
                throw new IndexOutOfRangeException();
            }

            for (int j = 0; j < ColumnCount; j++)
            {
                string temp = this[row1, j];
                this[row1, j] = this[row2, j];
                this[row2, j] = temp;
            }
        }

        private List<double> NumbersInRow(int rowIndex)
        {
            if (!IsValidIndexForRow(rowIndex))
            {
                throw new IndexOutOfRangeException();
            }

            List<double> numbers = [];

            for (int j = 0; j < ColumnCount; j++)
            {
                if (GetType(rowIndex, j) == CellType.Number)
                {
                    numbers.Add(double.Parse(this[rowIndex, j]));
                }
            }

            return numbers;
        }

        private List<double> NumbersInColumn(int columnIndex)
        {
            if (!IsValidIndexForColumn(columnIndex))
            {
                throw new IndexOutOfRangeException();
            }

            List<double> numbers = [];

            for (int i = 0; i < RowCount; i++)
            {
                if (GetType(i, columnIndex) == CellType.Number)
                {
                    numbers.Add(double.Parse(this[i, columnIndex]));
                }
            }

            return numbers;
        }

        private static List<List<string>> ReadCSV(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }

            List<List<string>> lines = File.ReadAllLines(fileName).Select(x => x.Split(IStorageHandler.CSV_SEPARATOR).ToList()).ToList();

            if (lines.Select(x => x.Count).Distinct().Count() != 1)
            {
                throw new FormatException("Nem egységes oszlopszámok.");
            }

            return lines;
        }
    }
}
