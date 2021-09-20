using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Background
{
    public class SetRequest
    {
        public string hostname = System.Configuration.ConfigurationManager.AppSettings["hostname"];

        public RestClient setUrl(String endpoint)
        {
            var url = $"{hostname}/{endpoint}";
            var restClient = new RestClient(url);
            return restClient;
        }

        public RestRequest CreateTokenPostRequest(dynamic payload, string guid)
        {
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddParameter("application/json", payload, ParameterType.RequestBody);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("X-CorrelationId", guid);
            restRequest.AddHeader("X-Forwarded-For", guid);
            restRequest.AddHeader("X-Clienttypeid", "5");
            return restRequest;
        }

        public RestRequest CreateGamePlayPostRequest(String payload, string productId, string moduleId, string newToken)
        {
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("X-Route-ProductId", productId);
            restRequest.AddHeader("X-Route-ModuleId", moduleId);
            restRequest.AddHeader("X-Clienttypeid", "38");
            restRequest.AddParameter("Authorization", string.Format("Bearer " + newToken));
            restRequest.AddHeader("X - correlationid", "93D10259 - 30F8 - 4339 - B456 - 3F30A43F65A2");
            restRequest.AddParameter("application/json", payload, ParameterType.RequestBody);
            return restRequest;
        }

        public string serialize(dynamic content)
        {
            string serializeObject = JsonConvert.SerializeObject(content, Formatting.Indented);
            return serializeObject;
        }

        // Respoce part
        public IRestResponse GetResponce(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }
    }
}
