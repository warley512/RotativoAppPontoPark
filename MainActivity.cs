using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RotativoApp
{
    [Activity(Label = "RotativoApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button btnCompraRotativo = FindViewById<Button>(Resource.Id.btnCompraRotativo);
            Button btnRenovaRotativo = FindViewById<Button>(Resource.Id.btnRenovaRotativo);
            Button btnConsultaPlaca = FindViewById<Button>(Resource.Id.btnConsultaPlaca);

            btnCompraRotativo.Click += delegate
            {
                StartActivity(typeof(ComprarActivity));
            };

            btnRenovaRotativo.Click += delegate
            {
                StartActivity(typeof(RenovarActivity));
            };

            btnConsultaPlaca.Click += delegate
            {
                StartActivity(typeof(ConsultarActivity));
            };
        }
    }
}

