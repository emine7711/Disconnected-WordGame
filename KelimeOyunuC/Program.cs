using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KelimeOyunuC
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection Conn = new SqlConnection("server=LAPTOP-B5H5EM2R\\SQLEXPRESS;Database=Northwind;Integrated Security=true");
            //SqlConnection Conn = new SqlConnection("server=.;Database=Northwind;UID=wissen;PWD=456789");
            SqlDataAdapter da = new SqlDataAdapter("select ProductName from Products order by ProductName", Conn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            while (true)
            {
                Console.WriteLine("Hangi harfle başlayalım?");
                string harf = Console.ReadLine().ToLower();
                int counter = 0;//girilen harfle başlayan ürünleri sayıyor
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    DataRow item = tbl.Rows[i];
                    string productName = item[0].ToString();//yazım kolaylığı olsun diye değişkene attım
                                                            
                    if (productName.IndexOf(" ") != -1)//boşluk içeriyorsa
                    {
                        int spaceIndex = productName.IndexOf(" ");
                        productName = productName.Substring(0, spaceIndex);//sadece ilk kelimeyi al

                        if (productName.ToLower().StartsWith(harf))//ürünadı girilen harfle başlıyors
                        {
                            counter++;
                            Console.WriteLine(productName);
                            harf = productName.Substring(productName.Length - 1, 1);//harfi kelimenin son harfiyle güncelle
                            tbl.Rows[i][0] = "------";// silmek yerine 1kere yazdırılan ürünün adını değiştiriyorum
                            i = -1;//yeni ürünü aramaya kaldığı yerden değil en baştan başlasın istiyorum
                        }
                        counter = 0;//yeni harf için sayacı sıfırlıyorum
                    }
                }
                if (counter == 0)// tüm satırlar gezildikten sonra sayac hala sıfırsa bu harfle başlayan kelime yoktur
                    Console.WriteLine("Bu harfle başlayan kelime bulunamadı");
            }
        }
    }
}
