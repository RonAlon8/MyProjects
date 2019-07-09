namespace Ex05_LogicOthelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public delegate void Notifier<T1, T2>(T1 i_T1, T2 i_T2);

    public delegate void Notifier();
}
