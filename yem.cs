using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yılan
{
    public class Yem
    {
        public Koordinat Konum { get; set; }
        public Color Renk { get; set; }
        public bool ZararliMi { get; set; }
        public DateTime OlusturulmaZamani { get; set; }
        public int OmurSaniyesi { get; set; } = 5;

        public Yem(int x, int y, bool zararliMi)
        {
            Konum = new Koordinat(x, y);
            ZararliMi = zararliMi;
            OlusturulmaZamani = DateTime.Now;

            if (ZararliMi)
            {
                Renk = Color.Red;
            }
            else
            {
                Renk = Color.Pink; 
            }
        }

        public bool SuresiDolduMu()
        {
            if (!ZararliMi) return false; 

            return (DateTime.Now - OlusturulmaZamani).TotalSeconds > OmurSaniyesi;
        }
    }
}
