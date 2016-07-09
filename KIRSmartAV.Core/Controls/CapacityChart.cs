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
        private bool _shouldInvalidate = false;

        public void DrawPie(long totalSpace, long freeSpace)
        {
            _chartPie = 360.0F * freeSpace / totalSpace;
            this.Invalidate();
            _shouldInvalidate = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_shouldInvalidate)
            {
                var G = e.Graphics;

                var Frame = new Rectangle(1, 1, this.Width - 1, this.Height - 1);
                G.DrawRectangle(new Pen(BackColor), Frame);

                var GraphicChart = new Rectangle(5, 5, this.Width - 10, this.Height - 10);
                G.FillPie(Brushes.Blue, GraphicChart, _chartPie, 360 - _chartPie);
                G.FillPie(Brushes.Magenta, GraphicChart, 0, _chartPie);

                _shouldInvalidate = false;
            }

            base.OnPaint(e);
        }
    }
}
