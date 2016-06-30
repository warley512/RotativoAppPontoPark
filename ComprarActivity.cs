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
                //Quando o jovem clicar no botão de comprar, ele pega oq vem da placa (textview) e qtd e cria um objeto rotativo com eles.
            };
        }

        public async Task ComprarAsync(Rotativo rotativo)
        {
            TextView textViewResultado = FindViewById<TextView>(Resource.Id.textViewResultado);
            //Aí o kra vem parar aki. Ele aí em cima tá pegando oq tem no textview de resultado. Q nesse momento, não tem nada kkkkk
            try
            {
                string url = "http://sistemarotativo.azurewebsites.net/api/Rotativo/Comprar";//mesma coisa do de cima. Ele coloca a uri q a gnt vai chamar o serviço. igual no postman ok
                var uri = new Uri(url);//essa uri não precisa ser formatada já q não tem parametros

                var json = JsonConvert.SerializeObject(rotativo);//Aqui ele já converte o objeto rotativo em json pra poder comunicar com o serviço
                var content = new StringContent(json, Encoding.UTF8, "application/json");//Prepara o objeto para a chamada do metodo do serviço

                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.PostAsync(uri, content);//Chama o método passando a uri e o rotativo por parametro. O rotativo que veio ali nos parametros desse método async
                //tendeu essa parte? tendi ele faz dessa forma para ter segurança com os dados enviados, já que não são enviados na url. Iiiiiiisso. Que orgulho gnt rsrs teacher is very good
                //kkkkkkkkkkkkkk 
                var result = await response.Content.ReadAsStringAsync();//pego o resultado que veio da chamada, igual no postman. Ele vem em json.
                Mensagem mensagem = JsonConvert.DeserializeObject<Mensagem>(result);//Converto json para mensagem

                textViewResultado.Text = response.IsSuccessStatusCode ? mensagem.descricao : "Ops! Erro " + response.StatusCode;//Se tudo deu certo, ele exibe os dados em json q vieram da chamada, senão ele exibe q deu erro
                //tendeu aki? tendi
                //é tudo muito parecido. Poucas coisas mudam. Isso q é bom.verdade. Então bora pro atualizar. tbm é parecido.bora
            }
            catch (Exception ex)
            {
                textViewResultado.Text = "Ops! " + ex.Message + (ex.InnerException == null ? ex.InnerException.Message : String.Empty);
            }
        }
    }
}