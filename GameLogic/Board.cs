using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Board
    {
        private int r_RowSize;
        private int r_ColumnSize;
        private char[,] r_BoardMatrix;
        private List<int> m_AvailableColumns = new List<int>();

        public int RowSize
        {
            get { return r_RowSize; }
            set { r_RowSize = value; }
        }

        public int ColumnSize
        {
            get { return r_ColumnSize; }
            set { r_ColumnSize = value; }
        }

        public char[,] Matrix
        {
            get { return r_BoardMatrix; }
        }

        public List<int> AvailableColumns
        {
            get { return m_AvailableColumns; }

            set { m_AvailableColumns = value; }
        }

        public Board(int rowSize, int columnSize)
        {
            r_ColumnSize = columnSize;
            r_RowSize = rowSize;
            r_BoardMatrix = new char[rowSize, columnSize];
            m_AvailableColumns = Enumerable.Range(0, ColumnSize).ToList();
            ClearBoard();
        }

        public int GetIndexOfRowToInsertAccordingToIndexOfColumn(int i_ColumnNumber)
        {
            int indexOfNextFreeRow = RowSize - 1;

            for (int i = RowSize -1; i >= 0; i--)
            {
                if (Matrix[i, i_ColumnNumber] == ' ')
                {
                    indexOfNextFreeRow = i;
                    break;
                }

            }

            return indexOfNextFreeRow;
        }

        public char[] GetColumn(int i_IndexColumn)
        {
            char[] arrayColumn = new char[RowSize];

            for (int i = 0; i < RowSize; i++)
            {
                arrayColumn[i] = Matrix[i, i_IndexColumn];
            }

            return arrayColumn;
        }

        public char[] GetRow(int i_IndexRow)
        {
            char[] arrayRow = new char[ColumnSize];

            for (int i = 0; i < ColumnSize; i++)
            {
                arrayRow[i] = Matrix[i_IndexRow, i];
            }

            return arrayRow;
        }

        public int GetMainDiagonalSize(int i_Row, int i_Column, ref int io_RowToStart, ref int io_ColumnToStart) // '\'
        {
            int count = 0;
            io_RowToStart = i_Row;
            io_ColumnToStart = i_Column;

            while (io_RowToStart < RowSize - 1 && io_ColumnToStart < ColumnSize - 1) // going down right
            {
                count++;
                io_RowToStart++;
                io_ColumnToStart++;
            }

            io_RowToStart = i_Row;
            io_ColumnToStart = i_Column;

            while (io_RowToStart > 0 && io_ColumnToStart > 0) //going up left 
            {
                count++;
                io_RowToStart--;
                io_ColumnToStart--;

            }

            return count + 1;
        }

        public char[] GetMainDiagonal(int i_Row, int i_Column) // '\' main
        {
            int columnToStart = i_Column;
            int currentRow = i_Row;
            int mainDiagonalSize = GetMainDiagonalSize(i_Row, i_Column, ref currentRow, ref columnToStart);
            char[] mainDiagonal = new char[mainDiagonalSize];
            int indexToInsert = mainDiagonalSize - 1;

            for (int i = columnToStart; i < ColumnSize && currentRow < RowSize; i++)
            {
                mainDiagonal[indexToInsert] = Matrix[currentRow, i];
                currentRow++;
                indexToInsert--;
            }

            return mainDiagonal;
        }

        public int GetAntiDiagonalSize(int i_Row, int i_Column, ref int io_RowToStart, ref int io_ColumnToStart) // '/'
        {
            int count = 0;
            io_RowToStart = i_Row;
            io_ColumnToStart = i_Column;

            while (io_RowToStart < RowSize - 1 && io_ColumnToStart > 0) // going left down
            {
                count++;
                io_RowToStart++;
                io_ColumnToStart--;
            }

            io_RowToStart = i_Row;
            io_ColumnToStart = i_Column;

            while (io_RowToStart > 0 && io_ColumnToStart < ColumnSize - 1) // going up right
            {
                count++;
                io_RowToStart--;
                io_ColumnToStart++;
            }

            return count + 1;
        }

        public char[] GetAntiDiagonal(int i_Row, int i_Column) //  '/'
        {
            int columnToStart = i_Column;
            int currentRow = i_Row;
            int antiDiagonalSize = GetAntiDiagonalSize(i_Row, i_Column, ref currentRow, ref columnToStart);
            char[] antiDiagonal = new char[antiDiagonalSize];
            int indexToInsert = antiDiagonalSize - 1;

            for (int i = columnToStart; i >= 0 && currentRow < RowSize; i--)
            {
                antiDiagonal[indexToInsert] = Matrix[currentRow, i];
                currentRow++;
                indexToInsert--;
            }

            return antiDiagonal;
        }

        public bool IsFullColumn(int i_ColumnNumber)
        {
            bool isFullColumn = false;

            if (Matrix[0, i_ColumnNumber] != ' ')
            {
                isFullColumn = true;
                AvailableColumns.Remove(i_ColumnNumber);
            }

            return isFullColumn;
        }

        public bool IsFullBoard()
        {
            bool isFull = true;

            for (int i = 0; i < ColumnSize; i++)
            {
                isFull = IsFullColumn(i);

                if (isFull == false)
                {
                    break;
                }

            }

            return isFull;
        }

        public int UpdateBoardAccordingToColumn(int i_Column, char i_Sign)
        {
            int row = GetIndexOfRowToInsertAccordingToIndexOfColumn(i_Column);
            Matrix[row, i_Column] = i_Sign;

            return row;
        }

        public void ClearBoard()
        {
            for (int i = 0; i < RowSize; i++)
            {
                for (int j = 0; j < ColumnSize; j++)
                {
                    Matrix[i, j] = ' ';
                }

            }

            AvailableColumns = Enumerable.Range(0, ColumnSize).ToList();
        }

    }
}