using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using PriorbankModule.Entities;
using PriorbankModule.Common;
using PriorbankModule.Properties;

namespace PriorbankModule
{
    public class Configuration
    {
        public string DisplayName = "Приорбанк";

        public bool IsDelayedJob = true;

        public byte Hours = 0;

        public byte Minutes = 20;

        public string ImageBase64 = Resources.ImageBase64;

        [XmlElement(ElementName = "Логин")]
        public string Login = string.Empty;

        [XmlElement(ElementName = "Пароль[*]")]
        public string Password = string.Empty;

        [XmlElement(ElementName = "Название карточки")]
        public string CardName = string.Empty;

        [XmlElement(ElementName = "Первый запуск[b]")]
        public bool FirstLaunch = true;

        [XmlElement(ElementName = "ПослЗапрБюджета[_]")]
        public DateTime LastUpdate = DateTime.Now;

        [XmlElement(ElementName = "ЗаблТранзакции[_]")]
        public PriorbankTransaction[] LockedTransactions = new PriorbankTransaction[0];

        [XmlElement(ElementName = "ПоследнПолучТранз[_]")]
        public List<PriorbankTransaction> LastGivenTransactions = new List<PriorbankTransaction>();
        
        public static string GetConfiguration()
        {
            return Serializer.Serialize<Configuration>(new Configuration());
        }
    }
}
