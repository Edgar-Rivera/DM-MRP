using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASG
{
    public static class MessageASG
    {
        public static void showMessage(string _message, string _stage, int _type)
        {
            if (_type == 0)
            {
                MessageBox.Show(_message, _stage, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                MessageBox.Show(_message, _stage, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static bool ShowConfirmMessage(string _message, string _stage)
        {
            DialogResult dialogResult = MessageBox.Show(_message, _stage, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

    }
}
