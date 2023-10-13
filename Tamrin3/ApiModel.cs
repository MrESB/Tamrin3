using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamrin3
{
    public class ApiModel
    {

        public List<ResultItem> result { get; set; }

    }

    public class ResultItem
    {
        public string key { get; set; }
        public string name_en { get; set; }
        public int? rank { get; set; }
        public string updated_at { get; set; }
        public double price { get; set; }

        public double? percent_change_1h { get; set; }
        public double? price_change_24h { get; set; }

    }

}
