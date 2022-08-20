using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Serialization;
using ДЗ_11.Services;

namespace ДЗ_11.Data
{
    internal class GetValute
    {
        private ValCurs DownloadValuteData()
        {
            var client = new HttpClient();
            var page = "http://www.cbr.ru/scripts/XML_daily.asp?";
            var response = client.GetAsync(page).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            File.WriteAllText("ValuteCurse.xml", content);
            ValCurs valCurs;
            XmlSerializer ser = new XmlSerializer(typeof(ValCurs));
            using (StreamReader sr = new StreamReader("ValuteCurse.xml"))
            {
                valCurs = ser.Deserialize(sr) as ValCurs;
            }
            return valCurs;
        }

        public (string valuteName, string valuteCharCode, double valuteValue) GetDataCurrentValute(Cash cash)
        {
            var valute = DownloadValuteData();
            foreach (var item in valute.Valute)
            {
                if(item.CharCode == cash.ToString())
                {
                    var valuteName = item.Name;
                    var valuteValue = double.Parse(item.Value);
                    var valuteCharCode = item.CharCode;
                    return (valuteName, valuteCharCode, valuteValue);
                }
            }
            return (" ", " ", 0.00);
        }



        public GetValute()
        {
            //var client = new HttpClient();
            //var page = "http://www.cbr.ru/scripts/XML_daily.asp?";
            //var response = client.GetAsync(page).Result;
            //var content = response.Content.ReadAsStringAsync().Result;
            //File.WriteAllText("ValuteCurse.xml", content);
            //ValCurs valCurs;
            //XmlSerializer ser = new XmlSerializer(typeof(ValCurs));
            //using (StreamReader sr = new StreamReader("test.xml"))
            //{
            //    valCurs = ser.Deserialize(sr) as ValCurs;
            //}
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
