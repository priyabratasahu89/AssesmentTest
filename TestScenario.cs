using AssessmentTest.Background;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AssessmentTest
{
    [TestFixture]
    public class TestScenario
    {
        public string userToken;
        public static string TokenEndPoint = "accounts/login/real";
        public string clientId = System.Configuration.ConfigurationManager.AppSettings["clientId"];
        public string moduleId = System.Configuration.ConfigurationManager.AppSettings["moduleId"];
        public string productId = System.Configuration.ConfigurationManager.AppSettings["productId"];
        public string guid = System.Configuration.ConfigurationManager.AppSettings["guid"];
        
        [SetUp]
        public static void Main(string[] args)
        {
            var test = new TestScenario();
            test.GetToken();
            test.PlayGameBalance();
        }

        [Test]
        public void GetToken()
        {
           
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var content = File.ReadAllText(Path.Combine(assemblyPath + @"\TestData\TokenCreationData.json"));
            GetTockenRequestPOCO testdata = JsonConvert.DeserializeObject<GetTockenRequestPOCO>(content);

            var tokendata = new GetTockenRequestPOCO();
            tokendata.userName = testdata.userName;
            tokendata.password = testdata.password;
            tokendata.sessionProductId = testdata.sessionProductId;
            tokendata.numLaunchTokens = testdata.numLaunchTokens;
            tokendata.marketType = testdata.marketType;
            tokendata.clientTypeId = testdata.clientTypeId;
            tokendata.languageCode = testdata.languageCode;
            var serviceResp = new ServiceResponse();
            var response = serviceResp.GetUserToken(TokenEndPoint, tokendata, guid);
            JObject jObj = JObject.Parse(response.Content);
            userToken = (string)jObj.Descendants()
               .OfType<JProperty>().Where(p => p.Name == "userToken").First().Value;
            Assert.AreEqual(userToken, "JYDFGDFGFDGRTMPWZDFDFG");
        }

        [Test]
        public void PlayGameBalance()
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var content = File.ReadAllText(Path.Combine(assemblyPath + @"\TestData\GameplayTestData.json"));
            PlayGameRequestPOCO testdata = JsonConvert.DeserializeObject<PlayGameRequestPOCO>(content);

            var GamePlay = new PlayGameRequestPOCO();
            GamePlay.packetType = testdata.packetType;
            GamePlay.payload = testdata.payload;
            GamePlay.useFilter = testdata.useFilter;
            GamePlay.isBase64Encoded = testdata.isBase64Encoded;

            var GamePlayEndpoint = $"v1/games/module/{moduleId}/client/{clientId}/play";            
            var serviceResp = new ServiceResponse();
            var response = serviceResp.GetBalance(GamePlayEndpoint, GamePlay, productId, moduleId, userToken);
            JObject jObj = JObject.Parse(response.Content);
            var amount = (string)jObj.Descendants()
               .OfType<JProperty>().Where(p => p.Name == "totalInAccountCurrency").First().Value;            
            Assert.AreEqual(amount, "10000.0");

        }

    }
}
