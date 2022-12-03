using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCustomVisionWPFApp
{
    internal class Prediction
    {
        public double probability { get; set; }
        public string tagId { get; set; }
        public string tagName { get; set; }
        public Prediction() { probability = 0; tagId = ""; tagName = ""; }
    }

    internal class CustomVision
    {
        public string? id { get; set; }
        public string? project { get; set; }
        public string? iteration { get; set; }
        public DateTime? created { get; set; }
        public IList<Prediction> predictions { get; set; }
        public CustomVision() { predictions = new List<Prediction>(); }
    }


}
