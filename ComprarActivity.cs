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
using System.Net;
using System.Net.Http;

namespace RotativoApp
{
    //comprar teste
    [Activity(Label = "Comprar Rotativo")]
    public class ComprarActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Comprar);

            EditText editTextPlaca = FindViewById<EditText>(Resource.Id.editTextPlaca);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerTempo);
            Button btnComprar = FindViewById<Button>(Resource.Id.btnComprar);

            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.tempos_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            int[] minutos_values = Resources.GetIntArray(Resource.Array.minutos_values);

            btnComprar.Click += async (sender, e) =>
            {
                await ComprarAsync(new Rotativo { plaVei = editTextPlaca.Text, qtdMin = minutos_values[spinner.SelectedItemPosition] });
                //Ao clicar no bot�o de comprar, ele pega o que vem da placa (textview) e qtd e cria um objeto rotativo com eles.
            };
        }

        public async Task ComprarAsync(Rotativo rotativo)
        {
            TextView textViewResultado = FindViewById<TextView>(Resource.Id.textViewResultado);
            //O objeto Rotativo vem para aqui. No c�digo acima ele pega o que tem no textview de resultado.
            try
            {
                string url = "http://rotativo.azurewebsites.net/api/Rotativo/Comprar";//coloca a uri para chamar o servi�o. igual no postman ok
                var uri = new Uri(url);//essa uri n�o precisa ser formatada j� q n�o tem parametros

                var json = JsonConvert.SerializeObject(rotativo);//Aqui ele j� converte o objeto rotativo em json pra poder comunicar com o servi�o
                var content = new StringContent(json, Encoding.UTF8, "application/json");//Prepara o objeto para a chamada do metodo do servi�o

                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.PostAsync(uri, content);//Chama o m�todo passando a uri e o rotativo por parametro. O rotativo que veio nos parametros desse m�todo async
                                                                                    //Ele faz dessa forma para ter seguran�a com os dados enviados, j� que n�o s�o enviados na url.

                var result = await response.Content.ReadAsStringAsync();//pego o resultado que veio da chamada, igual no postman. Ele vem em json.
                Mensagem mensagem = JsonConvert.DeserializeObject<Mensagem>(result);//Converto json para mensagem

                textViewResultado.Text = response.IsSuccessStatusCode ? mensagem.descricao : "Ops! Erro " + response.StatusCode;//Se tudo deu certo, ele exibe os dados em json q vieram da chamada, sen�o ele exibe q deu erro
            }
            catch (Exception ex)
            {
                textViewResultado.Text = "Ops! " + ex.Message + (ex.InnerException == null ? ex.InnerException.Message : String.Empty);
            }
        }
    }
}