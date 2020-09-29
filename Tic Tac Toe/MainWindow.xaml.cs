using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        // Holds the current results of cells in the active game
        private MarkType[] mResults;

        // True if it is player 1's (X) turn or player 2's "O" turn
        private bool mPlayer1Turn;

        //True if game has ended
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            NewGame();

        }
        #endregion


        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            // Create a new blank array of the free cells in the grid
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            // Make sure Player 1 starts the game
            mPlayer1Turn = true;

            // Interate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game hasn't finished
            mGameEnded = false;
        }


        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Starts a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;

            // Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // change noughts to red
            if (!mPlayer1Turn)
                button.Foreground = Brushes.DarkRed;

            // Toggle the players turns
            mPlayer1Turn ^= true;

            // Check for a winner
            CheckForWinner();

        }

        /// <summary>
        /// Checks if ther is a winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            // Check for horizontal wins
            // - Row 0
            if(mResults[0]!= MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.LightSeaGreen;
            }

            // Check for horizontal wins
            // - Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.LightSeaGreen;
            }

            // Check for horizontal wins
            // - Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.LightSeaGreen;
            }

            // Check for vertical wins
            // - Column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.LightSeaGreen;
            }

            // Check for vertical wins
            // - Column 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.LightSeaGreen;
            }

            // Check for vertical wins
            // - Column 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.LightSeaGreen;
            }

            // Check for diagonal wins
            // - diagonal top left to bottom right
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.LightSeaGreen;
            }

            // Check for diagonal wins
            // - diagonal bottom left to top right
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[4] & mResults[2]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.LightSeaGreen;
            }


            // Check for no winner and full board
            if (!mResults.Any(result => result == MarkType.Free))
            {
                // Game ended
                mGameEnded = true;

                // Turn all cells to orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    // Change background, foreground and content to default values
                    button.Background = Brushes.LightSalmon;
                });
            }
        }

        // Close window on pressing Escape
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
