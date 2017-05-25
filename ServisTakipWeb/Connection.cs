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
        public static string firmaKodu = "";
        public static string userName = "";
        public static string password = "";
        public static string whereAmI = "";

        public static string sayfaAdi = "";

        //Cagrilar
        public static string gelenCagrilar = "Gelen Çağrılar";
        public static string tamamlananCagrilar = "Tamamlanan Çağrılar";
        public static string atanmisCagrilar = "Atanmış Çağrılar";
        public static string bekleyenCagrilar = "Bekleyen Çağrılar";
        public static string cagriOlustur = "Çağrı Oluştur";
        public static string acilanCagrilar = "Açılan Çağrılar";

        public static string musteriler = "Müşteriler";
        public static string yoneticiler = "Yöneticiler";
        public static string calisanlar = "Çalışanlar";
        public static string cagrilar = "Çağrılar";
        public static string profile = "Profil";

        public static string yoneticiBilgileri = "Yönetici Bilgileri";
        public static string yoneticiEkle = "Yönetici Ekle";
        public static string firmacalisan = "Firma Çalışanı";
        public static string yonetici = "Yönetici";

        public static string musteriBilgileri = "Müşteri Bilgileri";
        public static string musteriYoneticiBilgileri = "Müşteri Yönetici Bilgileri";
        public static string musteriCalisanBilgileri = "Müşteri Çalışan Bilgileri";
        public static string musteriEkle = "Müşteri Ekle";
        public static string musteri = "Müşteri";
        public static string musteriCalisan = "Müşteri Çalışanı";
        public static string musteriYonetici = "Müşteri Yönetici"; 

        public static string calisanBilgileri = "Çalışan Bilgileri";
        public static string calisanEkle = "Çalışan Ekle";

        //Tamamlanan Cagri Ekrani Icin
        public static string bildirilenAriza = "Bildirilen Arıza";
        public static string hizmetTipi = "Hizmet Tipi";
        public static string ariza = "Arıza";
        public static string kurulus = "Kuruluş";
        public static string geriAlim = "Geri Alım";
        public static string diger = "Diğer";
        public static string cihazinHizmetDurumu = "Cihazın Hizmet Durumu";
        public static string cihazBilgileri = "Cihaz Bilgileri";
        public static string cagriBilgileri = "Çağrı Bilgileri";

        public static void Clear()
        {
            Connection.ID = -1;
            Connection.parentID = -1;
            Connection.adi = "";
            Connection.firmaKodu = ""; 
            Connection.userName = "";
            Connection.password = "";
            Connection.whereAmI = "";
        }

    }
}