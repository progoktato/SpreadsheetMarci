using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerProjekt.Models
{
    internal interface ITableManipulator
    {
        /// <summary>
        /// Egy sort kitölt a megadott értékkel
        /// </summary>
        /// <param name="row">A sor indexe</param>
        /// <param name="value">Az érték, ami a sor minden cellájába bekerül</param>
        /// <exception cref="IndexOutOfRangeException ">Ha az indexek kivül esnek az aktuális tábla méretén</exception>
        public void FillRow(int rowIndex, string value);

        /// <summary>
        /// Egy oszlopot kitölt a megadott értékkel
        /// </summary>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        /// <exception cref="IndexOutOfRangeException ">Ha az indexek kivül esnek az aktuális tábla méretén</exception>
        public void FillColumn(int colIndex, string value);


        /// <summary>
        /// Összeadja egy megadott sorban a cellák tartalmát. Amennyiben egy cella nem számot tartalmaz, akkor azt nem veszi figyelembe!
        /// </summary>
        /// <param name="rowIndex">A megadott indexű sor celláit adja össze.</param>
        /// <returns>A cellákban lévő számok összege</returns>
        /// <exception cref="IndexOutOfRangeException ">Ha az index kivül esik a megengedett tartományon</exception>
        public double SumRow(int rowIndex);

        // TODO TIPP!
        // double value = 0;
        // Double.TryParse(this[indexOfRow, indexOfColumn], out value);



        /// <summary>
        /// Összeadja egy megadott oszlopan a cellák tartalmát. Amennyiben egy cella nem számot tartalmaz, akkor azt nem veszi figyelembe!
        /// </summary>
        /// <param name="columnIndex">A megadott indexű oszlop celláit adja össze.</param>
        /// <returns>A cellákban lévő számok összege</returns>
        /// <exception cref="IndexOutOfRangeException ">Ha az index kivül esik a megengedett tartományon</exception>
        public double SumColumn(int columnIndex);


        /// <summary>
        /// A megadott sorban lévő cellák tartalmát átlagolja. Amennyiben egy cella nem számot tartalmaz, akkor azt nem veszi figyelembe!
        /// </summary>
        /// <param name="rowIndex">A megadott indexű sor celláit átlagolja</param>
        /// <returns>A cellákban lévő számok átlaga</returns>
        /// <exception cref="IndexOutOfRangeException ">Ha az index kivül esik a megengedett tartományon</exception>/// 
        public double AverageRow(int rowIndex);


        /// <summary>
        /// A megadott oszlopban lévő cellák tartalmát átlagolja. Amennyiben egy cella nem számot tartalmaz, akkor azt nem veszi figyelembe!
        /// </summary>
        /// <param name="rowIndex">A megadott indexű oszlop celláit átlagolja</param>
        /// <returns>A cellákban lévő számok átlaga</returns>
        /// <exception cref="IndexOutOfRangeException ">Ha az index kivül esik a megengedett tartományon</exception>
        public double AverageColumn(int columnIndex);

        /// <summary>
        /// Két sor tartalmát megcseréli
        /// </summary>
        /// <param name="row1">Első sor indexe</param>
        /// <param name="row2">Második sor indexe</param>
        /// <exception cref="IndexOutOfRangeException ">Ha az indexek kivül esnek az aktuális tábla méretén</exception>
        void SwapRows(int row1, int row2);

        /// <summary>
        /// Két oszlop tartalmát megcserélni
        /// </summary>
        /// <param name="column1">Első sor indexe</param>
        /// <param name="column2">Második sor indexe</param>
        /// <exception cref="IndexOutOfRangeException ">Ha az indexek kivül esnek az aktuális tábla méretén</exception>
        void SwapColumns(int column1, int column2);


    }
}
