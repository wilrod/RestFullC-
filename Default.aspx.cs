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


{% extends 'baseAdmin.html.twig' %}
{% set title = "Órdenes de Despacho "%}
{% block title title ~ " | Boosmap" %}
	{% block content %}
		<div class>
			<div class="page-title">
				<div class="title_left"> 
					<h3>Órdenes de Despacho</h3>
				</div>
			</div>
			<div class="clearfix"></div>
			<div class="row">
				<div class="col-md-12 col-sm-12 col-xs-12">
					<div class="x_panel" style="height: auto;">
						<div class="x_title">
							<h2>FILTRO DE FECHA</h2>
							<ul class="nav navbar-right panel_toolbox">
								<li><a class="collapse-link"><i class="fa fa-chevron-down"></i></a>
								</li>
								<li><a class="close-link"><i class="fa fa-close"></i></a>
								</li>
							</ul>
							<div class="clearfix"></div>
						</div>
						<div class="x_content" style="display: none;">
							<small>Selecciona una fecha para revisar tus pedidos</small>
							<form action="{{ path('despachos') }}" method="POST" class="dashboard-stat-list">
								<div class="row clearfix">
									<div class="col-sm-5 m-b-0">
										<div class="form-group m-b-0">
											<div class="form-line">
												<input type="text" name="desdePedido" class="datepicker form-control" placeholder="Por favor elija una fecha desde..." data-dtp="dtp_GaxGJ" value="{{ desde is empty ? "" : desde|date('d-m-Y') }}">
											</div>
										</div>
									</div>
									<div class="col-sm-5 m-b-0">
										<div class="form-group m-b-0">
											<div class="form-line">
												<input type="text" name="hastaPedido" class="datepicker form-control" placeholder="Por favor elija una fecha hasta..." data-dtp="dtp_GaxGJ" value="{{ hasta is empty ? "" : hasta|date('d-m-Y') }}">
											</div>
										</div>
									</div>
									<div class="col-sm-2">
										<button type="submit" class="btn btn-primary">FILTRAR</button>
									</div>
								</div>
							</form>
						</div>
					</div>
				</div>
			</div>
			<div class="row">
				<!-- Visitors -->
				<div class="col-md-12 col-sm-12 col-xs-12">
					<div class="x_panel">
						<div class="x_title">
							<h2>Ordenes de Despacho</h2>
							<div class="clearfix"></div>
						</div>
						<div class="x_content">
							<table class="table">
								<thead>
									<tr>
										<th>#</th>
										<th>Cuenta regresiva / Hora solicitud</th>
										<th>Hora término / Tiempo transcurrido</th>
										<th>Direccion</th>
										<th>Agente(s) / Estado</th>
										<th>Nombre cliente</th>
										<th>Items</th>
										<th>Opciones</th>
									</tr>
								</thead>
								<tbody>
									{% if pedidos|length > 0 %}
										{% for pedido in pedidos %}
											<tr>
												{# ID del pedido #}
												<th scope="row">{{ pedido.id }}</th>
												{# Cuenta regresiva / Hora solicitud #}
												<td>
													<div>
														<div id={{ pedido.id }} class="timer">
															{{ pedido.inicio|date() }}

														</div>
														<div class="request-time text-center">
															{{ pedido.inicio|date('d-m H:i', "America/Santiago") }}
														</div>
													</div>
												</td>
												{# Hora término / Tiempo transcurrido #}
												<td>
													{% if pedido.fin %}
														<div>{{ pedido.fin|date('d-m H:i', "America/Santiago") }}</div>
														<div>
															<i class="fa fa-clock-o"></i>
															{{pedido.calcDiffDates(pedido.inicio, pedido.fin)}}
														</div>
													{% else %}
														En Progreso
													{% endif %}
												</td>
												{# Direccion #}
												<td>
													<span>
														<small class="lbl-order">Zona de despacho</small>:&nbsp;<span>{{ pedido.sucursalDespacho }}</span><br>
													</span>
													<span>
														<small class="lbl-order">Comuna</small>:&nbsp;<span>{{ pedido.comunaDireccion }}</span><br>
													</span>
													<small><b>{{ pedido.direccionB2B }}</b></small>
												</td>
												{# Agente(s) / Estado #}
												<td>
													<div>
														<i class="fa fa-user"></i>
														{% if  pedido.booster is not null  %}
															 {{ pedido.booster }}
														{% endif %}
													</div>
													<div>
														<i class="fa fa-envelope-o"></i>
														{% if pedido.booster.usuario.email != NULL %}
															{{ pedido.booster.usuario.email }}
														{% endif %}
													</div>
													<div class="request-time">
														<i class="fa fa-building"></i>
														{% for estado in pedido.estados %}
															{% if loop.last %}
																{{ estado.estado }}<br>
																{{ estado.created |date('d-m H:i', "America/Santiago")}}
															{% endif %}
														
														{% endfor %}

													</div>
												</td>
												{# Nombre cliente #}
												<td>{{ pedido.nombreCliente }}</td>
												{# Items #}
												<td>{{ pedido.items|length }}</td>
												{# Opciones #}
												<td><a href="" class="btn btn-success"><i class="fa fa-eye"></i></a></td>
											</tr>
										{% endfor %}
									{% else %}
										<span>No posees Ordenes de Despacho</span>
									{% endif %}
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</div>
		</div>
	{% endblock %}
{% block javascripts %}
	{{ parent() }}
	<script type="text/javascript" src="http://momentjs.com/downloads/moment-with-locales.min.js"></script>
	<!-- Bootstrap Material Datetime Picker Plugin Js -->
	<script src="/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>
	<script src="https://cdn.pubnub.com/sdk/javascript/pubnub.4.15.1.js"></script>
	<script src="{{ asset('js/pages/pedidos.js') }}"></script>
	<script src="{{ asset('js/pages/chronometer.js') }}"></script>
	<script src="{{ asset('js/pages/pubnub.js') }}"></script>
{% endblock %}

      

    
        
