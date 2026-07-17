using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yılan
{
    public class Koordinat
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Koordinat(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Çarpışma kontrolü için
        public bool AyniMi(Koordinat digerKoordinat)
        {
            return this.X == digerKoordinat.X && this.Y == digerKoordinat.Y;
        }
    }
}
