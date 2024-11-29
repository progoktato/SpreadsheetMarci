using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerProjekt.Models
{
    public abstract class SpreadsheetBase
    {
        public enum CellType
        {
            String,
            Number,
            Date,
            Bool
        }

        const int MAX_ROW_NUMBER = 100;
        const int MAX_COLUMN_NUMBER = 20;
        //todo - Készítsen MIN_ROW_NUMBER és MIN_COLUMN_NUMBER néven konstansokat.
        //A legkevesebb sor 6, a legkevesebb oszlop pedig 10 lehet!
        const int MIN_ROW_NUMBER = 6;
        const int MIN_COLUMN_NUMBER = 10;

        private string[,] cells;

        /// <summary>
        /// Sorok száma
        /// </summary>
        public int RowCount => cells.GetLength(0);

        /// <summary>
        /// Oszlopok száma
        /// </summary>
        public int ColumnCount => cells.GetLength(1);

        /// <summary>
        /// Táblázatot létrehozó konstruktor. A cellák üres stringet tartalmaznak a létrehozáskor.
        /// </summary>
        /// <param name="numberOfRows">Sorok száma</param>
        /// <param name="numberOfColumns">Oszlopok száma</param>
        /// <exception cref="ArgumentOutOfRangeException">Megengedet tartományon kivüli méret!</exception>///
        public SpreadsheetBase(int numberOfRows, int numberOfColumns)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(numberOfRows, MAX_ROW_NUMBER, "numberOfRows");
            //todo A méretekre végezzen további hibavizsgálatot!
            ArgumentOutOfRangeException.ThrowIfLessThan(numberOfRows, MIN_ROW_NUMBER, "numberOfRows");
            ArgumentOutOfRangeException.ThrowIfGreaterThan(numberOfColumns, MAX_COLUMN_NUMBER, "numberOfColumns");
            ArgumentOutOfRangeException.ThrowIfLessThan(numberOfColumns, MIN_COLUMN_NUMBER, "numberOfColumns");
            cells = new string[numberOfRows, numberOfColumns];
            ResetTable();
        }

        public SpreadsheetBase() : this(MIN_ROW_NUMBER, MIN_COLUMN_NUMBER)
        {

        }
        //todo 1) - [SpreadsheetBase] Készítsen paraméter nélküli konstruktort, amit a meglévő konstruktorra vezet vissza!
        // Ebben az esetben a minimum számú oszlop és minimum számú sor legyen a létrejövő táblázatban!

        /// <summary>
        /// A táblázat cellájához biztosít írási és olvasási hozzáférést
        /// </summary>
        /// <param name="rowIndex">A cella sorának indexe</param>
        /// <param name="columnIndex">A cella oszlopának indexe</param>
        /// <returns>A cella tartalma</returns>
        /// <exception cref="IndexOutOfRangeException ">
        /// </exception>
        public string this[int rowIndex, int columnIndex]
        {
            get
            {
                //todo Hibavizsgálat az indexekre!
                // Hiba esetén IndexOutOfRangeException kiváltása!
                if (!IsValidCell(rowIndex, columnIndex))
                {
                    throw new IndexOutOfRangeException();
                }

                return cells[rowIndex, columnIndex];
            }
            set
            {
                //todo Hibavizsgálat az indexekre!
                // Hiba esetén IndexOutOfRangeException kiváltása!
                if (!IsValidCell(rowIndex, columnIndex))
                {
                    throw new IndexOutOfRangeException();
                }

                cells[rowIndex, columnIndex] = value;
            }
        }



        /// <summary>
        /// Megvizsgálja, hogy valós-e a sorindex
        /// </summary>
        /// <param name="rowIndex">Vizsgálandó sor indexe</param>
        /// <returns>true - létezik ilyen sor</returns>
        public abstract bool IsValidIndexForRow(int rowIndex);

        public abstract bool IsValidIndexForColumn(int columnIndex);

        /// <summary>
        /// Megvizsgálja, hogy létezik-e a megadott cella a táblázatban.
        /// </summary>
        /// <param name="rowIndex">Cella sorának száma (0-tól induló számozás)</param>
        /// <param name="columnIndex">Cella oszlopának száma (0-tól induló számozás)</param>
        /// <returns>true - érvényes a hivatkozás, false - nem létezik ilyen cella</returns>
        public abstract bool IsValidCell(int rowIndex, int columnIndex);
        //TODO Tipp - Vezesse át az előző két függvényre!


        /// <summary>
        /// A táblázatot alapértelmezett állapotba hozza. Minden cellát üressé tesz!
        /// </summary>
        public void ResetTable()
        {
            for (int rowIndex = 0; rowIndex < cells.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < cells.GetLength(1); colIndex++)
                {
                    cells[rowIndex, colIndex] = "";
                }
            }
        }

        /// <summary>
        /// Kitörli a táblázat megadott sorát. A alatta lévő sorok eggyel fentebb kerülnek.
        /// </summary>
        /// <param name="row">A törlendő sor indexe</param>
        /// <exception cref="IndexOutOfRangeException ">Ha az index kivül esik a megengedett tartományon</exception>
        public void DeleteRow(int row)
        {
            // TODO TIPP!
            // Az implementáció során vegye figyelembe, hogy a kétdimenziós tömb nem átméretezhető!
            // Egy új, kisebb méretű tömböt kell létrehozni és abba átmásolni a régi megmaradó elemeket.
            // A másolás után az új tömbbel foogunk tovább dolgozni.

            if (!IsValidIndexForRow(row))
            {
                throw new IndexOutOfRangeException();
            }

            string[,] newCells = new string[RowCount - 1, ColumnCount];
            for (int i = 0; i < RowCount; i++)
            {
                if (i == row)
                {
                    continue;
                }
                int copyTo = i > row ? i - 1 : i;

                for (int j = 0; j < ColumnCount; j++)
                {
                    newCells[copyTo, j] = cells[i, j];
                }
            }

            cells = newCells;
        }



        /// <summary>
        /// Kitörli a táblázat megadott oszlopát. Az utána következő oszlopok eggyel balra kerülnek. A szerkezet változik!
        /// </summary>
        /// <param name="column">A törlendő oszlop indexe</param>
        /// <exception cref="IndexOutOfRangeException ">Ha az index kivül esik a megengedett tartományon</exception>
        public void DeleteColumn(int column)
        {
            if (!IsValidIndexForColumn(column))
            {
                throw new IndexOutOfRangeException();
            }

            string[,] newCells = new string[RowCount, ColumnCount - 1];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (j == column)
                    {
                        continue;
                    }
                    int copyTo = j > column ? j - 1 : j;

                    newCells[i, copyTo] = cells[i, j];
                }
            }

            cells = newCells;
        }


        /// <summary>
        /// Kiüríti a táblázat megadott sorát (törli annak tartalmát). A szerkezet NEM változik!
        /// </summary>
        /// <param name="row">A sor indexe</param>
        /// <exception cref="IndexOutOfRangeException ">Ha az index kivül esik a megengedett tartományon</exception>
        public abstract void ClearRow(int row);


        /// <summary>
        /// Kiüríti a táblázat megadott oszlopát (törli annak tartalmát). A szerkezet NEM változik!
        /// </summary>
        /// <param name="column">Az oszlop indexe</param>
        /// <exception cref="IndexOutOfRangeException ">Ha az index kivül esik a megengedett tartományon</exception>
        public abstract void ClearColumn(int column);

        /// <summary>
        /// Megadja a meghatározott cella típusát
        /// </summary>
        /// <param name="rowIndex">A cella sorának indexe</param>
        /// <param name="columnIndex">A cella oszlopának indexe</param>
        /// <returns>A cella típusa, ami lehet CellType.String, CellType.Number, CellType.Date, CellType.Bool</returns>
        /// <exception cref="IndexOutOfRangeException ">Ha az indexek kivül esnek az aktuális tábla méretén</exception>
        public CellType GetType(int rowIndex, int columnIndex)
        {
            //TODO Itt készítse el az implementációt!
            if (!IsValidCell(rowIndex, columnIndex))
            {
                throw new IndexOutOfRangeException();
            }

            if (double.TryParse(cells[rowIndex, columnIndex], out _))
            {
                return CellType.Number;
            }

            if (DateTime.TryParse(cells[rowIndex, columnIndex], out _))
            {
                return CellType.Date;
            }

            if (bool.TryParse(cells[rowIndex, columnIndex], out _))
            {
                return CellType.Bool;
            }

            return CellType.String;
        }

        /// <summary>
        /// A táblázat megjelenítésére alkamas Stringet állítja elő, ahol a cella szélessége 10 karakter.
        /// </summary>
        /// <returns>Táblázat String formában</returns>
        public override string? ToString()
        {
            return ToString(cellsLength: 10);
        }

        /// <summary>
        /// A megadott cellaszélességgel hozza létre a táblázatot.
        /// </summary>
        /// <param name="cellsLength">Cella szélessége karakterben</param>
        /// <returns>Táblázat String formában</returns>
        public string? ToString(int cellsLength)
        {
            StringBuilder sb = new StringBuilder();
            //todo Ide jön a tanári kód rövidesen!
            String cellFrame = new string('─', cellsLength);
            StringBuilder rowFrame = new StringBuilder("┌" + cellFrame);
            for (int i = 1; i < ColumnCount; i++)
            {
                rowFrame.Append('┬' + cellFrame);
            }
            rowFrame.Append("┐" + "\n");
            sb.Append(rowFrame);

            StringBuilder rowData = new();

            for (int rowIndex = 0; rowIndex < RowCount; rowIndex++)
            {
                rowData = new StringBuilder();
                for (int colIndex = 0; colIndex < ColumnCount; colIndex++)
                {
                    rowData.Append('│' + (cells[rowIndex, colIndex].Length > cellsLength
                        ? cells[rowIndex, colIndex].Substring(0, cellsLength - 1) + '■'
                        : cells[rowIndex, colIndex].PadLeft(cellsLength)));
                }
                rowData.Append("│");
                sb.Append(rowData + "\n");


                if (rowIndex < RowCount - 1)
                {
                    rowFrame = new StringBuilder("│" + cellFrame);
                    for (int i = 1; i < ColumnCount; i++)
                    {
                        rowFrame.Append('┼' + cellFrame);
                    }
                    rowFrame.Append("┤");
                    sb.Append(rowFrame + "\n");
                }
            }

            rowFrame = new StringBuilder("└" + cellFrame);
            for (int i = 1; i < ColumnCount; i++)
            {
                rowFrame.Append('┴' + cellFrame);
            }
            rowFrame.Append("┘" + "\n");
            sb.Append(rowFrame);

            return sb.ToString();
        }

    }
}


