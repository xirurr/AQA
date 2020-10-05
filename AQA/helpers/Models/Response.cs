using System.Collections.Generic;

namespace AQA.helpers
{
    public class Response
    {
        public List<string> Errors { get; set; }
        public string Input { get; set; }
        public decimal Profit { get; set; }
        public decimal Commission { get; set; }
        public decimal Margin { get; set; }
    }
}