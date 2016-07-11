/*   
      ListViewEx.cs (KIRSmartAV.Core)
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

using KIRSmartAV.Core.Native;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace KIRSmartAV.Core.Controls
{
    public class ListViewEx : ListView
    {
        private delegate void ChangeItemDelegate(int index, int imageIndex, string[] subitems);
        private delegate string[] GetListItemDelegate(int index);

        public ListViewEx()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != NativeConstants.WM_ERASEBKGND)
                base.OnNotifyMessage(m);
        }

        public void SafeAddItem(ListViewItem item)
        {
            if (this.InvokeRequired)
            {
                var addDelegate = new Action<ListViewItem>((ls) => SafeAddItem(ls));
                this.Invoke(addDelegate, new object[] { item });
            }
            else
            {
                this.Items.Add(item);
            }
        }

        public string[] SafeGetItem(int index)
        {
            if (this.InvokeRequired)
            {
                var getDelegate = new GetListItemDelegate((i) => SafeGetItem(i));
                return (string[])this.Invoke(getDelegate, new object[] { index });
            }
            else
            {
                var subitemsList = new List<string>();
                for (int i = 0; i < this.Items[index].SubItems.Count; i++)
                {
                    subitemsList.Add(this.Items[index].SubItems[i].Text);
                }
                return subitemsList.ToArray();
            }
        }

        public void SafeChangeItem(int index, int imageIndex = -1, string[] subitems = null)
        {
            if (this.InvokeRequired)
            {
                var getDelegate = new ChangeItemDelegate((i, m, k) => SafeChangeItem(i, m, k));
                this.Invoke(getDelegate, new object[] { index, imageIndex, subitems });
            }
            else
            {
                if (imageIndex != -1)
                    this.Items[index].ImageIndex = imageIndex;

                if (subitems != null)
                {
                    for (int i = 0; i < subitems.Length; i++)
                    {
                        this.Items[index].SubItems[i].Text = subitems[i];
                    } //End for loop
                } //End subitems logic
            } //End invoke logic
        }
    
    }
}
