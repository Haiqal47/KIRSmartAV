/*   
      WndProcOverrider.cs (KIRSmartAV)
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
using System.Text;
using System.Windows.Forms;

namespace KIRSmartAV.ApplicationServices
{
    [DesignerCategory("Code")]
    public class WndProcWatcher : Form
    {
        public event EventHandler<WndProcArrived> ProcArrived;

        public WndProcWatcher()
        {
            Size = new System.Drawing.Size(1, 1);
            FormBorderStyle = FormBorderStyle.None;
            //Visible = false;
            ShowInTaskbar = false;
            Opacity = 0;

            this.Load += KcavMessagePump_Load;

            Show();
        }

        protected override void WndProc(ref Message m)
        {
            // handle original messages
            base.WndProc(ref m);

            // invoke event
            var handler = ProcArrived;
            if (handler != null)
            {
                handler(this, new WndProcArrived() { Msg = m });
            }
        }

        private void KcavMessagePump_Load(object sender, EventArgs e)
        {
            this.Hide();
        }

        public class WndProcArrived : EventArgs
        {
            public Message Msg { get; set; }
        }
    }
}
