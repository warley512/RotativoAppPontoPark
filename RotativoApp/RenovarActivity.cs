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
                //Msm coisa aki. O krinha clicou no botão, ele manda por parametro o objeto rotativo q é criado com os valores q estão nos textviews
                // ok esse vai ser muito parecido com o comprar  kkkkkk isso
                //Então, já q é parecido. Explica pra mim oq kd um faz :D rsrs xa comigo
            };
        }

        public async Task RenovarAsync(Rotativo rotativo)
        {
            TextView textViewResultado = FindViewById<TextView>(Resource.Id.textViewResultado);// aqui ele pega o resultado vindo no formato json,
             //NÃO. esse resultado aki tá vazio ainda. kkKKKKKKEkkkklkkkkekkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk Ele só está pegando o textview pra poder mostrar lá na última linah kkkkkkkkkkkkkkkkkk
             //blz 

            try
            {
                string url = "http://sistemarotativo.azurewebsites.net/api/Rotativo/Renovar";//aqui como no postman voce coloca renovar na url, mas os dados são enviados no corpo
                var uri = new Uri(url);// aqui ele  está apenas criando um objeto uri para receber os dados do corpo

                var json = JsonConvert.SerializeObject(rotativo);// aqui ele deserializa o objeto. Nesse aki ele vai serializar kkkkkkk ele transforma o objeto rotativo num json hum entendi
                var content = new StringContent(json, Encoding.UTF8, "application/json");// aqui ele converte o arquivo que já está json mas o vs não reconhece
                
                HttpClient client = new HttpClient();// tká certo kkkkkkkkkkkkkk kkkkkkkkkkk tá msm?
                //tá sim. Esse objeto aí é usado pra fazer o chamado por HTTP entendi next

                HttpResponseMessage response = await client.PutAsync(uri, content);// Aqui ele tá chamando o método, passando a URI e o ojbeto rotativo por parametro

                var result = await response.Content.ReadAsStringAsync();// aqui que ele exibe o resultado?Ixxxxto . Ele pega aí
                //Corrigir um negócio q falei com vc
                Mensagem mensagem = JsonConvert.DeserializeObject<Mensagem>(result);//Nesse aqui, ao invés de transformar pra json, ele converte json em um ojbeto mensagem :D blz
                //tendeu? tendddi ficou bem claro
                //Não é difiiiiiicil assim. Ele só requer um pouquinho de café e práticak kkkkkkkkkkkkkkkkkkkkk
                // kkkkkkkkkk vou praticar e café já tomo msm kkkkkk
                //Vc já está beeem fera. Pelo q eu vi aki. mais ou menos kkkkkk 
                // vou estuddar mais quando dedico sai bubu. eu sei disso. E vc está dedicando agr. ENtão agr vaaaai kkkkkkkkkkkkkk
                //kkkkkkkkkkk vai sim muito obrigado viu
                //nada goiabadinha. Qq 10 real é isso ai
                //kkkkkkkkk aqui temos q combinar amanha o horario que vou te pegar aí
                //Se vc achar melhor, eu desço pro via
                // não de lá já vamos te pegeuo em casa
                //tão tá. Amn a gnt olha o horário melhor. V
                // isso, aqui mais uma coisinha, para passar o app para o telefone
                // qual o procedimento.
                //Vc pd fazer de duas maneiras. Eu faço só por uma e o tleis faz por outra. mas, da na meeeexma
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