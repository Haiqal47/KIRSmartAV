/*   
  GradientPanel.cs (KIRSmartAV.Core)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
*/
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KIRSmartAV.Core.Controls
{
    public class GradientPanel : Panel
    {
        public Color GradientColor1 { get; set; } = Color.FromArgb(35, 168, 109);
        public Color GradientColor2 { get; set; } = Color.White;
        public LinearGradientMode GradientMode { get; set; } = LinearGradientMode.Horizontal;

        public GradientPanel()
        {
            var styles = ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.ResizeRedraw;
            SetStyle(styles, true);
            DoubleBuffered = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if ((GradientColor1 == null) || (GradientColor2 == null))
                return;

            var G = e.Graphics;
            G.Clear(BackColor);

            var rect = new Rectangle(0, 0, this.Width, this.Height);
            using (var gradBursh = new LinearGradientBrush(rect, GradientColor1, GradientColor2, GradientMode))
            {
                G.FillRectangle(gradBursh, rect);
            }
        }
    }
}
