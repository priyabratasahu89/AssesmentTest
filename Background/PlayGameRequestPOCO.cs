using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Background
{
    public class PlayGameRequestPOCO
    {
        public int packetType { get; set; }
        public string payload { get; set; }
        public bool useFilter { get; set; }
        public bool isBase64Encoded { get; set; }
    }
}
