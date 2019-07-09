using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05_UIOthelo
{
    public partial class FormGameSetting : Form
    {
        private const int k_OneUserPlayer = 1;
        private const int k_TwoUserPlayers = 3;
        private int m_BoardSize = 6;

        public event Action<int> NewGameListeners;

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public FormGameSetting()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
        }

        private void boardSizeButton_Click(object sender, EventArgs e)
        {
            m_BoardSize += 2;

            if (m_BoardSize > 12)
            {
                m_BoardSize = 6;
            }

            boardSizeButton.Text = string.Format(@"Board Size: {0}x{0} (click to increase)", m_BoardSize);
        }

        private void playAgainstComputerButton_Click(object sender, EventArgs e)
        {
            NewGameListeners.Invoke(k_OneUserPlayer);
        }

        private void playAgainstUserButton_Click(object sender, EventArgs e)
        {
            NewGameListeners.Invoke(k_TwoUserPlayers);
        }
    }
}
