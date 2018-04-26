using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02
{
    public class Interface
    {
        public static void PrintMainMenu()
        {
            string m_MainMenu = @"_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ Connect Four Game _-_-_-_-_-_-_-_-_-_-_-_-_-_-_
Please select one of the following options:

1.Start Match
2.Quit Game";
            System.Console.WriteLine(m_MainMenu);
        }

        public static void PrintPlayerWon(int playerNum)
        {
            System.Console.WriteLine("{1}Game Over! Player {0} has won the match and increased his score by one point!{1}", playerNum, Environment.NewLine);
        }

        public static void PrintComputerWon()
        {
            System.Console.WriteLine("{0}Game Over! Computer has won the match and increased his score by one point!{0}", Environment.NewLine);
        }

        public static void PrintScoreBoard(int player1Score, int player2Score, GameManager.GameMode currGameMode)
        {
            if (currGameMode == GameManager.GameMode.PlayerVsPlayer)
            {
                System.Console.WriteLine(
    @"                    ************************************
                    **                                **
                    **           SCOREBOARD           **
                    **                                **
                    **      Player 1 Score: {0}         **
                    **                                **
                    **      Player 2 Score: {1}         **
                    **                                **
                    ************************************",
                                                             player1Score,
                                                             player2Score);
            }
            else
            {
                System.Console.WriteLine(
@"                    ************************************
                    **                                **
                    **           SCOREBOARD           **
                    **                                **
                    **      Player 1 Score: {0}         **
                    **                                **
                    **      Computer Score: {1}         **
                    **                                **
                    ************************************",
                                             player1Score,
                                             player2Score);
            }
        }
        
        public static void PrintDrawBoardFull()
        {
            System.Console.WriteLine(@"Game Over! The board is full, Player 1 & Player 2 have reached a DRAW!{0}", Environment.NewLine);
        }

        public static void PrintPlayAgainRequest()
        {
            System.Console.WriteLine(@"Would you like to play again?
1.Yes
2.No");
        }

        public static void PrintBoardDimensionsError()
        {
            System.Console.WriteLine("Error - The number of Rows and Columns must each be between 4 and 8!");
        }

        public static void PrintQuitMessege()
        {
            System.Console.WriteLine("Thank you for playing Connect Four, Goodbye!");
        }

        public static void PrintPlayerTurn(int playerNum)
        {
            System.Console.WriteLine(@"Player {0} it's your turn, please enter the column number you would like to insert your coin to:{1}", playerNum, Environment.NewLine);
        }

        public static void PrintInvalidActionNoRoom()
        {
            System.Console.WriteLine("Invalid Action - selected column is full, please select a different column..");
        }

        public static void PrintSelectionError(int minRange, int maxRange)
        {
            System.Console.WriteLine(string.Format("Error - Invalid selection, please select an option between {0} and {1}..", minRange, maxRange));
        }

        public static void PrintRowSizeRequest()
        {
            string m_RequestRowSize = "Please enter the desired amount of rows in the game board:";
            System.Console.WriteLine(m_RequestRowSize);
        }

        public static void PrintColSizeRequest()
        {
            string m_RequestColumnSize = "Please enter the desired amount of columns in the game board:";
            System.Console.WriteLine(m_RequestColumnSize);
        }

        public static void PrintGameModeSubMenu()
        {
            string m_SelectGameModeSubMenu = @"Please select a game mode:

1.Player vs Player
2.Player vs AI";
            System.Console.WriteLine(m_SelectGameModeSubMenu);
        }

        public static void PrintBoard(Board board)
        {
            board.PrintBoard();
        }
    }
}
