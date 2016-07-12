/*   
      CapacityChart.cs (KIRSmartAV.Core)
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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KIRSmartAV.Core.Controls
{
    public class CapacityChart : Control
    {
        private float _chartPie = 0F;

        public void DrawPie(long totalSpace, long freeSpace)
        {
            _chartPie = 360.0F * freeSpace / totalSpace;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.Clear(BackColor);

            var Frame = new Rectangle(1, 1, this.Width - 1, this.Height - 1);
            G.DrawRectangle(new Pen(BackColor), Frame);

            var GraphicChart = new Rectangle(5, 5, this.Width - 10, this.Height - 10);
            G.FillPie(Brushes.Blue, GraphicChart, _chartPie, 360 - _chartPie);
            G.FillPie(Brushes.Magenta, GraphicChart, 0, _chartPie);

            base.OnPaint(e);
        }
    }
}
