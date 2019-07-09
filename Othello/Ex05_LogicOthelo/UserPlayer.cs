namespace Ex05_LogicOthelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserPlayer
    {
        private eBoardSign m_Sign;

        public eBoardSign Sign
        {
            get
            {
                return m_Sign;
            }
        }

        public UserPlayer(eBoardSign i_Sign)
        {
            m_Sign = i_Sign;
        }
    }
}
