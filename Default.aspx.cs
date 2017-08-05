using System;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.IO.Ports;
using System.Text;

public partial class _Default : Page
{



    protected void Page_Load(object sender, EventArgs e)
    {
        //Funciona consumo de un ServicioJson  para Deseralizarlo y es una lista con []
        var client = new RestClient("https://jsonplaceholder.typicode.com/posts");
        // client.Authenticator = new HttpBasicAuthenticator(username, password);

        var request = new RestRequest("/", Method.GET);
        //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
        //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource


        // execute the request

        IRestResponse response = client.Execute(request);

        if (response.StatusCode.ToString() == "OK")
        {
            var content = response.Content;
            //Books book;
            var objResponse1 = JsonConvert.DeserializeObject<List<Books>>(content);
            lbl_Tittle.Text = objResponse1[0].title;

        }



    }

    public class Books
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }

    }
}


      

        //Funciona Codigo para consumir servicios Rest
        //    Meteorologia meteorologia;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://weathers.co/api.php?city=Madrid");
        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    using (Stream stream = response.GetResponseStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        var json = reader.ReadToEnd();
        //        meteorologia = JsonConvert.DeserializeObject<Meteorologia>(json);
        //    }
        //    lbl_Tittle.Text = "La temperatura en Madrid es: " + meteorologia.data.temperature;
        //    //Console.WriteLine("La temperatura en Madrid es: " + meteorologia.temperature);

        //}

        //public class Data
        //{
        //    public string location { get; set; }
        //    public string temperature { get; set; }
        //    public string skytext { get; set; }
        //    public string humidity { get; set; }
        //    public string wind { get; set; }
        //    public string date { get; set; }
        //    public string day { get; set; }
        //}

        //public class Meteorologia
        //{
        //    public string apiVersion { get; set; }
        //    public Data data { get; set; }
        //}

      

    
        