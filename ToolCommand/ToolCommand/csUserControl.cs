using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ToolCommand
{
    public class csUserControl
    {
        public string OfficeSpaceId;
        public ucForm getPanel(string strConnect, string user, string Position, string Command)
        {
            ucForm UCF = new ucForm();

            switch (Command)
            {
                case "TestCS":
                    Label labTemp = new Label();
                    labTemp.Text = "text";
                    UCF._Header = "ทดสอบระบบ";
                    UCF._MainPanel.Controls.Add(labTemp);
                    break;
            }

            return UCF;
        }
    }
}
