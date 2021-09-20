using AssessmentTest.Background;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest
{
    internal class ServiceResponse
    {
        public IRestResponse GetUserToken(string endpoint, dynamic payload, string guid)
        {
            var setReq = new SetRequest();
            var url = setReq.setUrl(endpoint);
            var jsonReq = setReq.serialize(payload);
            var request = setReq.CreateTokenPostRequest(jsonReq, guid);
            var response = setReq.GetResponce(url, request);
            //GetTokenResponseDTO content = JsonConvert.DeserializeObject<GetTokenResponseDTO>(responce.Content);
            //GetTokenResponseDTO content = user.GetContent<GetTokenResponseDTO>(response);
            return response;
        }

        public IRestResponse GetBalance(string endpoint, dynamic payload, string productID, String ModuleID, string token)
        {
            var setReq = new SetRequest();
            var url = setReq.setUrl(endpoint);
            var jsonReq = setReq.serialize(payload);
            var request = setReq.CreateGamePlayPostRequest(jsonReq, productID, ModuleID, token);
            var responce = setReq.GetResponce(url, request);
           // PlayGameResponseDTO content = user.GetContent<PlayGameResponseDTO>(responce);
            return responce;

        }
    }
}
