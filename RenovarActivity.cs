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
    [Activity(Label = "Renovar Rotativo")]
    public class RenovarActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Renovar);

            EditText editTextPlaca = FindViewById<EditText>(Resource.Id.editTextPlaca);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerTempo);
            Button btnRenovar = FindViewById<Button>(Resource.Id.btnRenovar);

            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.tempos_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            int[] minutos_values = Resources.GetIntArray(Resource.Array.minutos_values);

            btnRenovar.Click += async (sender, e) =>
            {
                await RenovarAsync(new Rotativo { plaVei = editTextPlaca.Text, qtdMin = minutos_values[spinner.SelectedItemPosition] });
                //Msm coisa aki. O krinha clicou no bot�o, ele manda por parametro o objeto rotativo q � criado com os valores q est�o nos textviews
                // ok esse vai ser muito parecido com o comprar  kkkkkk isso
                //Ent�o, j� q � parecido. Explica pra mim oq kd um faz :D rsrs xa comigo
            };
        }

        public async Task RenovarAsync(Rotativo rotativo)
        {
            TextView textViewResultado = FindViewById<TextView>(Resource.Id.textViewResultado);// aqui ele pega o resultado vindo no formato json,
             //N�O. esse resultado aki t� vazio ainda. kkKKKKKKEkkkklkkkkekkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk Ele s� est� pegando o textview pra poder mostrar l� na �ltima linah kkkkkkkkkkkkkkkkkk
             //blz 

            try
            {
                string url = "http://sistemarotativo.azurewebsites.net/api/Rotativo/Renovar";//aqui como no postman voce coloca renovar na url, mas os dados s�o enviados no corpo
                var uri = new Uri(url);// aqui ele  est� apenas criando um objeto uri para receber os dados do corpo

                var json = JsonConvert.SerializeObject(rotativo);// aqui ele deserializa o objeto. Nesse aki ele vai serializar kkkkkkk ele transforma o objeto rotativo num json hum entendi
                var content = new StringContent(json, Encoding.UTF8, "application/json");// aqui ele converte o arquivo que j� est� json mas o vs n�o reconhece
                
                HttpClient client = new HttpClient();// tk� certo kkkkkkkkkkkkkk kkkkkkkkkkk t� msm?
                //t� sim. Esse objeto a� � usado pra fazer o chamado por HTTP entendi next

                HttpResponseMessage response = await client.PutAsync(uri, content);// Aqui ele t� chamando o m�todo, passando a URI e o ojbeto rotativo por parametro

                var result = await response.Content.ReadAsStringAsync();// aqui que ele exibe o resultado?Ixxxxto . Ele pega a�
                //Corrigir um neg�cio q falei com vc
                Mensagem mensagem = JsonConvert.DeserializeObject<Mensagem>(result);//Nesse aqui, ao inv�s de transformar pra json, ele converte json em um ojbeto mensagem :D blz
                //tendeu? tendddi ficou bem claro
                //N�o � difiiiiiicil assim. Ele s� requer um pouquinho de caf� e pr�ticak kkkkkkkkkkkkkkkkkkkkk
                // kkkkkkkkkk vou praticar e caf� j� tomo msm kkkkkk
                //Vc j� est� beeem fera. Pelo q eu vi aki. mais ou menos kkkkkk 
                // vou estuddar mais quando dedico sai bubu. eu sei disso. E vc est� dedicando agr. ENt�o agr vaaaai kkkkkkkkkkkkkk
                //kkkkkkkkkkk vai sim muito obrigado viu
                //nada goiabadinha. Qq 10 real � isso ai
                //kkkkkkkkk aqui temos q combinar amanha o horario que vou te pegar a�
                //Se vc achar melhor, eu des�o pro via
                // n�o de l� j� vamos te pegeuo em casa
                //t�o t�. Amn a gnt olha o hor�rio melhor. V
                // isso, aqui mais uma coisinha, para passar o app para o telefone
                // qual o procedimento.
                //Vc pd fazer de duas maneiras. Eu fa�o s� por uma e o tleis faz por outra. mas, da na meeeexma
                //A cada rebuild solution q vc der, ele gera pra vc um apk na pasta bin/debug
                //Te mostrar.

                textViewResultado.Text = response.IsSuccessStatusCode ? mensagem.descricao : "Ops! Erro " + response.StatusCode;
            }
            catch (Exception ex)
            {
                textViewResultado.Text = "Ops! " + ex.Message + (ex.InnerException == null ? ex.InnerException.Message : String.Empty);
            }
        }
    }
}