using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Background
{
    public class GetTockenRequestPOCO
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string sessionProductId { get; set; }
        public long numLaunchTokens { get; set; }
        public string marketType { get; set; }
        public long clientTypeId { get; set; }
        public string languageCode { get; set; }
    }
}
