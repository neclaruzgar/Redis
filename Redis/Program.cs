using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    class Program
    {
        static void Main(string[] args)
        {

            Ogrenci ogrenci1 = new Ogrenci();

            //Ögrenci sınıfı oluşturuldu ve değer atamaları yapılıyor
            ogrenci1.Ad = "Necla";
            ogrenci1.Soyadi = "RÜZGAR";
            ogrenci1.OgrenciNo = "1111111111";
            ogrenci1.Sinif = "4";
            ogrenci1.Mail = "neclaruzgarr@gmail.com";

            Ogrenci ogrenci2 = new Ogrenci()
            {
                Ad = "Ogrenci 2 Adı",
                Soyadi = "Ogrenci 2 Soyadı",
                Mail = "ogrenci2@gmail.com",
                OgrenciNo = "22222222",
                Sinif = "3"
            };

            List<Ogrenci> ogrenciListesi = new List<Ogrenci>() { ogrenci1, ogrenci2 };

            //Redis Clientımız oluşturuldu
            RedisClient redisClient= new RedisClient();

            //Oluşturduğumuz öğrenci nesnesi Redis'e yazılıyor
            redisClient.Set("Ogrenciler", ogrenciListesi, 5);

            //Yazılan öğrenci nesnesi Redis'den okunuyor
            //ogrenci1 = redisClient.Get<Ogrenci>("Ogrenciler");
            //var registeredOgrenci= redisClient.Get<Ogrenci>("Öğrenciler");//Ogrenci titpinde bir değer dönücek

            //var allKeys=redisClient.GetAllKeys(); //GetAllKeys'den dönen tüm değerleri allKeys'e atarım

            var ogrenciler = redisClient.Get<List<Ogrenci>>("Ogrenciler");
            Console.ReadKey();
        }
    }
}
