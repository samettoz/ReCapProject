using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constans
{
    public static class Messages
    {
        public static string CarAdded = "Yeni araç eklendi...";
        public static string CarNameAlready = "Aynı isimde bir araç mevcut...";
        public static string BrandLimitExceded = "Araç marka sayısı 10 u geçtiği için yeni araç eklenemez";

        public static string UserAdded = "Kullanıcı eklendi";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UserAlreadyExist = "Böyle bir kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı oluşturuldu";
        public static string AccessTokenCreated = "accesstoken oluşturuldu";
    }
}
