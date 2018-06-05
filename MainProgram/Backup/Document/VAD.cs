using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Document
{
    public class VAD
    {
        public static bool isNull(TextBox tbTemp)
        {
            if (tbTemp.Text == "")
                return true;
            else
                return false;
        }
    }
}
