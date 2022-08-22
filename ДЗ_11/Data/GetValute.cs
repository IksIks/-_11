using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ДЗ_11.Services;

namespace ДЗ_11.Data
{
    internal class GetValute
    {
        /// <summary>
        /// Скачивание курса валют с сайта ЦБ
        /// </summary>
        /// <returns></returns>
        private void DownloadValuteData()
        {
            var client = new HttpClient();
            var page = $"http://www.cbr.ru/scripts/XML_daily.asp?date_req={DateTime.Now:dd/MM/yyyy}";
            var response = client.GetAsync(page).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            File.WriteAllText("ValuteCurse.xml", content);
        }

        /// <summary>
        /// Десирилизация скаченных данных из файла
        /// </summary>
        /// <returns>Объект типа ValCurs</returns>
        private ValCurs GetFromXML()
        {
            ValCurs valCurs;
            XmlSerializer ser = new XmlSerializer(typeof(ValCurs));
            using (StreamReader sr = new StreamReader("ValuteCurse.xml"))
            {
                valCurs = ser.Deserialize(sr) as ValCurs;
            }
            return valCurs;
        }

        /// <summary>
        /// Получение валюты в виде "Название", "Символьного Кода", "Значения"
        /// </summary>
        /// <param name="cash">ENUM класса GetValute</param>
        /// <returns>Tuple<string, string, double></returns>
        public Tuple<string, string, double> GetDataCurrentValute(Cash cash)
        {
            var valute = new GetValute().GetFromXML();
            foreach (var item in valute.Valute)
            {
                if(item.CharCode == cash.ToString())
                {
                    var valuteName = item.Name;
                    var valuteValue = double.Parse(item.Value);
                    var valuteCharCode = item.CharCode;
                    return new Tuple<string, string, double>(valuteName, valuteCharCode, valuteValue);
                }
            }
            return new Tuple<string, string, double>(" ", " ", 0.00);
        }

        public GetValute()
        {
            DownloadValuteData();
        }

    }
    public class ValCurs
    {
        [XmlElement]
        public List<Valute> Valute { get; set; }

        [XmlAttribute("Date")]
        public string Date { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
        public ValCurs(){ }
    }
    public class Valute
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public string Nominal { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Valute(){ }
    }
}
