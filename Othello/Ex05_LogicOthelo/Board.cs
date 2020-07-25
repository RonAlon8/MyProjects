namespace Ex05_LogicOthelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Board
    {
        private eBoardSign[,] m_GameMatrix = null;
        private bool[,] m_ValidMovesMatrix;
        private int m_XCounter;
        private int m_OCounter;

        public Board(int i_Size)
        {
            m_GameMatrix = new eBoardSign[i_Size, i_Size];
            setStartValues();
            m_ValidMovesMatrix = new bool[i_Size, i_Size];
        }

        public eBoardSign[,] GameMatrix
        {
            get
            {
                return m_GameMatrix;
            }
        }

        public bool[,] ValidMovesMatrix
        {
            get
            {
                return m_ValidMovesMatrix;
            }
        }

        public int XScore
        {
            get
            {
                return m_XCounter;
            }
        }

        public int OScore
        {
            get
            {
                return m_OCounter;
            }
        }

        public void InitBoard()
        {
            makeAllZeroMat();
            setStartValues();
            UpdateValidMovesMatrix(ePlayer.Xplayer);
        }

        public bool IsBoardFull()
        {
            return m_XCounter + m_OCounter == m_GameMatrix.GetLength(0) * m_GameMatrix.GetLength(1);
        }

        private void makeAllZeroMat()
        {
            int matrixRowAndColLength = m_GameMatrix.GetLength(0);
            for (int i = 0; i < matrixRowAndColLength; i++)
            {
                for (int j = 0; j < matrixRowAndColLength; j++)
                {
                    m_GameMatrix[i, j] = 0;
                }
            }
        }

        private void setStartValues()
        {
            int matrixRowAndColLength = m_GameMatrix.GetLength(0);
            m_GameMatrix[((matrixRowAndColLength / 2) - 1), ((matrixRowAndColLength / 2) - 1)] = eBoardSign.O;
            m_GameMatrix[(matrixRowAndColLength / 2), (matrixRowAndColLength / 2)] = eBoardSign.O;
            m_GameMatrix[((matrixRowAndColLength / 2) - 1), (matrixRowAndColLength / 2)] = eBoardSign.X;
            m_GameMatrix[(matrixRowAndColLength / 2), ((matrixRowAndColLength / 2) - 1)] = eBoardSign.X;
            m_XCounter = 2;
            m_OCounter = 2;
        }

        public bool UpdateValidMovesMatrix(ePlayer i_NumOfPlayer)
        {
            bool isThereAMove = false;
            for (int i = 0; i < m_GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < m_GameMatrix.GetLength(1); j++)
                {
                    if (m_GameMatrix[i, j] == eBoardSign.Empty)
                    {
                        if (i_NumOfPlayer == ePlayer.Xplayer)
                        {
                            m_ValidMovesMatrix[i, j] = checkSquare(i, j, eBoardSign.X);
                        }
                        else
                        {
                            m_ValidMovesMatrix[i, j] = checkSquare(i, j, eBoardSign.O);
                        }
                    }
                    else
                    {
                        m_ValidMovesMatrix[i, j] = false;
                    }

                    isThereAMove |= m_ValidMovesMatrix[i, j];
                }
            }

            return isThereAMove;
        }

        private bool checkSquare(int i_Row, int i_Col, eBoardSign i_Sing)
        {
            bool isValidSquare = false;
            int dirX, dirY;
            int numOfSteps;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    dirX = i;
                    dirY = j;
                    if (!(i == 0 && j == 0))
                    {
                        isValidSquare |= checkIsValidDirection(i_Row, i_Col, i_Sing, dirX, dirY, out numOfSteps);
                    }
                }
            }

            return isValidSquare;
        }

        private bool checkIsValidDirection(int i_Row, int i_Col, eBoardSign i_Sing, int i_DirX, int i_DirY, out int o_NumOfSteps)
        {
            o_NumOfSteps = 0;
            bool isValidDir = false;
            int dirX = i_DirX, dirY = i_DirY;
            int sign = (int)i_Sing;
            bool isInBoundaries = checkIsInBoundaries(i_Row + dirX, i_Col + dirY);
            ///Note that X = 1 and O = -1.
            while (isInBoundaries && m_GameMatrix[i_Row + dirX, i_Col + dirY] == (eBoardSign)(sign * (-1)))
            {
                o_NumOfSteps++;
                dirX += i_DirX;
                dirY += i_DirY;
                isInBoundaries = checkIsInBoundaries(i_Row + dirX, i_Col + dirY);
            }

            if (isInBoundaries && o_NumOfSteps > 0)
            {
                if (m_GameMatrix[i_Row + dirX, i_Col + dirY] == i_Sing)
                {
                    isValidDir = true;
                }
            }

            return isValidDir;
        }

        private bool checkIsInBoundaries(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < m_GameMatrix.GetLength(0) && i_Col >= 0 && i_Col < m_GameMatrix.GetLength(1);
        }

        public bool GetValueFromMovesMatrix(int i_Row, int i_Col)
        {
            return m_ValidMovesMatrix[i_Row, i_Col];
        }

        public int GetMatrixRowAndColLength()
        {
            return m_GameMatrix.GetLength(0);
        }

        public void FlipSquaresSigns(int i_Row, int i_Col, eBoardSign i_Sing)
        {
            int dirX, dirY;
            int numOfSteps;

            if (i_Sing == eBoardSign.X)
            {
                m_XCounter++;
            }
            else
            {
                m_OCounter++;
            }

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    dirX = i;
                    dirY = j;
                    if (!(i == 0 && j == 0))
                    {
                        if (checkIsValidDirection(i_Row, i_Col, i_Sing, dirX, dirY, out numOfSteps))
                        {
                            m_GameMatrix[i_Row, i_Col] = i_Sing;
                            for (int k = 0; k < numOfSteps; k++)
                            {
                                m_GameMatrix[i_Row + dirX, i_Col + dirY] = i_Sing;
                                dirX += i;
                                dirY += j;
                                m_XCounter += (int)i_Sing;
                                m_OCounter -= (int)i_Sing;
                            }
                        }
                    }
                }
            }
        }

        public bool IsInBoardRange(string i_UserChoise, out int o_Row, out int o_Colm)
        {
            int rowAndColmLength = m_GameMatrix.GetLength(1);
            bool isInRange = (i_UserChoise[0] >= 'A' && i_UserChoise[0] < 'A' + rowAndColmLength) &&
                (i_UserChoise[1] - '0' > 0 && i_UserChoise[1] - '0' <= rowAndColmLength);

            if (isInRange)
            {
                o_Row = i_UserChoise[1] - '1';
                o_Colm = i_UserChoise[0] - 'A';
            }
            else
            {
                o_Row = -1;
                o_Colm = -1;
            }

            return isInRange;
        }
    }
}
