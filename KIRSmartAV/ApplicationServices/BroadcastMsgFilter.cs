/*   
  BroadcastMsgFilter.cs (KIRSmartAV)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
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
