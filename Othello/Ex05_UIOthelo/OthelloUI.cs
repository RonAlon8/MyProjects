namespace Ex05_UIOthelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using Ex05_LogicOthelo;

    public class OthelloUI
    {
        private const string k_XPlayerColor = "White";
        private const string k_OPlayerColor = "Black";
        private FormGameSetting m_FormGameSetting = new FormGameSetting();
        private FormBoard m_FormBoard;
        private Game m_Game;

        public OthelloUI()
        {
            m_FormGameSetting.NewGameListeners += this.StartGameAccordingToSettings;
        }

        public void Run()
        {
            m_FormGameSetting.ShowDialog();
        }

        public void StartGameAccordingToSettings(int i_NumOfUserPlayers)
        {
            m_Game = new Game(m_FormGameSetting.BoardSize, i_NumOfUserPlayers);
            m_Game.GameOverListeners += m_Game_gameOverListeners;
            m_Game.WhosTurnListeners += m_Game_whosTurnListeners;

            m_FormGameSetting.Hide();

            m_FormBoard = new FormBoard(m_FormGameSetting.BoardSize);
            m_Game_whosTurnListeners();
            m_Game.MatrixChangeListeners += m_FormBoard.DrowBoard;

            for (int i = 0; i < m_FormGameSetting.BoardSize; i++)
            {
                for (int j = 0; j < m_FormGameSetting.BoardSize; j++)
                {
                    m_FormBoard.SquaresBoard[i, j].Click += OthelloUI_Click;
                }
            }

            m_FormBoard.DrowBoard(m_Game.Board.GameMatrix, m_Game.Board.ValidMovesMatrix);
            m_FormBoard.ShowDialog();
        }

        private void m_Game_whosTurnListeners()
        {
            string playerColor;

            if (m_Game.NowPlaying == ePlayer.Xplayer)
            {
                playerColor = k_XPlayerColor;
            }
            else
            {
                playerColor = k_OPlayerColor;
            }

            m_FormBoard.Text = string.Format("Othello - {0}'s turn", playerColor);
        }

        private void m_Game_gameOverListeners(ePlayer i_WinnerNAme, bool i_IsTie)
        {
            string gameOverMessage, winnerName, questionMsg = "Would you like another round?";
            int winnerScore, loserScore, winsCounter;

            if (i_IsTie)
            {
                gameOverMessage = string.Format(
@"It's a Tie! ({0}/{1})
{2}",
m_Game.Board.XScore,
m_Game.Board.OScore,
questionMsg);
            }
            else
            {
                if (i_WinnerNAme == ePlayer.Xplayer)
                {
                    winnerName = k_XPlayerColor;
                    winnerScore = m_Game.Board.XScore;
                    loserScore = m_Game.Board.OScore;
                    winsCounter = m_Game.XwinningCounter;
                }
                else
                {
                    winnerName = k_OPlayerColor;
                    winnerScore = m_Game.Board.OScore;
                    loserScore = m_Game.Board.XScore;
                    winsCounter = m_Game.OwinningCounter;
                }

                gameOverMessage = string.Format(
@"{0} Won!! ({1}/{2}) ({3}/{4}))
{5}",
winnerName,
winnerScore,
loserScore,
winsCounter,
m_Game.XwinningCounter + m_Game.OwinningCounter,
questionMsg);
            }

            DialogResult dialogResult = MessageBox.Show(gameOverMessage, "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                m_Game.StartNewGame();
            }
            else
            {
                m_FormBoard.Hide();
            }
        }

        private void OthelloUI_Click(object sender, EventArgs e)
        {
            int row = 0, colm = 0;
            eBoardSign boardSign;

            Square squareToCheck = sender as Square;

            if (squareToCheck != null)
            {
                row = squareToCheck.Row;
                colm = squareToCheck.Colm;
            }

            if (m_Game.NowPlaying == ePlayer.Xplayer)
            {
                boardSign = eBoardSign.X;
            }
            else
            {
                boardSign = eBoardSign.O;
            }

            m_Game.MakePlayerTurn(row, colm, boardSign);
        }
    }
}
