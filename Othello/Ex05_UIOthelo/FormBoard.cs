namespace Ex05_UIOthelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Ex05_LogicOthelo;

    public partial class FormBoard : Form
    {
        private Square[,] m_SquaresBoard;

        public FormBoard(int i_BoardSize)
        {
            InitializeComponent();
            this.ClientSize = new Size((i_BoardSize * 43) + 17, (i_BoardSize * 43) + 17);
            InitBoard(i_BoardSize);
        }

        public Square[,] SquaresBoard
        {
            get
            {
                return m_SquaresBoard;
            }
        }

        private void FormBoard_Load(object sender, EventArgs e)
        {
        }

        public void InitBoard(int i_BoardSize)
        {
            m_SquaresBoard = new Square[i_BoardSize, i_BoardSize];

            for(int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_SquaresBoard[i, j] = new Square(i, j);
                    m_SquaresBoard[i, j].Left = this.Left + (j * 43) + 10;
                    m_SquaresBoard[i, j].Top = this.Top + (i * 43) + 10;
                    this.Controls.Add(m_SquaresBoard[i, j]);
                }
            }
        }

        public void DrowBoard(eBoardSign[,] i_GameMatrix, bool[,] i_ValidMovesMatrix)
        {
            for (int i = 0; i < m_SquaresBoard.GetLength(0); i++)
            {
                for (int j = 0; j < m_SquaresBoard.GetLength(1); j++)
                {
                    if (i_GameMatrix[i, j] == eBoardSign.Empty)
                    {
                        if (i_ValidMovesMatrix[i, j] == true)
                        {
                            m_SquaresBoard[i, j].SetSquareValues(null, true);
                        }
                        else
                        {
                            m_SquaresBoard[i, j].SetSquareValues(null, false);
                        }
                    }
                    else
                    {
                        if (i_GameMatrix[i, j] == eBoardSign.O)
                        {
                            m_SquaresBoard[i, j].SetSquareValues(Ex05_UIOthelo.Properties.Resources.CoinBlack, false);
                        }
                        else
                        {
                            m_SquaresBoard[i, j].SetSquareValues(Ex05_UIOthelo.Properties.Resources.CoinWhite, false);
                        }
                    }
                }
            }

            this.Refresh();
        }
    }
}
