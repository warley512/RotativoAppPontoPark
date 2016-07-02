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
                //Ao clicar no botão, ele manda por parametro o objeto rotativo q é criado com os valores q estão nos textviews
                
            };
        }

        public async Task RenovarAsync(Rotativo rotativo)
        {
            TextView textViewResultado = FindViewById<TextView>(Resource.Id.textViewResultado);
            try
            {
                string url = "http://rotativo.azurewebsites.net/api/Rotativo/Renovar";//aqui como no postman voce coloca renovar na url, mas os dados são enviados no corpo
                var uri = new Uri(url);// aqui ele  está apenas criando um objeto uri para receber os dados do corpo

                var json = JsonConvert.SerializeObject(rotativo);// Nesse aki ele vai serializar o objeto, ele transforma o objeto rotativo num json
                var content = new StringContent(json, Encoding.UTF8, "application/json");// aqui ele converte o arquivo que já está json mas o vs não reconhece
                
                HttpClient client = new HttpClient();
                //Esse objeto aí é usado pra fazer o chamado por HTTP entendi

                HttpResponseMessage response = await client.PutAsync(uri, content);// Aqui ele tá chamando o método, passando a URI e o ojbeto rotativo por parametro

                var result = await response.Content.ReadAsStringAsync();// aqui que ele exibe o resultado
                
                Mensagem mensagem = JsonConvert.DeserializeObject<Mensagem>(result);//Ele converte json em um ojbeto mensagem
                

                textViewResultado.Text = response.IsSuccessStatusCode ? mensagem.descricao : "Ops! Erro " + response.StatusCode;
            }
            catch (Exception ex)
            {
                textViewResultado.Text = "Ops! " + ex.Message + (ex.InnerException == null ? ex.InnerException.Message : String.Empty);
            }
        }
    }
}