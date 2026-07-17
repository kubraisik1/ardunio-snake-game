using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yılan
{
    public class Yilan
    {
        public const int IzgaraGenisligi = 20; //44
        public const int IzgaraYuksekligi = 20; //24
        public const int KareBoyutu = 30; //43
  
        public List<Koordinat> Govde { get; private set; }
        public Yon MevcutYon { get; set; }

        public Yilan(int baslangicX, int baslangicY)
        {
            Govde = new List<Koordinat>();

            Govde.Add(new Koordinat(baslangicX, baslangicY));    
            Govde.Add(new Koordinat(baslangicX - 1, baslangicY)); 
            Govde.Add(new Koordinat(baslangicX - 2, baslangicY));

            MevcutYon = Yon.Sag;
        }

        public void HareketEt(bool duvarOlumuAktif)
        {
            Koordinat bas = Govde.First();
            int yeniX = bas.X;
            int yeniY = bas.Y;

            switch (MevcutYon)
            {
                case Yon.Yukari: yeniY--; break;
                case Yon.Asagi: yeniY++; break;
                case Yon.Sol: yeniX--; break;
                case Yon.Sag: yeniX++; break;
            }

            if (!duvarOlumuAktif)
            {
                if (yeniX >= IzgaraGenisligi) 
                yeniX = 0; 
                  
                else if (yeniX < 0) yeniX = IzgaraGenisligi - 1;

                if (yeniY >= IzgaraYuksekligi) yeniY = 0;
                else if (yeniY < 0) yeniY = IzgaraYuksekligi - 1;
            }
           
            Govde.Insert(0, new Koordinat(yeniX, yeniY));
            Govde.RemoveAt(Govde.Count - 1);
        }

        public void Buyu()
        {
            Koordinat kuyruk = Govde.Last();
            Govde.Add(new Koordinat(kuyruk.X, kuyruk.Y));
        }
    }
}
