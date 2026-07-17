using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms; 

namespace yılan
{
    public partial class Form1 : Form
    {
        Yilan yilanim;
        Yem faydaliYem;
        Yem zararliYem;
        SerialPort arduinoPort;
        System.Windows.Forms.Timer oyunTimer;
        int skor = 0;
        bool oyunBasladi = false;
        int enIyiSkor = 0;
        Image menuGorseli = Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "anaekran.jpeg"));
        Image oyunGorseli = Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "oyunekrani.jpeg"));
        public Form1()
        {
            InitializeComponent();
            OyunuSifirla();

            arduinoPort = new SerialPort("COM5", 9600);
            arduinoPort.DataReceived += ArduinoVeriGeldi; 
            arduinoPort.Open();

            oyunTimer = new System.Windows.Forms.Timer();
            oyunTimer.Interval = 150;
            oyunTimer.Tick += OyunDongusu;
          
            this.DoubleBuffered = true;
            this.ClientSize = new Size(Yilan.IzgaraGenisligi * Yilan.KareBoyutu, Yilan.IzgaraYuksekligi * Yilan.KareBoyutu);
            this.TopMost = true;
        }

        private void ArduinoVeriGeldi(object sender, SerialDataReceivedEventArgs e)
        {
            string veri = arduinoPort.ReadLine().Trim();


            if (veri == "YUKARI" && yilanim.MevcutYon != Yon.Asagi) yilanim.MevcutYon = Yon.Yukari;
            if (veri == "ASAGI" && yilanim.MevcutYon != Yon.Yukari) yilanim.MevcutYon = Yon.Asagi;
            if (veri == "SOL" && yilanim.MevcutYon != Yon.Sag) yilanim.MevcutYon = Yon.Sol;
            if (veri == "SAG" && yilanim.MevcutYon != Yon.Sol) yilanim.MevcutYon = Yon.Sag;
            if (veri == "START" && oyunBasladi)
            {
                this.Invoke((MethodInvoker)delegate {
                    if (oyunTimer.Enabled) oyunTimer.Stop();
                    else oyunTimer.Start();
                });
                return;
            }

            if (veri == "START" && !oyunBasladi)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    OyunuSifirla();
                    oyunBasladi = true;
                    button1.Visible = false;
                    chkDuvarOlsun.Visible = false;
                    lblBaslangic.Visible = false;
                    lblEnIyiSkor.Visible = false;
                    oyunTimer.Start();
                    this.Invalidate();
                });
            }
            if (veri == "DUVAR_MOD_DEGISTIR")
            {
                this.Invoke((MethodInvoker)delegate
                {
                    chkDuvarOlsun.Checked = !chkDuvarOlsun.Checked;
                });
            }
        }
        private void OyunDongusu(object sender, EventArgs e)
        {
            yilanim.HareketEt(chkDuvarOlsun.Checked);

           
            Koordinat kafa = yilanim.Govde[0];
            if (chkDuvarOlsun.Checked)
            {
                if (kafa.X < 0 || kafa.X >= Yilan.IzgaraGenisligi || kafa.Y < 0 || kafa.Y >= Yilan.IzgaraYuksekligi)
                {
                    oyunTimer.Stop();
                    MessageBox.Show("Duvara çarptın! Oyun bitti.");
                    OyunBitti();
                    return;
                }
            }

            if (faydaliYem != null && kafa.AyniMi(faydaliYem.Konum))
            {
                yilanim.Buyu();      
                faydaliYem = null;   
                skor += 10; 

            }

            if (zararliYem != null && kafa.AyniMi(zararliYem.Konum))
            {
                oyunTimer.Stop();
                MessageBox.Show("Zehirli yemi yedin! Oyun bitti.");
                OyunBitti();
                return;
            }

            if (zararliYem != null && zararliYem.SuresiDolduMu())
            {
                zararliYem = null;
            }
            if (faydaliYem == null && zararliYem == null)
            {
                YeniYemOlustur();
            }

            for (int i = 1; i < yilanim.Govde.Count; i++)
            {
                if (kafa.AyniMi(yilanim.Govde[i]))
                {
                    oyunTimer.Stop();
                    MessageBox.Show("Kendini ısırdın! Oyun bitti.");
                    OyunBitti();
                    return;
                }
            }

            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if (!oyunBasladi)
            {
                if (menuGorseli != null)
                {
                    g.DrawImage(menuGorseli, 0, 0, this.Width, this.Height);
                }
                lblEnIyiSkor.Visible = true;
                button1.Visible = true;
                return;
            }

            if (oyunGorseli != null)
            {
                g.DrawImage(oyunGorseli, 0, 0, this.Width, this.Height);
            }
            lblEnIyiSkor.Visible = false;
            button1.Visible = false;

            g.FillRectangle(new SolidBrush(Color.FromArgb(120, Color.Black)), 0, 0, this.Width, this.Height);
        
            int kareBoyutu = Yilan.KareBoyutu;
            Pen izgaraKalemi = new Pen(Color.LightGray, 1);
            for (int x = 0; x <= Yilan.IzgaraGenisligi; x++)
            {
                g.DrawLine(izgaraKalemi, x * kareBoyutu, 0, x * kareBoyutu, Yilan.IzgaraYuksekligi * kareBoyutu);
            }

            for (int y = 0; y <= Yilan.IzgaraYuksekligi; y++)
            {
                g.DrawLine(izgaraKalemi, 0, y * kareBoyutu, Yilan.IzgaraGenisligi * kareBoyutu, y * kareBoyutu);
            }
            foreach (var parca in yilanim.Govde)
            {
                int x = parca.X * Yilan.KareBoyutu;
                int y = parca.Y * Yilan.KareBoyutu;

                if (parca == yilanim.Govde[0])
                {
                    Point[] uclari = new Point[3];
                    if (yilanim.MevcutYon == Yon.Yukari)
                    {
                        uclari[0] = new Point(x + Yilan.KareBoyutu / 2, y);
                        uclari[1] = new Point(x, y + Yilan.KareBoyutu);
                        uclari[2] = new Point(x + Yilan.KareBoyutu, y + Yilan.KareBoyutu);
                    }
                    else if (yilanim.MevcutYon == Yon.Asagi)
                    {
                        uclari[0] = new Point(x + Yilan.KareBoyutu / 2, y + Yilan.KareBoyutu);
                        uclari[1] = new Point(x, y);
                        uclari[2] = new Point(x + Yilan.KareBoyutu, y);
                    }
                    else if (yilanim.MevcutYon == Yon.Sag)
                    {
                        uclari[0] = new Point(x + Yilan.KareBoyutu, y + Yilan.KareBoyutu / 2);
                        uclari[1] = new Point(x, y);
                        uclari[2] = new Point(x, y + Yilan.KareBoyutu);
                    }
                    else if (yilanim.MevcutYon == Yon.Sol)
                    {
                        uclari[0] = new Point(x, y + Yilan.KareBoyutu / 2);
                        uclari[1] = new Point(x + Yilan.KareBoyutu, y);
                        uclari[2] = new Point(x + Yilan.KareBoyutu, y + Yilan.KareBoyutu);
                    }

              
                    g.FillPolygon(new SolidBrush(Color.Pink), uclari);
                    g.DrawPolygon(Pens.Purple, uclari);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.Pink), x, y, Yilan.KareBoyutu, Yilan.KareBoyutu);

                    g.DrawRectangle(Pens.Purple, x, y, Yilan.KareBoyutu, Yilan.KareBoyutu);
                }
            }

            if (faydaliYem != null)
            {
                g.FillEllipse(Brushes.Pink, faydaliYem.Konum.X * kareBoyutu, faydaliYem.Konum.Y * kareBoyutu, kareBoyutu, kareBoyutu);
            }

            if (zararliYem != null)
            {
                g.FillEllipse(Brushes.Red, zararliYem.Konum.X * kareBoyutu, zararliYem.Konum.Y * kareBoyutu, kareBoyutu, kareBoyutu);
            }
            g.DrawString("Skor: " + skor, new Font("Arial", 16, FontStyle.Regular), Brushes.IndianRed, 450, 5);
        }
      
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
             if (keyData == Keys.Enter && !oyunBasladi)
             {
                 OyunuSifirla();
                 oyunBasladi = true;
                 button1.Visible = false;
                 chkDuvarOlsun.Visible = false;
                 lblBaslangic.Visible = false;
                 lblEnIyiSkor.Visible = false;
                 oyunTimer.Start();
                 this.Invalidate();
             }
             if (keyData == Keys.Up && yilanim.MevcutYon != Yon.Asagi) yilanim.MevcutYon= Yon.Yukari;
             if (keyData == Keys.Down && yilanim.MevcutYon != Yon.Yukari) yilanim.MevcutYon = Yon.Asagi;
             if (keyData == Keys.Left && yilanim.MevcutYon != Yon.Sag) yilanim.MevcutYon = Yon.Sol;
             if (keyData == Keys.Right && yilanim.MevcutYon != Yon.Sol) yilanim.MevcutYon = Yon.Sag;
             return base.ProcessCmdKey(ref msg, keyData);
        }
        private void YeniYemOlustur()
        {
            Random rnd = new Random();

            int x = rnd.Next(0, Yilan.IzgaraGenisligi);
            int y = rnd.Next(0, Yilan.IzgaraYuksekligi);

            bool zararliMi = rnd.Next(0, 100) < 20;
            if (zararliMi)
            {
                zararliYem = new Yem(x, y, true);
                faydaliYem = null;
            }
            else
            {
                faydaliYem = new Yem(x, y, false);
                zararliYem = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OyunuSifirla();
            oyunBasladi = true;
            oyunTimer.Start();
            button1.Visible = false;
            lblBaslangic.Visible = false;
            chkDuvarOlsun.Visible = false;
        }
        private void OyunuSifirla()
        {
            yilanim = new Yilan(10, 10);
            skor = 0;
            YeniYemOlustur();
            this.Invalidate();
        }
        void OyunBitti()
        {
            oyunTimer.Stop();
            oyunBasladi = false;
            if (skor > enIyiSkor)
            {
                enIyiSkor = skor;
                lblEnIyiSkor.Text = "En İyi Skor: " + enIyiSkor;
            }

            MessageBox.Show("Oyun Bitti! Skorunuz: " + skor);

            button1.Visible = true;
            lblBaslangic.Visible = true;
            chkDuvarOlsun.Visible = true;
            lblEnIyiSkor.Visible = true;
            arduinoPort.Write("FINISH");


            this.Invalidate(); 
        }

        private void chkDuvarOlsun_CheckedChanged(object sender, EventArgs e)
        {

            if (chkDuvarOlsun.Checked)
            {
                chkDuvarOlsun.ForeColor = Color.Red;
                chkDuvarOlsun.Text = "Duvar Ölümü: AÇIK";
            }
            else
            {
                chkDuvarOlsun.ForeColor = Color.Green;
                chkDuvarOlsun.Text = "Duvar Ölümü: KAPALI";
            }
        }
      
    }
}
