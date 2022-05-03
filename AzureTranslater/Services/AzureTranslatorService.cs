using AzureTranslater.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureTranslater.Services
{
    public class AzureTranslatorService
    {
        //DECLARAMOS
        private static readonly string Key = "d3af587817f841c385d04a6341496a63";
        private static readonly string EndPoind = "https://api.cognitive.microsofttranslator.com/";
        private string route = "/translate?api-version=3.0&to=en";
        private static readonly string location = "westeurope";
        public async Task<List<AzureTranslatorModel>> Execute(List<AzureTranslatorRequestBody> requestBody)
        {
            //PARA CONTROLAR ERRORRES
            try
            {
                var body = requestBody;
                var requestBodyObject = JsonConvert.SerializeObject(body);
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    //PONEMOS DOLAR PARA LLAMAR DIRECTAMENTE A LOS CAMPOS
                    request.RequestUri = new Uri($"{EndPoind}{route}");
                    request.Content = new StringContent(requestBodyObject, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", Key);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", location);
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<List<AzureTranslatorModel>>(responseBody);
                        return model;
                    }
                    //SINO SE EJECUTA CORRECTAMENTE EL SERVICIO RETORNAMOS UNA LISTA VACÍA
                    return new List<AzureTranslatorModel>();
                }
            }
            catch(Exception)
            {
                return new List<AzureTranslatorModel>();
            }
        }
    }
}
