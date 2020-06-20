using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes_Delegate
{
    public class UF
    {
        public int cod_uf { get; set; }
        public string des_uf { get; set; }
    }

    public class DataResult
    {
        public bool sucess { get; set; }
        public List<UF> data { get; set; }
    }

}
