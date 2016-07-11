/*   
      BroadcastMsgFilter.cs (KIRSmartAV)
      ============================================
      Copyright(C) 2016  Fahmi Noor Fiqri
  
      This program is free software: you can redistribute it and/or modify
      it under the terms of the GNU Lesser General Public License as published by
      the Free Software Foundation, either version 3 of the License, or
      (at your option) any later version.
  
      This program is distributed in the hope that it will be useful,
      but WITHOUT ANY WARRANTY; without even the implied warranty of   
      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
      GNU Lesser General Public License for more details.
  
      You should have received a copy of the GNU Lesser General Public License
      along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

using KIRSmartAV.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace KIRSmartAV.ApplicationServices
{
    class BroadcastMsgFilter : IMessageFilter
    {
        private static LogManager _logger = LogManager.GetClassLogger();

        public bool PreFilterMessage(ref Message m)
        {
            // show main form
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                _logger.Debug("Got WM_SHOWME broadcast message, showing \"MainForm\"");
                KcavContext.CurrentContext.ShowMainForm();
                KcavContext.CurrentContext.BringMainForm();
                return true;
            }

            // show baloon tip
            if (m.Msg == NativeMethods.WM_QUICKFIXNOTIFY)
            {
                _logger.Debug("QuickFix has finished it's job.");
                KcavContext.CurrentContext.ShowBaloonTip("QuickFix Auto-Action", strings.QuickFixFinished, ToolTipIcon.Info);
                return true;
            }

            // return the rest as unhandled
            return false;
        }
    }
}
