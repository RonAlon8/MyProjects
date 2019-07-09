namespace Ex05_LogicOthelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PcPlayer
    {
        private eBoardSign m_Sign = eBoardSign.O;
        private int[] m_ValidSquaresToChooseArr;
        private int m_NumOfValidSquaresToChoose;
        private Random m_Random = new Random();

        public PcPlayer(int i_Size)
        {
            m_ValidSquaresToChooseArr = new int[i_Size * i_Size];
            m_NumOfValidSquaresToChoose = 0;
        }

        public eBoardSign Sign
        {
            get
            {
                return m_Sign;
            }
        }

        public void UpdateValidSquaresToChooseArr(bool[,] i_ValidMovesMatrix)
        {
            m_NumOfValidSquaresToChoose = 0;
            int rowAndColmLength = i_ValidMovesMatrix.GetLength(0);

            for (int i = 0; i < rowAndColmLength; i++)
            {
                for (int j = 0; j < rowAndColmLength; j++)
                {
                    if (i_ValidMovesMatrix[i, j] == true)
                    {
                        m_ValidSquaresToChooseArr[m_NumOfValidSquaresToChoose] = (i * rowAndColmLength) + j;
                        m_NumOfValidSquaresToChoose++;
                    }
                }
            }
        }

        public void CalculateNextMove(out int o_RowChoose, out int o_ColmChoose, bool[,] i_ValidMovesMatrix)
        {
            UpdateValidSquaresToChooseArr(i_ValidMovesMatrix);
            int rowAndColmLength = i_ValidMovesMatrix.GetLength(0);

            int pcChoise = m_Random.Next(0, m_NumOfValidSquaresToChoose);
            o_RowChoose = m_ValidSquaresToChooseArr[pcChoise] / rowAndColmLength;
            o_ColmChoose = m_ValidSquaresToChooseArr[pcChoise] % rowAndColmLength;
        }
    }
}
