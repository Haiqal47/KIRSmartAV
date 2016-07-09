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
