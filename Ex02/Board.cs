using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02
{
    public class Board
    {
        public enum BoardSquare
        {
            Blank = ' ', Player1 = 'O', Player2 = 'X'
        }

       private int m_NumOfRows;
       private int m_NumOfColumns;
       private BoardSquare[,] m_GameBoard;

        public int NumOfRows
       {
           get { return m_NumOfRows; }
           set { m_NumOfRows = value; }
       }

        public int NumOfColumns
        {
            get
            {
                return m_NumOfColumns; 
            }

            set 
            {
                m_NumOfColumns = value;
            }
        }

        public BoardSquare this[int row, int col]
        {
            get 
            {
                return m_GameBoard[row, col];
            }

            set
            {
                m_GameBoard[row, col] = value;
            }
        }

        public string this[int row]
        {
            get
            {
                return ConvertBoardSquareArrayToString(row);
            }
        }

        public void InitializeBoard(int numOfCols, int numOfRows)
        {
            m_GameBoard = new BoardSquare[numOfRows, numOfCols];

            for (byte i = 0; i < numOfRows; i++)
            {
                for (byte j = 0; j < numOfCols; j++)
                {
                    m_GameBoard[i, j] = BoardSquare.Blank;
                }
            }
        }

        private string ConvertBoardSquareArrayToString(int row)
        {
            char[] tempCharArray = new char[m_NumOfColumns];

            for(int i = 0;  i < m_NumOfColumns;  i++)
            {
                tempCharArray[i] = (char)m_GameBoard[row, i];
            }

            string convertedResult = new string(tempCharArray);

            return convertedResult;
        }

        public bool CheckIfBoardFull()
        {
            bool boardFull = true;

            for (int i = 0; i < NumOfColumns; i++)
            {
                if (this[0, i] == BoardSquare.Blank)
                {
                    boardFull = false;
                }
            }

            return boardFull;
        }

        public bool CheckIfValidLocationOnBoard(int row, int col)
        {
            bool validLocation = true;
            int maxRowIndex = NumOfRows - 1;
            int maxColIndex = NumOfColumns - 1;

            if (row > maxRowIndex || row < 0)
            {
                validLocation = false;
            }

            if (col > maxColIndex || col < 0)
            {
                validLocation = false;
            }

            return validLocation;
        }

        public void PrintBoard()
        {
            int numOfEquales = (NumOfColumns + 1) + (NumOfColumns * 3);
            string colNumber;

            for (byte g = 1; g <= NumOfColumns; g++)
            {
                colNumber = string.Format("  {0} ", g);
                System.Console.Write(colNumber);
            }

            System.Console.WriteLine();

            for (byte i = 0; i < NumOfRows; i++)
            {
                System.Console.Write("|");
                for (byte j = 0; j < NumOfColumns; j++)
                {
                    System.Console.Write(" ");
                    System.Console.Write((char)this[i, j]);
                    System.Console.Write(" |");
                }

                System.Console.WriteLine();

                for (byte g = 0; g < numOfEquales; g++)
                {
                    System.Console.Write("=");
                }

                System.Console.WriteLine();
            }
        }
    }
}
