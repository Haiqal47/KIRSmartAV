﻿/*   
      FormsGC.cs (KIRSmartAV)
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
using System.Text;
using System.Windows.Forms;

namespace KIRSmartAV.ApplicationServices
{
    public class FormsGC : IDisposable
    {
        private List<Form> _currentViews = null;
        private static LogManager _logger = LogManager.GetClassLogger();

        // constructor
        public FormsGC()
        {
            // prepare views
            _currentViews = new List<Form>();
        }

        public bool HasShownForm
        {
            get { return _currentViews.Count > 0; }
        }

        public void ShowForm(Form frm)
        {
            if (frm == null)
            {
                throw new ArgumentNullException("frm");
            }
            
            _currentViews.Add(frm);
            frm.FormClosed += ViewForm_FormClosed;
            _logger.Debug("Showing form. \"" + frm.GetType().Name + "\"");
            frm.Show();
        }

        private void ViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_currentViews != null && _currentViews.Count > 0)
            {
                var currentForm = (Form)sender;
                currentForm.FormClosed -= ViewForm_FormClosed;
                currentForm.Dispose();
                _currentViews.Remove(currentForm);
                _logger.Debug("Disposing opened form. \"" + currentForm.GetType().Name + "\"");
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
            {
                if (_currentViews != null && _currentViews.Count > 0)
                {
                    // close all loaded forms
                    foreach (Form frm in _currentViews)
                    {
                        frm.Close();
                    }

                    // clear all remaining items
                    _currentViews.Clear();
                }
            }

            disposedValue = true;
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
