using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgiGüvenliğiKriptolojiFinalProje
{
    public partial class Form1 : Form
    {
       
   public Form1()
        {
            InitializeComponent();
        }
       
        static int UstelMod(int a, double b, int n)
        {
            int _a = a % n;
            double _b = b;
            if (b == 0)
            {
                return 1;
            }
            while (_b > 1)
            {
                _a *= a;
                _a %= n;
                _b--;
            }
            return _a;
        }
        static double DBelirle(int phi, int e)
        {
            
            double d;
            for (int i = 0; i <1000000; i++)
            {
                
                d = (double)(1 + i * phi) / (double)e;
                double hesapla = d - ((int)d);
                if (hesapla == 0)
                {
                        return d;
                }
            }
            return 0;

        }
        static int OBEB(int x, int y)
        {
            int min = Math.Min(x, y);
            int obeb = 1;
            for (int i = 2; i <= min; i++)
            {
                if (x % i == 0 && y % i == 0)
                {
                    obeb = i;
                }

            }
            return obeb;
        }
        static string RSASifre(string metin, int n, int e)
        {
            char[] chars = metin.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                builder.Append(Convert.ToChar(UstelMod(chars[i], e, n)));
            }
            return builder.ToString();
        }
        static string RSADeSifre(string metin2, int m, double d)
        {
            char[] chars2 = metin2.ToCharArray();
            StringBuilder builder2 = new StringBuilder();
            for (int y = 0; y < chars2.Length; y++)
            {
                builder2.Append(Convert.ToChar(UstelMod(chars2[y], d, m)));
            }
            return builder2.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int m = Convert.ToInt32(textBox5.Text)*Convert.ToInt32(textBox6.Text);
            int b = Convert.ToInt32(textBox1.Text);
            textBox2.Text = RSASifre("4", m, b);
            int k = (Convert.ToInt32(textBox5.Text) - 1) * (Convert.ToInt32(textBox6.Text) - 1);
            double x = DBelirle(k, b);
            textBox3.Text = x.ToString();
            textBox4.Text = RSADeSifre(textBox2.Text, m, x);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int üretilen = Convert.ToInt32(textBox5.Text);
            int üretilen2 = Convert.ToInt32(textBox6.Text);


            int n = üretilen * üretilen2;
            int phi = (üretilen - 1) * (üretilen2 - 1);


            for (int i = 2; i < phi; i++)
            {

                if (OBEB(phi, i) == 1)
                {
                    listBox1.Items.Add(i);
                }



            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text = textBox7.Text;
            double karaktersayisi = 0;
            double yuvarlanmissayi;
            Random random = new Random();
            for (int i = 0; i < text.Length; i++)
            {
               
                    karaktersayisi++;
           
            }
            double kareköksayi = Math.Sqrt(karaktersayisi);
            double sıfırkontrol = kareköksayi - (int)kareköksayi;
            if (sıfırkontrol == 0)
            {
                yuvarlanmissayi = kareköksayi;
            }
            else
            {
                yuvarlanmissayi = Math.Ceiling(kareköksayi);
            }
            char[,] dizi = new char[(int)yuvarlanmissayi, (int)yuvarlanmissayi];
            char[] karakterr = new char[(int)karaktersayisi];
            int b = 0;
            int c = 0;
            for (int i = 0; i < text.Length; i++)
            {
                    b++;
                    karakterr[c] = text[b - 1];
                    c++;

            }
            int a = 0;
            for (int i = 0; i < yuvarlanmissayi; i++)
            {
                for (int y = 0; y < yuvarlanmissayi; y++)
                {
                    if (karaktersayisi > a)
                    {
                        dizi[i, y] = karakterr[a];
                        a++;
                    }
                    else
                    {
                        int rastgele = random.Next(65, 91);
                        char karakter = Convert.ToChar(rastgele);
                        dizi[i, y] = karakter;
                    }

                }
            }
            
            for (int i = 0; i < yuvarlanmissayi; i++)
            {
                for (int y = 0; y < yuvarlanmissayi; y++)
                {
                    listBox2.Items.Add(dizi[y, i]);
                    textBox8.Text += dizi[y, i].ToString();
                   
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int a = 0;
            char[,] dizi2 = new char[Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox4.Text)];
            for (int i = 0; i < Convert.ToInt32(textBox4.Text); i++)
            {
                for (int y = 0; y < Convert.ToInt32(textBox4.Text); y++)
                {
                    
                    dizi2[i, y] = (char)listBox2.Items[a];
                    a++;
                   
                }
                
            }
            int b = 0;
            for (int i = 0; i < Convert.ToInt32(textBox4.Text); i++)
            {
                for (int y = 0; y < Convert.ToInt32(textBox4.Text); y++)
                {
                    if(textBox7.Text.Length>b)
                    textBox9.Text += dizi2[y, i].ToString();
                    b++;

                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox7.Text.Length < 20)
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                byte[] Hash = new byte[20];
                for (int i = 0; i < textBox7.Text.Length; i++)
                {
                    Hash[i] = (byte)textBox7.Text[i];
                }
                RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
                RSAFormatter.SetHashAlgorithm("SHA1");
                byte[] Hash2 = new byte[20];
                for (int i = 0; i < textBox9.Text.Length; i++)
                {
                    Hash2[i] = (byte)textBox9.Text[i];
                }

                byte[] SignedHash = RSAFormatter.CreateSignature(Hash2);
                RSAPKCS1SignatureDeformatter signatureDeformatter = new RSAPKCS1SignatureDeformatter(RSA);
                signatureDeformatter.SetHashAlgorithm("SHA1");
                if (signatureDeformatter.VerifySignature(Hash, SignedHash)) { MessageBox.Show("İmza Doğrulandı."); } else { MessageBox.Show("İmza Doğrulanmadı."); }

            }
            else
            {
                int uzunluk = textBox7.Text.Length;
                int kacTaneVar=1;
                while (uzunluk >20)
                {
                    uzunluk= uzunluk - 20;
                    kacTaneVar++;

                }
                for (int i = 0; i <kacTaneVar; i++)
                {
                        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                        byte[] Hash = new byte[20];
                        for (int m = 0; m <20; m++)
                        {
                        if((textBox7.Text.Length-(i * 20)) > 20) { Hash[i * 20 + m] = (byte)textBox7.Text[i * 20 + m]; }
                        else
                        {
                            for (int p = 0; p < textBox7.Text.Length - (i * 20); p++)
                            {
                                Hash[i * 20 + p] = (byte)textBox7.Text[i * 20 + p];
                            }
                        }
                          

                        }
                        RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
                        RSAFormatter.SetHashAlgorithm("SHA1");
                        byte[] Hash2 = new byte[20];
                        for (int l = 0; l <20; l++)
                        {
                        if ((textBox9.Text.Length - (i * 20)) > 20) { Hash[i * 20 + l] = (byte)textBox9.Text[i * 20 + l]; }
                        else {
                            for (int p = 0; p < textBox9.Text.Length - (i * 20); p++)
                            {
                                Hash[i * 20 + p] = (byte)textBox9.Text[i * 20 + p];
                            }
                        }  
                        
                       }

                        byte[] SignedHash = RSAFormatter.CreateSignature(Hash2);
                        RSAPKCS1SignatureDeformatter signatureDeformatter = new RSAPKCS1SignatureDeformatter(RSA);
                        signatureDeformatter.SetHashAlgorithm("SHA1");
                        if (signatureDeformatter.VerifySignature(Hash, SignedHash)) { MessageBox.Show("İmza Doğrulandı."); } else { MessageBox.Show("İmza Doğrulanmadı."); }
                    
                }
            }



        }
    }

}

