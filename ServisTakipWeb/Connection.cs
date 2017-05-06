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
        public static string adi = "";
        public static string userName = "";
        public static string password = "";

        public static string whereAmI = "";

        //Cagrilar
        public static string gelenCagrilar = "Gelen Çağrılar";
        public static string tamamlananCagrilar = "Tamamlanan Çağrılar";
        public static string atanmisCagrilar = "Atanmış Çağrılar";
        public static string bekleyenCagrilar = "Bekleyen Çağrılar";

        public static string musteriler = "Müşteriler";
        public static string yoneticiler = "Yöneticiler";
        public static string calisanlar = "Çalışanlar";
        public static string cagrilar = "Çağrılar";

        public static string yoneticiBilgileri = "Yönetici Bilgileri";
        public static string yoneticiEkle = "Yönetici Ekle";

        public static string musteriBilgileri = "Müşteri Bilgileri";
        public static string musteriEkle = "Müşteri Ekle";

        public static string calisanBilgileri = "Çalışan Bilgileri";
        public static string calisanEkle = "Çalışan Ekle";


    }
}