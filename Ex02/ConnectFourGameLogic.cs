using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02
{
   public class ConnectFourGameLogic
    {
        public void MakeMove(Board connectFourBoard, Board.BoardSquare playerCoin, int selectedColumn, ref int lastInsertedToRow, ref bool gameWon)
        {
            int rowToInsertTo = GetFirstOpenSpotInColumn(connectFourBoard, selectedColumn);
            connectFourBoard[rowToInsertTo, selectedColumn] = playerCoin;
            lastInsertedToRow = rowToInsertTo;

            gameWon = CheckIfGameWon(connectFourBoard, rowToInsertTo, selectedColumn, playerCoin);
        }

        public int GetFirstOpenSpotInColumn(Board connectFourBoard, int column)
        {
            int bottomRow = connectFourBoard.NumOfRows - 1;
            bool openSpotFound = false;
            int openSpotRowNum = 0;

            for (int i = bottomRow; i >= 0 && !openSpotFound; i--)
            {
                if(connectFourBoard[i, column] == Board.BoardSquare.Blank)
                {
                    openSpotFound = true;
                    openSpotRowNum = i;
                }
            }

            return openSpotRowNum;
        }

        private bool CheckIfGameWon(Board connectFourBoard, int lastInsertedRow, int lastInsertedCol, Board.BoardSquare playerCoin)
        {
            bool gameWon = false;

            gameWon = CheckRowForConnectedFour(connectFourBoard, lastInsertedRow, playerCoin);

            if (!gameWon)
            {
                gameWon = CheckColForConnectedFour(connectFourBoard, lastInsertedCol, playerCoin);

                if (!gameWon)
                {
                    gameWon = CheckDiagonalsForConnectedFour(connectFourBoard, lastInsertedRow, lastInsertedCol, playerCoin);
                }
            }

            return gameWon;
        }

        private bool CheckRowForConnectedFour(Board connectFourBoard, int lastInsertedRow, Board.BoardSquare playerCoin)
        {
            int connectedFourFound = 0;
            bool connectedFourFoundInRow = false;

            string rowString = connectFourBoard[lastInsertedRow];

            if (playerCoin == Board.BoardSquare.Player1)
            {
                connectedFourFound = rowString.IndexOf("OOOO");
            }
            else
            {
                connectedFourFound = rowString.IndexOf("XXXX");
            }

            if (connectedFourFound != -1)
            {
                connectedFourFoundInRow = true;
            }

            return connectedFourFoundInRow;
        }

        private bool CheckColForConnectedFour(Board connectFourBoard, int lastInsertedCol, Board.BoardSquare playerCoin)
        {
            bool connectedFourFoundInCol = false;
            int numOfRows = connectFourBoard.NumOfRows;
            int coinCount = 0;
            for (int i = 0; i < numOfRows; i++)
            {
                if (connectFourBoard[i, lastInsertedCol] == playerCoin)
                {
                    coinCount++;

                    if (coinCount == 4)
                    {
                        connectedFourFoundInCol = true;
                    }
                }
                else
                {
                    coinCount = 0;
                }
            }

            return connectedFourFoundInCol;
        }

        private bool CheckDiagonalsForConnectedFour(Board connectFourBoard, int lastInsertedRow, int lastInsertedCol, Board.BoardSquare playerCoin)
        {
            bool gameWon = false;

            gameWon = CheckLeftDiagonalForConnectedFour(connectFourBoard, lastInsertedRow, lastInsertedCol, playerCoin);

            if (!gameWon)
            {
                gameWon = CheckRightDiagonalForConnectedFour(connectFourBoard, lastInsertedRow, lastInsertedCol, playerCoin);
            }

            return gameWon;
        }

        private bool CheckRightDiagonalForConnectedFour(Board connectFourBoard, int row, int col, Board.BoardSquare playerCoin)
        {
            bool validLocation = false;
            int i = 0, coinCount = 0;
            bool connectedFourFoundInLeftDiagonal = false;

            validLocation = connectFourBoard.CheckIfValidLocationOnBoard(row - i, col + i);

            while (validLocation)
            {
                if (connectFourBoard[row - i, col + i] == playerCoin)
                {
                    coinCount++;

                    if (coinCount == 4)
                    {
                        connectedFourFoundInLeftDiagonal = true;
                    }
                }
                else
                {
                    coinCount = 0;
                }

                i++;

                validLocation = connectFourBoard.CheckIfValidLocationOnBoard(row - i, col + i);
            }

            i = 1;

            validLocation = connectFourBoard.CheckIfValidLocationOnBoard(row + i, col - i);

            while (validLocation)
            {
                if (connectFourBoard[row + i, col - i] == playerCoin)
                {
                    coinCount++;

                    if (coinCount == 4)
                    {
                        connectedFourFoundInLeftDiagonal = true;
                    }
                }
                else
                {
                    coinCount = 0;
                }

                i++;

                validLocation = connectFourBoard.CheckIfValidLocationOnBoard(row + i, col - i);
            }

            return connectedFourFoundInLeftDiagonal;
        }

        private bool CheckLeftDiagonalForConnectedFour(Board connectFourBoard, int row, int col, Board.BoardSquare playerCoin)
        {
            bool validLocation = false;
            int i = 0, coinCount = 0;
            bool connectedFourFoundInLeftDiagonal = false;

            validLocation = connectFourBoard.CheckIfValidLocationOnBoard(row - i, col - i);

            while (validLocation)
            {
                if (connectFourBoard[row - i, col - i] == playerCoin)
                {
                    coinCount++;

                    if (coinCount == 4)
                    {
                        connectedFourFoundInLeftDiagonal = true;
                    }
                }
                else
                {
                    coinCount = 0;
                }

                i++;

                validLocation = connectFourBoard.CheckIfValidLocationOnBoard(row - i, col - i);
            }

            i = 1;

            validLocation = connectFourBoard.CheckIfValidLocationOnBoard(row + i, col + i);

            while (validLocation)
            {
                if (connectFourBoard[row + i, col + i] == playerCoin)
                {
                    coinCount++;

                    if (coinCount == 4)
                    {
                        connectedFourFoundInLeftDiagonal = true;
                    }
                }
                else
                {
                    coinCount = 0;
                }

                i++;

                validLocation = connectFourBoard.CheckIfValidLocationOnBoard(row + i, col + i);
            }

            return connectedFourFoundInLeftDiagonal;
        }
    }
}
