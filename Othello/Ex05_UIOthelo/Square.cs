namespace Ex05_UIOthelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;

    public class Square : System.Windows.Forms.PictureBox
    {
        private int m_Row;
        private int m_Col;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        public Square(int i_Row, int i_Col)
        {
            this.Name = "SquareButton";
            this.BackColor = Color.LightGray;
            this.Width = 40;
            this.Height = 40;
            this.Enabled = false;
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int Row
        {
            get
            {
                return m_Row;
            }
        }

        public int Colm
        {
            get
            {
                return m_Col;
            }
        }
        
        public void SetSquareValues(Bitmap i_Image, bool i_Enabled)
        {
            if (i_Image == null)
            {
                if (i_Enabled == true)
                {
                    this.BackColor = Color.LightGreen;
                }
                else
                {
                    this.BackColor = Color.LightGray;
                }
            }

            this.BackgroundImage = i_Image;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Enabled = i_Enabled;
        }
    }
}
