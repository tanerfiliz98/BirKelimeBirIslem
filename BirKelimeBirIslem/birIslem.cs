using System;
using System.Windows.Forms;

namespace yazilimYapimi
{
    public partial class birIslem : Form
    {
        public birIslem()
        {
            InitializeComponent();
        }

        private void randomSayiVer(object sender, EventArgs e)
        {
            //listbox temizliyor
            listBox1.Items.Clear();
            Random rnd = new Random();
            // 1 den 9 a kadar rastgele sayılar veriliyor
            textBox1.Text = rnd.Next(1, 10).ToString();
            textBox2.Text = rnd.Next(1, 10).ToString();
            textBox3.Text = rnd.Next(1, 10).ToString();
            textBox4.Text = rnd.Next(1, 10).ToString();
            textBox5.Text = rnd.Next(1, 10).ToString();
            //onun katları olan iki haneli sayi için 1 den 9 a kadar rastgele sayı verilip 10 ile çarpılıyor
            textBox6.Text = ((rnd.Next(1, 10)) * 10).ToString();
            //üç haneli sayı rastgele sayı veriliyor
            textBox7.Text = rnd.Next(100, 1000).ToString();
        }

        private void numaraAl(decimal[] numara)
        {
            //textboxtaki sayıları bir diziye atılıyor
            //işlem yapılacak sayilar
            numara[0] = Convert.ToDecimal(textBox1.Text);
            numara[1] = Convert.ToDecimal(textBox2.Text);
            numara[2] = Convert.ToDecimal(textBox3.Text);
            numara[3] = Convert.ToDecimal(textBox4.Text);
            numara[4] = Convert.ToDecimal(textBox5.Text);
            numara[5] = Convert.ToDecimal(textBox6.Text);
            //istenilen sayi
            numara[6] = Convert.ToDecimal(textBox7.Text);
        }

        private decimal hesapla(decimal numara1, decimal numara2, int operatorChar)
        {
            //fonksiyona verilen iki sayı istenilen işleme göre sonucu dönüyor
            switch (operatorChar)
            {
                case 0:
                    return numara1 + numara2;
                case 1:
                    return numara1 - numara2;
                case 2:
                    return numara1 / numara2;
                case 3:
                    return numara1 * numara2;
            }
            return 0;
        }

        private void islemYapilacakNumaraBul(decimal[] dnckNumara, int[] dnckIslem)
        {
            decimal[] numara = new decimal[7];
            numaraAl(numara);//textboxtaki sayılar diziye atılıyor
            decimal[] sonucTutucu = new decimal[6];
            int numaraSayisi = 6;
            int operatorSayisi = 4;
            int cezaPuani = 0;
            //9 yaklaşığa kadar dönüyor
            while (cezaPuani < 10)
            {
                //dizideki numaralardan birini seçmek için for dönüyor
                for (int numaraI1 = 0; numaraI1 < numaraSayisi; numaraI1++)
                {
                    //numaraI1 indisli numara ilk sonuca yazılıyor
                    sonucTutucu[0] = numara[numaraI1];
                    //ilk yapılacak işlem için for dönüyor
                    for (int operatorI1 = 0; operatorI1 < operatorSayisi; operatorI1++)
                    {
                        //ikinci numarayı seçmek için for
                        for (int numaraI2 = 0; numaraI2 < numaraSayisi; numaraI2++)
                        {
                            //seçilen sayılar aynı textboxtaki olması halinde aşağıdakiler yapılmayıp sayaç artırılarak farklı numara seçilmesi sağlanıyor
                            //(yani verilen sayıların sadece bir defa kullanıması için)
                            if (numaraI2 == numaraI1)
                            {
                                continue;
                            }
                            //ilk seçilen sayı ile ikinci arasında operatorI ya gore işlem yapılıyor
                            sonucTutucu[1] = hesapla(sonucTutucu[0], numara[numaraI2], operatorI1);
                            //eğer dönen sonuç istenilen sayıya ulaşmışsa duruyor ve sayılar döndürülmek için diziye atanıyor
                            if (sonucTutucu[1] == (numara[6] - cezaPuani) || sonucTutucu[1] == (numara[6] + cezaPuani))
                            {
                                dnckNumara[0] = numara[numaraI1];
                                dnckIslem[0] = operatorI1; dnckNumara[1] = numara[numaraI2];
                                dnckIslem[5] = cezaPuani; dnckIslem[6] = 1;
                                return;
                            }
                            //ikinci işlemi seçmek için for
                            for (int operatorI2 = 0; operatorI2 < operatorSayisi; operatorI2++)
                            {
                                //üçüncü sayıyı seçmek için for
                                for (int numaraI3 = 0; numaraI3 < numaraSayisi; numaraI3++)
                                {
                                    //seçilen sayılar aynı textboxtaki olması halinde aşağıdakiler yapılmayıp sayaç artırılarak farklı numara seçilmesi sağlanıyor
                                    //(yani verilen sayıların sadece bir defa kullanıması için)
                                    if (numaraI3 == numaraI1 || numaraI3 == numaraI2)
                                    {
                                        continue;
                                    }
                                    //yukarıda yapılan işlemin sonucuyla üçüncü sayıyla işleme sokuyor
                                    //yani örneğin (1.numara+2.numara)+3.numara
                                    sonucTutucu[2] = hesapla(sonucTutucu[1], numara[numaraI3], operatorI2);
                                    //eğer dönen sonuç istenilen sayıya ulaşmışsa duruyor ve sayılar döndürülmek için diziye atanıyor
                                    if (sonucTutucu[2] == (numara[6] - cezaPuani) || sonucTutucu[2] == (numara[6] + cezaPuani))
                                    {
                                        dnckNumara[0] = numara[numaraI1];
                                        dnckIslem[0] = operatorI1; dnckNumara[1] = numara[numaraI2];
                                        dnckIslem[1] = operatorI2; dnckNumara[2] = numara[numaraI3];
                                        dnckIslem[5] = cezaPuani; dnckIslem[6] = 2;
                                        return;
                                    }
                                    //üçüncü işlemi seçmek için for
                                    for (int operatorI3 = 0; operatorI3 < operatorSayisi; operatorI3++)
                                    {
                                        //dördüncü sayıyı seçmek için for
                                        for (int numaraI4 = 0; numaraI4 < numaraSayisi; numaraI4++)
                                        {
                                            //seçilen sayılar aynı textboxtaki olması halinde aşağıdakiler yapılmayıp sayaç artırılarak farklı numara seçilmesi sağlanıyor
                                            //(yani verilen sayıların sadece bir defa kullanıması için)
                                            if (numaraI4 == numaraI1 || numaraI4 == numaraI2 || numaraI4 == numaraI3)
                                            {
                                                continue;
                                            }
                                            //yukarıda yapılan işlemin sonucuyla dördüncü sayıyla işleme sokuyor
                                            //yani örneğin ((1.numara+2.numara)+3.numara)+4.numara
                                            sonucTutucu[3] = hesapla(sonucTutucu[2], numara[numaraI4], operatorI3);
                                            //eğer dönen sonuç istenilen sayıya ulaşmışsa duruyor ve sayılar döndürülmek için diziye atanıyor
                                            if (sonucTutucu[3] == numara[6] - cezaPuani || sonucTutucu[3] == numara[6] + cezaPuani)
                                            {
                                                dnckNumara[0] = numara[numaraI1];
                                                dnckIslem[0] = operatorI1; dnckNumara[1] = numara[numaraI2];
                                                dnckIslem[1] = operatorI2; dnckNumara[2] = numara[numaraI3];
                                                dnckIslem[2] = operatorI3; dnckNumara[3] = numara[numaraI4];
                                                dnckIslem[5] = cezaPuani; dnckIslem[6] = 3;
                                                return;
                                            }
                                            //dördüncü işlemi seçmek için for
                                            for (int operatorI4 = 0; operatorI4 < operatorSayisi; operatorI4++)
                                            {
                                                //beşinci sayıyı seçmek için for
                                                for (int numaraI5 = 0; numaraI5 < numaraSayisi; numaraI5++)
                                                {
                                                    //seçilen sayılar aynı textboxtaki olması halinde aşağıdakiler yapılmayıp sayaç artırılarak farklı numara seçilmesi sağlanıyor
                                                    //(yani verilen sayıların sadece bir defa kullanıması için)
                                                    if (numaraI5 == numaraI1 || numaraI5 == numaraI2 || numaraI5 == numaraI3 || numaraI5 == numaraI4)
                                                    {
                                                        continue;
                                                    }
                                                    //yukarıda yapılan işlemin sonucuyla beşinci sayıyla işleme sokuyor
                                                    //yani örneğin (((1.numara+2.numara)+3.numara)+4.numara)+5.numara.
                                                    //birde aşağıdaki numara foru sonuca ulaşmazsa sayıyı değiştirmeye çalışıyor.o da olmazsa işlemi değiştiriyor yani (((1.numara+2.numara)+3.numara)+4.numara)-5.numara gibi
                                                    sonucTutucu[4] = hesapla(sonucTutucu[3], numara[numaraI5], operatorI4);
                                                    //eğer dönen sonuç istenilen sayıya ulaşmışsa duruyor ve sayılar döndürülmek için diziye atanıyor
                                                    if (sonucTutucu[4] == numara[6] - cezaPuani || sonucTutucu[4] == numara[6] + cezaPuani)
                                                    {
                                                        dnckNumara[0] = numara[numaraI1];
                                                        dnckIslem[0] = operatorI1; dnckNumara[1] = numara[numaraI2];
                                                        dnckIslem[1] = operatorI2; dnckNumara[2] = numara[numaraI3];
                                                        dnckIslem[2] = operatorI3; dnckNumara[3] = numara[numaraI4];
                                                        dnckIslem[3] = operatorI4; dnckNumara[4] = numara[numaraI5];
                                                        dnckIslem[5] = cezaPuani; dnckIslem[6] = 4;
                                                        return;
                                                    }
                                                    //beşinci işlemi seçmek için for
                                                    for (int operatorI5 = 0; operatorI5 < operatorSayisi; operatorI5++)
                                                    {
                                                        //altıncı sayıyı seçmek için for
                                                        for (int numaraI6 = 0; numaraI6 < numaraSayisi; numaraI6++)
                                                        {
                                                            //seçilen sayılar aynı textboxtaki olması halinde aşağıdakiler yapılmayıp sayaç artırılarak farklı numara seçilmesi sağlanıyor
                                                            //(yani verilen sayıların sadece bir defa kullanıması için)
                                                            if (numaraI6 == numaraI1 || numaraI6 == numaraI2 || numaraI6 == numaraI3 || numaraI6 == numaraI4 || numaraI6 == numaraI5)
                                                            {
                                                                continue;
                                                            }
                                                            //yukarıda yapılan işlemin sonucuyla altıncı sayıyla işleme sokuyor
                                                            //yani örneğin ((((1.numara+2.numara)+3.numara)+4.numara)+5.numara)+6.numara)
                                                            sonucTutucu[5] = hesapla(sonucTutucu[4], numara[numaraI6], operatorI5);
                                                            //eğer dönen sonuç istenilen sayıya ulaşmışsa duruyor ve sayılar döndürülmek için diziye atanıyor
                                                            if (sonucTutucu[5] == numara[6] - cezaPuani || sonucTutucu[5] == numara[6] + cezaPuani)
                                                            {
                                                                dnckNumara[0] = numara[numaraI1];
                                                                dnckIslem[0] = operatorI1; dnckNumara[1] = numara[numaraI2];
                                                                dnckIslem[1] = operatorI2; dnckNumara[2] = numara[numaraI3];
                                                                dnckIslem[2] = operatorI3; dnckNumara[3] = numara[numaraI4];
                                                                dnckIslem[3] = operatorI4; dnckNumara[4] = numara[numaraI5];
                                                                dnckIslem[4] = operatorI5; dnckNumara[5] = numara[numaraI6];
                                                                dnckIslem[5] = cezaPuani; dnckIslem[6] = 5;
                                                                return;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //bütün yan yana yazılabilecek ikili kombinasyonları deneyerek olmazsa ceza puanını artırarak istenilen sonuca göre +/- ceza puanına göre sonucu bulmaya çalışıyor
                cezaPuani++;
            }
            //9 yaklaşığa kadar bulamazsa sadece ceza puanını döndürüyor
            if (cezaPuani > 9)
            {
                dnckIslem[5] = cezaPuani;
                return;
            }
        }
        // istenilen yerde 0 dan 3 e kadar verilen sayıya göre programda kullanılan operatorun char karşılğını döndürüyor
        private char operatorKarakterVer(int operatorChar)
        {
            switch (operatorChar)
            {
                case 0:
                    return '+';
                case 1:
                    return '-';
                case 2:
                    return '/';
                case 3:
                    return 'x';
            }
            return ' ';
        }

        private void sonucGoster(object sender, EventArgs e)
        {
            //listboxi temizliyor
            listBox1.Items.Clear();
            //textboxlardan biri boşsa uyarı veriyor ve doldurulması bekleniyor
            if (textBox1.Text.Length < 1 || textBox2.Text.Length < 1 || textBox3.Text.Length < 1 || textBox4.Text.Length < 1 || textBox5.Text.Length < 1 || textBox6.Text.Length < 1 || textBox7.Text.Length < 1)
            {
                MessageBox.Show("Lütfen boş yer bırakmayınız");
                return;
            }
            decimal[] donenNumara = new decimal[6];
            int[] donenIslem = new int[7];
            islemYapilacakNumaraBul(donenNumara, donenIslem);
            // ceza puanı 10 dan büyükse aşağıdaki messagebox gösteriliyor
            if (donenIslem[5] > 9)
            {
                MessageBox.Show("Yaklaşık değeri 10u geçti");
            }
            else
            {
                //ilk dönen numarayı sonuca yazıyor
                decimal sonuc = donenNumara[0];
                string islemSatir = sonuc.ToString();
                for (int i = 0; i < donenIslem[6]; i++)
                {
                    //çarpmada 1 etkisiz olduğu için dahil etmiyor
                    if ((donenIslem[i] == 2 || donenIslem[i] == 3) && donenNumara[i + 1] == 1)
                    {
                        continue;
                    }
                    //satıra operator türünü ekliyor
                    islemSatir += (" " + operatorKarakterVer(donenIslem[i]) + " ");
                    //onceki sonuç ile işlem yapılacak numarayı ekliyor
                    islemSatir += donenNumara[i + 1];
                    //onceki sonuç ile numarayı işlem yaptırıyor
                    sonuc = hesapla(sonuc, donenNumara[i + 1], donenIslem[i]);
                    //sonucu yazdırıyor
                    islemSatir += " = " + string.Format("{0:0.###########}", sonuc);
                    //islem satırını listboxa ekliyor
                    listBox1.Items.Add(islemSatir);
                    //olusan sonucu yeni satırın başı olarak atıyor
                    islemSatir = string.Format("{0:0.###########}", sonuc);
                }
                //eğer tam şekilde bulunmamışsa kaç yaklaşık olduğunu listboxın sonuna ekliyor
                if (donenIslem[5] > 0)
                {
                    listBox1.Items.Add(donenIslem[5].ToString() + " Yaklaşık");
                }
                genelPuanHesaplayici(10 - donenIslem[5]);
            }

        }
        //burada anaForma erişip puanı değitiriyor
        private void genelPuanHesaplayici(int kazanPuan)
        {
            listBox1.Items.Add("Kazanılan puan = " + kazanPuan);
            int genelPuan = ((anaForm)this.ParentForm).puanDöndürücü();
            genelPuan += kazanPuan;
            ((anaForm)this.ParentForm).puanDeğistirici(genelPuan);
        }
        //burada textboxlara sadece sayi girmeyi sağlıyor
        private void sadeceSayi(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}