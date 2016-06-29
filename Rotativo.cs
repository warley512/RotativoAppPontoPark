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

namespace RotativoApp
{
    public class Rotativo
    {
        public int codRot { get; set; }
        public string plaVei { get; set; }
        public System.DateTime datEnt { get; set; }
        public Nullable<System.DateTime> datSai { get; set; }
        public int qtdMin { get; set; }
        public decimal valTotal { get; set; }
    }

    public class Mensagem
    {
        public int codigo { get; set; }
        public string descricao { get; set; }
    }
}