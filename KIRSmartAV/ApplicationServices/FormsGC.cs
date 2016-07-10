/*   
  FormsGC.cs (KIRSmartAV)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace KIRSmartAV.ApplicationServices
{
    public class FormsGC
    {
        private List<Form> _currentViews = null;
        private static LogManager _logger = LogManager.GetClassLogger();
        private static FormsGC _default = new FormsGC();

        public static FormsGC Default
        {
            get { return _default; }
        }

        private FormsGC()
        {
            _currentViews = new List<Form>();
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
    }
}
