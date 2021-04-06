using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Dynamic;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
// Works with method 1
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
// Works with method 2
//[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))] 
namespace Week10
{
    public class Function
    {
        
        public async Task<ExpandoObject> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context) 
        {
            HttpClient client = new HttpClient();
            string url = string.Empty;
            Dictionary<string, string> dict = (Dictionary<string, string>)input.QueryStringParameters;
            dict.TryGetValue("list", out url);
            string HttpCall = $"https://api.nytimes.com/svc/books/v3/lists/current/{url}.json?api-key=YAdI0vf6rIKPLCmLtzlt1K77m8L78VSO";

            // Method 1
            return JsonConvert.DeserializeObject<ExpandoObject>(await client.GetStringAsync(HttpCall));
            
            // Method 2
            //return JsonConvert.DeserializeObject(await client.GetStringAsync(HttpCall));
        }
    }
}
