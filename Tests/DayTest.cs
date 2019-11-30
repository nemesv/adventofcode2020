using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class DayTest<TDay>
    {
        public TDay Sut(string input)
        {
            return (TDay)System.Activator.CreateInstance(typeof(TDay), input);
        }
    }
}
