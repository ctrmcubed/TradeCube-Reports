using System.Collections.Generic;

namespace TradeCube_Reports.Messages
{
    public class WebServiceRequest
    {
        public string EntityType { get; set; }
        public string Format { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Entities { get; set; }
    }
}