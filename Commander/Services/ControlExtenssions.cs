using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Commander.Services
{
    public static class ControlExtenssions
    {
        /// <summary>
        /// 扩展control类型，使得在多线程情况下可以直接更新控件
        /// </summary>
        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                if (!control.IsDisposed)
                    control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
