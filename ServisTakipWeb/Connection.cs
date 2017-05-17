using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisTakipWeb
{
    public class Connection
    {
        public static int ID;
        public static int parentID;
        public static int _id;
        public static string adi = "";
        public static string userName = "";
        public static string password = "";
        public static string whereAmI = "";

        public static string sayfaAdi = "";

        //Cagrilar
        public static string gelenCagrilar = "Gelen Çağrılar";
        public static string tamamlananCagrilar = "Tamamlanan Çağrılar";
        public static string atanmisCagrilar = "Atanmış Çağrılar";
        public static string bekleyenCagrilar = "Bekleyen Çağrılar";

        public static string musteriler = "Müşteriler";
        public static string yoneticiler = "Yöneticiler";
        public static string calisanlar = "Çalışanlar";
        public static string cagrilar = "Çağrılar";
        public static string profile = "Profil";

        public static string yoneticiBilgileri = "Yönetici Bilgileri";
        public static string yoneticiEkle = "Yönetici Ekle";
        public static string yonetici = "Yönetici";

        public static string musteriBilgileri = "Müşteri Bilgileri";
        public static string musteriEkle = "Müşteri Ekle";
        public static string musteri = "Müşteri";

        public static string calisanBilgileri = "Çalışan Bilgileri";
        public static string calisanEkle = "Çalışan Ekle";

        public static void Clear()
        {
            Connection.ID = -1;
            Connection.parentID = -1;
            Connection.adi = "";
            Connection.userName = "";
            Connection.password = "";
            Connection.whereAmI = "";
        }

    }
}