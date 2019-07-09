namespace Ex05_LogicOthelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Game
    {
        private Board m_Board;
        private UserPlayer m_UserPlayerX;
        private UserPlayer m_UserPlayerO;
        private PcPlayer m_PcPlayerO;
        private int m_NumOfUserPlayers;
        private int m_XwinningCounter;
        private int m_OwinningCounter;
        private bool m_AreThereMovesForX;
        private bool m_AreThereMovesForO;
        private ePlayer m_NowPlaying = ePlayer.Xplayer;

        public event Notifier<eBoardSign[,], bool[,]> MatrixChangeListeners;

        public event Notifier<ePlayer, bool> GameOverListeners;

        public event Notifier WhosTurnListeners;

        public Game(int i_BoardSize, int i_NumOfUserPlayers)
        {
            initGame(i_BoardSize, i_NumOfUserPlayers);
        }

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        public ePlayer NowPlaying
        {
            get
            {
                return m_NowPlaying;
            }

            set
            {
                m_NowPlaying = value;
            }
        }

        public void MakePlayerTurn(int i_Row, int i_Col, eBoardSign i_Sign)
        {
            bool isGameOver;
            ePlayer nextPlayer = getNextPlayer();

            m_AreThereMovesForO = true;
            m_AreThereMovesForX = true;

            m_Board.FlipSquaresSigns(i_Row, i_Col, i_Sign);
            isGameOver = updateGameAccordingToMove(nextPlayer);

            if (m_NumOfUserPlayers == 1 && m_AreThereMovesForO)
            {
                do
                {
                    System.Threading.Thread.Sleep(2000);
                    chooseAndApplyPcrMove();
                    isGameOver = updateGameAccordingToMove(ePlayer.Xplayer);
                }
                while (!m_AreThereMovesForX && m_AreThereMovesForO);
            }

            if (isGameOver)
            {
                handleGameResult();
            }
        }

        private ePlayer getNextPlayer()
        {
            ePlayer nextPlayer;

            if (m_NowPlaying == ePlayer.Xplayer)
            {
                if (m_NumOfUserPlayers == 2)
                {
                    nextPlayer = ePlayer.Oplayer;
                }
                else
                {
                    nextPlayer = ePlayer.PcOPlayer;
                }
            }
            else
            {
                nextPlayer = ePlayer.Xplayer;
            }

            return nextPlayer;
        }

        public int XwinningCounter
        {
            get
            {
                return m_XwinningCounter;
            }
        }

        public int OwinningCounter
        {
            get
            {
                return m_OwinningCounter;
            }
        }

        /// return value is Game Over
        private bool updateGameAccordingToMove(ePlayer i_NextPlayer)
        {
            updateValidMovesMembers(i_NextPlayer);

            if (!m_AreThereMovesForO || !m_AreThereMovesForX)
            {
                updateValidMovesMembers(m_NowPlaying);
            }
            else
            {
                changeTurn();
            }

            MatrixChangeListeners.Invoke(m_Board.GameMatrix, m_Board.ValidMovesMatrix);

            bool isGameOver = m_Board.IsBoardFull() || (!m_AreThereMovesForX && !m_AreThereMovesForO);

            return isGameOver;
        }

        private void updateValidMovesMembers(ePlayer i_Player)
        {
            if (i_Player == ePlayer.Xplayer)
            {
                m_AreThereMovesForX = m_Board.UpdateValidMovesMatrix(i_Player);
            }
            else
            {
                m_AreThereMovesForO = m_Board.UpdateValidMovesMatrix(i_Player);
            }
        }

        private void changeTurn()
        {
            if (m_NowPlaying == ePlayer.Oplayer || m_NowPlaying == ePlayer.PcOPlayer)
            {
                m_NowPlaying = ePlayer.Xplayer;
            }
            else
            {
                if (m_NumOfUserPlayers == 2)
                {
                    m_NowPlaying = ePlayer.Oplayer;
                }
                else
                {
                    m_NowPlaying = ePlayer.PcOPlayer;
                }
            }

            WhosTurnListeners.Invoke();
        }

        public void StartNewGame()
        {
            m_Board.InitBoard();
            MatrixChangeListeners.Invoke(Board.GameMatrix, Board.ValidMovesMatrix);
            NowPlaying = ePlayer.Xplayer;
            WhosTurnListeners.Invoke();
        }

        private void handleGameResult()
        {
            ePlayer winner;
            bool isTie = false;

            if (m_Board.XScore > m_Board.OScore)
            {
                winner = ePlayer.Xplayer;
                m_XwinningCounter++;
            }
            else if (m_Board.OScore > m_Board.XScore)
            {
                if (m_NumOfUserPlayers == 2)
                {
                    winner = ePlayer.Oplayer;
                }
                else
                {
                    winner = ePlayer.PcOPlayer;
                }

                m_OwinningCounter++;
            }
            else
            {
                winner = 0;
                isTie = true;
                m_OwinningCounter++;
                m_XwinningCounter++;
            }

            GameOverListeners.Invoke(winner, isTie);
        }
        
        private void initGame(int i_BoardSize, int i_NumOfUserPlayers)
        {
            m_NumOfUserPlayers = i_NumOfUserPlayers;

            m_Board = new Board(i_BoardSize);
            m_Board.UpdateValidMovesMatrix(ePlayer.Xplayer);

            m_UserPlayerX = new UserPlayer(eBoardSign.X);

            if (i_NumOfUserPlayers == 2)
            {
                m_UserPlayerO = new UserPlayer(eBoardSign.O);
            }
            else
            {
                m_PcPlayerO = new PcPlayer(m_Board.GetMatrixRowAndColLength());
            }
        }

        private void chooseAndApplyPcrMove()
        {
            int rowChoose, colmChoose;
            m_PcPlayerO.CalculateNextMove(out rowChoose, out colmChoose, m_Board.ValidMovesMatrix);
            m_Board.FlipSquaresSigns(rowChoose, colmChoose, m_PcPlayerO.Sign);
        }
    }
}