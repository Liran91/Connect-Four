using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02
{
    public class GameManager
    {
        public enum GameState 
        {
            Quit = -1, ContinuePlaying, Draw, Victory
        }

        public enum GameMode
        {
            PlayerVsPlayer = 1, PlayerVsComputer = 2
        }

        public enum UserAnswer
        {
            Yes = 1, No
        }

        public enum PlayerID
        {
            Computer = 0, Player1, Player2
        }

        public enum ValidNumOfRows
        {
            MinRows = 4, MaxRows = 8
        }

        public enum ValidNumOfCols
        {
            MinCols = 4, MaxCols = 8
        }

        private Board connectFourBoard;
        private ConnectFourGameLogic connectFourGame;
        private readonly Random random;
        private GameMode currGameMode;
        public GameState CurrGameState;
        private bool gameInProgress;
        private int m_player1Score;
        private int m_player2Score;

       public GameManager()
        {
            connectFourBoard = new Board();
            connectFourGame = new ConnectFourGameLogic();
            random = new Random();

            m_player1Score = 0;
            m_player2Score = 0;
            gameInProgress = false;
        }

        public int Player1Score
        {
            get
            {
                return m_player1Score;
            }

            set
            {
                m_player1Score = value;
            }
        }

        public int Player2Score
        {
            get
            {
                return m_player2Score;
            }

            set
            {
                m_player2Score = value;
            }
        }

        public void RunGame()
        {
            SetupGame();
            PlayConnectFour();
        }

        private void PlayConnectFour()
        {
            bool keepPlaying = true;
            
            while(keepPlaying)
            {
                gameInProgress = true;

                while (CurrGameState == GameState.ContinuePlaying)
                {
                    PlayTurn(PlayerID.Player1, Board.BoardSquare.Player1);

                    if (CurrGameState == GameState.ContinuePlaying)
                    {
                        if (currGameMode == GameMode.PlayerVsComputer)
                        {
                            PlayTurn(PlayerID.Computer, Board.BoardSquare.Player2);
                        }
                        else
                        {
                            PlayTurn(PlayerID.Player2, Board.BoardSquare.Player2);
                        }
                    }
                }

                gameInProgress = false;
                keepPlaying = PlayAgainRequest();
            }

            Interface.PrintQuitMessege();
        }

        private bool PlayAgainRequest()
        {
           bool ans = false;

            Interface.PrintPlayAgainRequest();
            
            int selection = GetValidInput(1, 2, gameInProgress);

            if(selection == (int)UserAnswer.Yes)
            {
                ans = true;
                CurrGameState = GameState.ContinuePlaying;
                connectFourBoard.InitializeBoard(connectFourBoard.NumOfColumns, connectFourBoard.NumOfRows);
            }

            return ans;
        }

        private void UpdateScoreboard(PlayerID playerNum)
        {
            if(playerNum == PlayerID.Player1)
            {
                Player1Score++;
            }
            else
            {
                Player2Score++;
            }
        }

        private void PlayTurn(PlayerID playerNum, Board.BoardSquare playerCoin)
        {
            int selectedColumn;
            int lastInsertedToRow = 0;          
            bool boardFull = false;
            bool gameWon = false;

            Interface.PrintPlayerTurn((int)playerNum);
            Interface.PrintBoard(connectFourBoard);

            selectedColumn = GetValidColumn(currGameMode, playerNum);

            if (CurrGameState != GameState.Quit)
            {
                connectFourGame.MakeMove(connectFourBoard, playerCoin, selectedColumn, ref lastInsertedToRow, ref gameWon);
                Ex02.ConsoleUtils.Screen.Clear();

                if (gameWon)
                {
                    CurrGameState = GameState.Victory;
                    Interface.PrintBoard(connectFourBoard);

                    if(playerNum == PlayerID.Computer)
                    {
                        Interface.PrintComputerWon();
                    }
                    else
                    {
                        Interface.PrintPlayerWon((int)playerNum);
                    }
                    
                    UpdateScoreboard(playerNum);
                    Interface.PrintScoreBoard(Player1Score, Player2Score, currGameMode);
                    CurrGameState = GameState.Victory;
                }
                else
                {
                    boardFull = connectFourBoard.CheckIfBoardFull();

                    if (boardFull)
                    {
                        Interface.PrintDrawBoardFull();
                        Interface.PrintBoard(connectFourBoard);
                        Interface.PrintScoreBoard(Player1Score, Player2Score, currGameMode);
                        CurrGameState = GameState.Draw;
                    }
                }
            }
        }
        
        private void SetupGame()
        {
                    SetupBoard();
                    SetupGameMode();                             
        }

        private void SetupGameMode()
        {
            Interface.PrintGameModeSubMenu();
            int gameModeChoice = GetValidInput(1, 2, gameInProgress);

            if (gameModeChoice == (int)GameMode.PlayerVsComputer)
            {
                currGameMode = GameMode.PlayerVsComputer;
            }
            else
            {
                currGameMode = GameMode.PlayerVsPlayer;
            }
        }

        private void SetupBoard()
        {
            GetValidBoardDimensions();
            connectFourBoard.InitializeBoard(connectFourBoard.NumOfColumns, connectFourBoard.NumOfRows);
        }

        private void GetValidBoardDimensions()
        {
            int NumOfRows = 0, NumOfCols = 0;
            bool validBoardDimensions = false;

            while (!validBoardDimensions)
            {
                Interface.PrintColSizeRequest();
                NumOfCols = GetValidInput(4, 8, gameInProgress);

                Interface.PrintRowSizeRequest();
                NumOfRows = GetValidInput(4, 8, gameInProgress);

                if ((NumOfRows <= (int)ValidNumOfRows.MaxRows && NumOfRows >= (int)ValidNumOfRows.MinRows) && (NumOfCols <= (int)ValidNumOfCols.MaxCols && NumOfCols >= (int)ValidNumOfCols.MinCols))
                {
                    validBoardDimensions = true;
                }
                else
                {
                    Interface.PrintBoardDimensionsError();
                }
            }

            connectFourBoard.NumOfRows = NumOfRows;
            connectFourBoard.NumOfColumns = NumOfCols;
        }

        private int GetValidInput(int selectionRangeMin, int selectionRangeMax, bool gameInProgress)
        {
            int choice = -1;
            bool validChoice = false;

            while (!validChoice)
            {
                string inputString = System.Console.ReadLine();

                choice = -1;

                if (inputString == "Q" && gameInProgress)
                {
                    validChoice = true;
                    CurrGameState = GameState.Quit;
                }
                else
                {
                    if (int.TryParse(inputString, out choice))
                    {
                        if (choice >= selectionRangeMin && choice <= selectionRangeMax)
                        {
                            validChoice = true;
                        }
                    }

                    if (!validChoice)
                    {
                        Interface.PrintSelectionError(selectionRangeMin, selectionRangeMax);
                    }
                }
            }

            return choice;
        }

        private int GetValidColumn(GameMode gameMode, PlayerID playerId)
        {
            int selectedColumn = 1;
            int maxColIndex = connectFourBoard.NumOfColumns - 1; 
            bool validAction = false;

            while (!validAction)
            {
                if ((currGameMode == GameMode.PlayerVsComputer) && (playerId == PlayerID.Computer))
                {
                    selectedColumn = getRandomColumn(0, connectFourBoard.NumOfColumns);
                }
                else
                {
                    selectedColumn = GetValidInput(1, connectFourBoard.NumOfColumns, gameInProgress) - 1;
                }

                if (CurrGameState == GameState.Quit)
                {
                    validAction = true;
                }
                else
                {
                    validAction = CheckForRoom(connectFourBoard, selectedColumn);

                    if (!validAction)
                    {
                        if (playerId != PlayerID.Computer)
                        {
                            Interface.PrintInvalidActionNoRoom();
                        }
                    }
                }
            }

            return selectedColumn;
        }

        private bool CheckForRoom(Board connectFourBoard, int colNum)
        {
            bool roomAvailableInColumn = false;

            for (byte i = 0; i < connectFourBoard.NumOfRows; i++)
            {
                if (connectFourBoard[i, colNum] == Board.BoardSquare.Blank)
                {
                    roomAvailableInColumn = true;
                }
            }

            return roomAvailableInColumn;
        }

        private int getRandomColumn(int min, int max)
        {
                return random.Next(min, max);          
        }
    }
}
