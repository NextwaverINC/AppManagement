using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace ToolsDocument
{
    public class DocumentControls
    {
        ArrayList AL = new ArrayList();
        public void AddControls(Control ctl)
        {
            AL.Add(ctl);
        }
        public Control GetControls(int Index)
        {
            return (Control)AL[Index];
        }
        public int CountControl
        {
            get { return AL.Count; }
        }
    }

}
