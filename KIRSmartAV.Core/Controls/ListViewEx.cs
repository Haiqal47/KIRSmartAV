/*   
  ListViewEx.cs (KIRSmartAV.Core)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
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
