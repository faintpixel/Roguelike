using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DodongosQuest.Creatures
{
    public class Stat
    {
        public readonly int Maximum;
        public int Current;

        public Stat(int maximum)
        {
            Maximum = maximum;
            Current = maximum;
        }

        public Stat(int maximum, int current)
        {
            Maximum = maximum;
            Current = current;
        }
    }
}
