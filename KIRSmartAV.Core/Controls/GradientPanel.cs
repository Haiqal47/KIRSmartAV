/*   
      GradientPanel.cs (KIRSmartAV.Core)
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
