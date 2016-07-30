/*   
      MessagePumpManager.cs (KIRSmartAV)
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using KIRSmartAV.Localization;

namespace KIRSmartAV.ApplicationServices.MsgFilters
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    class MessagePumpManager : IDisposable
    {
        private static LogManager _logger = LogManager.GetClassLogger();

        private List<IMessageFilter> _msgFilters = null;
        private WndProcWatcher _internalForm = null;
        private bool _watching = false;

        // constructor
        public MessagePumpManager()
        {
            // set up filter collection
            _msgFilters = new List<IMessageFilter>();
            
            // setup watcher
            _internalForm = new WndProcWatcher();
            _internalForm.ProcArrived += ProcWatcher_ProcArrived;
        }

        #region Methods
        public void StartWndProcHandler()
        {
            _watching = true;
            _logger.Debug("MessagePump WndProc started.");
        }

        public void StopWndProcHandler()
        {
            _watching = false;
            _logger.Debug("MessagePump WndProc stopped.");
        }

        public void AddMsgFilter(IMessageFilter filter)
        {
            // add filter to application filter
            Application.AddMessageFilter(filter);
            _logger.Debug("Application message hook installed. Name: " + filter.GetType().Name);
        }

        public void AddWndProcFilter(IMessageFilter filter)
        {
            // add filter that needs to be in WndProc
            _msgFilters.Add(filter);
            _logger.Debug("WndProc message hook installed. Name: " + filter.GetType().Name);
        }
        #endregion

        private void ProcWatcher_ProcArrived(object sender, WndProcWatcher.WndProcArrived e)
        {
            // check if we are watching
            if (!_watching) return;

            // filter each message
            var msg = e.Msg;
            foreach (IMessageFilter filter in _msgFilters)
            {
                filter.PreFilterMessage(ref msg);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        // these following methods is used to follow Dispose Pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
            {
                if (_internalForm != null)
                {
                    _internalForm.Dispose();
                }

                if (_msgFilters != null)
                {
                    _msgFilters.Clear();
                }
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}