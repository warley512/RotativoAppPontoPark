using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace RotativoApp
{
    [Activity(Label = "Consultar por Placa")]
    public class ConsultarActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Consultar);

            EditText editTextPlaca = FindViewById<EditText>(Resource.Id.editTextPlaca);
            Button btnConsultar = FindViewById<Button>(Resource.Id.btnConsultar);

            btnConsultar.Click += async (sender, e) =>
            {
                await ConsultarAsync(editTextPlaca.Text);
            };
        }

        public async Task ConsultarAsync(string placa)
        {
            TextView textViewResultado = FindViewById<TextView>(Resource.Id.textViewResultado);
            //Pega o que vem na tela de consulta
            try
            {
                string url = "http://rotativo.azurewebsites.net/api/Rotativo/Consultar/{0}";//aki, vc coloca a url q vc vai buscar, igual no postman
                var uri = new Uri(string.Format(url, placa));//Aqui pega a url ai de cima e uniu com a placa. Ele substitui o 0 pela placa.

                var content = new StringContent(placa, Encoding.UTF8, "application/json");//Aqui ele vai chamar o serviço passando que o formato é em json

                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(uri);//Tá chamando o método pelo HTTP, passando a uri por parametro

                var result = await response.Content.ReadAsStringAsync();//Aqui ele pega o resultado, q é igual no postman, em json
                Mensagem mensagem = JsonConvert.DeserializeObject<Mensagem>(result);//Aqui também converte json em mensagem

                textViewResultado.Text = response.IsSuccessStatusCode ? mensagem.descricao : "Ops! Erro " + response.StatusCode; //Aqui é o final. Ele faz uma lógica booleana testando se deu erro no retorno ou não. E exibe no textview de resultado
               
            }
            catch (Exception ex)
            {
                textViewResultado.Text = "Ops! " + ex.Message + (ex.InnerException == null ? ex.InnerException.Message : String.Empty);
            }
        }
    }
}