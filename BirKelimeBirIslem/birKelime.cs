using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace yazilimYapimi
{
    public partial class birKelime : Form
    {
        public birKelime()
        {
            InitializeComponent();
        }

        private void rastgeleHarfYapici(object sender, EventArgs e)
        {
            //listbox temizliyor
            listBox1.Items.Clear();
            Random rnd = new Random();
            //alfabeden seçilen rastgele harfleri textboxlara ekliyor
            textBox1.Text = abc[rnd.Next(0, 29)].ToString();
            textBox2.Text = abc[rnd.Next(0, 29)].ToString();
            textBox3.Text = abc[rnd.Next(0, 29)].ToString();
            textBox4.Text = abc[rnd.Next(0, 29)].ToString();
            textBox5.Text = abc[rnd.Next(0, 29)].ToString();
            textBox6.Text = abc[rnd.Next(0, 29)].ToString();
            textBox7.Text = abc[rnd.Next(0, 29)].ToString();
            textBox8.Text = abc[rnd.Next(0, 29)].ToString();
        }

        private void kelimeBul(object sender, EventArgs e)
        {
            //listbox temizliyor
            listBox1.Items.Clear();
            //textboxlardan biri boşsa uyarı veriyor ve doldurulması bekleniyor
            if (textBox1.Text.Length < 1 || textBox2.Text.Length < 1 || textBox3.Text.Length < 1 || textBox4.Text.Length < 1 || textBox5.Text.Length < 1 || textBox6.Text.Length < 1 || textBox7.Text.Length < 1 || textBox8.Text.Length < 1)
            {
                MessageBox.Show("Lütfen boş yer bırakmayınız");
                return;
            }
            //textboxtaki harfleri bir stringe atıyor
            string str = textBox1.Text + textBox2.Text + textBox3.Text + textBox4.Text + textBox5.Text + textBox6.Text + textBox7.Text + textBox8.Text;
            //joker harfte dahil etmek için kullanılıyor
            jokerliArama(str);
        }

        string abc = "abcçdefgğhıijklmnoöprsştvuüyz";

        private void jokerliArama(string str)
        {
            //kelime listesini txtden programa (ram e) yüklüyor
            List<string> kelimeListesi = new List<string>(File.ReadAllText("liste.txt").Split('\n'));
            string uzunKelime = "", kelime = "";
            char jokerHarf = ' ';
            //joker harf herhangi bir harf olabildiği için alfabediki harf sayısı kadar dönüyor
            for (int i = 0; i < abc.Count(); i++)
            {
                kelime = harfKelimeArama(abc[i], str, kelimeListesi);
                //donen kelimenin karakter sayısı uzunKelimeden daha uzunsa kelimeyi uzunKelimeye atıyor
                if (kelime.Count() > uzunKelime.Count())
                {
                    uzunKelime = kelime;
                    jokerHarf = abc[i];
                }
            }
            //uzunKelime 2 karakterden fazla ise listboxa ekleniyor
            if (uzunKelime.Count() > 2)
            {
                listBox1.Items.Add(uzunKelime);
                genelPuanHesaplayici(puanBul(uzunKelime.Count()));
                //uzunKelimedeki bulunan joker harf sayısı textboxlardaki olan harflerin içinde bulunan joker harf sayısından fazla ise joker harf kullanıldığı anlaşılıyor bu yüzden kullanılan joker harf yazılıyor
                if (uzunKelime.ToLower().Count(x => x == jokerHarf) > str.ToLower().Count(x => x == jokerHarf))
                {
                    listBox1.Items.Add("Joker Harf: " + jokerHarf);
                }
            }
            //değilse kelime bulunmadı diye messagebox gosteriliyor
            else
            {
                MessageBox.Show("Kelime bulunamadı");
            }
        }
        //kelime bulmak için kullanılan metod
        private string harfKelimeArama(char joker, string harfler, List<string> kelimeList)
        {
            //textboxtaki harflere joker harfi ekliyor
            harfler += joker;
            //bütün kelime listesi kadar dönüyor ve her donüşte kelime listesindeki elemanları sırasıyla kelime değişkenine atıyor
            foreach (string kelime in kelimeList)
            {
                //kelime değişkenin içindeki textboxtlardaki olan ortak karakterleri çıkarıyor ve k
                if ((kelime.ToLower().Except(harfler.ToLower()).Count() == 0))
                {
                    bool harfSayiKontrol = true;
                    //yukardaki .except metodu kelime değişkeninde ortak bütün harfleri çıkardığı için kelime değişkenindeki karakter sayısının textboxlardaki karakter sayısından fazla olmadığını kontrol ediyor
                    for (int i = 0; i < harfler.Count(); i++)
                    {
                        if (kelime.ToLower().Count(chr => chr == harfler[i]) > harfler.ToLower().Count(chr => chr == harfler[i]))
                        {
                            harfSayiKontrol = false;
                        }
                    }
                    //eğer word içindeki karakter sayıları eşit yada daha küçük ise kelime döndürülüyor
                    if (harfSayiKontrol)
                    {
                        return kelime;
                    }
                }
            }
            //kelime bulamazsa boş string döndürülüyor
            return "";
        }

        private int puanBul(int harfSayisi)
        {
            //bulunan kelimenin harf sayısına göre puanlamanın sonucu döndürülüyor
            switch (harfSayisi)
            {
                case 3:
                    return 3;
                case 4:
                    return 4;
                case 5:
                    return 5;
                case 6:
                    return 7;
                case 7:
                    return 9;
                case 8:
                    return 11;
                case 9:
                    return 15;
            }
            return 0;
        }
        //burada anaForma erişip puanı değitiriyor
        private void genelPuanHesaplayici(int kazanPuan)
        {
            listBox1.Items.Add("Kazanılan puan = " + kazanPuan);
            int genelPuan = ((anaForm)this.ParentForm).puanDöndürücü();
            genelPuan += kazanPuan;
            ((anaForm)this.ParentForm).puanDeğistirici(genelPuan);
        }
        //burada textboxlara sadece küçük harf girmeyi sağlıyor
        private void sadeceHarf(object sender, KeyPressEventArgs e)
        {
            e.Handled = !abc.Contains(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}