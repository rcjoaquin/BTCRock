using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCRock.bitcoin
{
    public class BitcoinClient
    {
        private HttpClient httpClient;

        
        public BitcoinClient(string endPoint, int port, string user, string password) 
        {
            this.httpClient = new HttpClient(endPoint, port, user, password);
        }
                
        public string getPublicKey(string accountId)
        {
            string parameters = string.Format("\"%s\"", accountId);
            
            string responseStr = this.httpClient.Post( buildPayload("getaddressesbyaccount", parameters));

            return parseArrayResult(responseStr);
        }

        
        public string getPrivateKey(string publicKey) 
        {
            string parameters = string.Format("\"%s\"", publicKey);

            string responseStr = this.httpClient.Post(buildPayload("dumpprivkey",  parameters));

            return parseResult(responseStr);
        }

        public float getBalance(string accountId, int confirmations)
        {
            string parameters = string.Format("\"%s\",%s", accountId, confirmations);

            string responseStr = this.httpClient.Post(buildPayload("getbalance",
                    parameters));

            return float.Parse(parseResult(responseStr));
        }

        public string createAddress(string accountId)
        {
            string parameters = string.Format("\"%s\"", accountId);

            string responseStr = this.httpClient.Post(buildPayload("getnewaddress",parameters));

            return parseResult(responseStr);
        }

        
        public string send(string accountId, string toAddress, float amount)
        {
            string parameters = string.Format("\"%s\",\"%s\",%s", accountId, toAddress,
                    string.Format("%.8f", amount));

            string responseStr = this.httpClient.Post(buildPayload("sendfrom",
                    parameters));

            string transactionId = parseResult(responseStr);

            if (string.IsNullOrEmpty(transactionId)) 
            {
                throw new Exception(string.Format("Call '%s' , Response: '%s', Error: '%s'",buildPayload("sendfrom", parameters), responseStr,
                        parseError(responseStr)));
            }

            return transactionId;
        }

        private static string buildPayload(string method, string parameters) {
            return string
                    .Format("{\"jsonrpc\": \"1.0\", \"id\":\"rpc\", "
                            + "\"method\": \"%s\", " + "\"parameters\": [%s] }",
                            method, parameters);
        }

        private static string parseArrayResult(string response)
        {

            throw new NotImplementedException();

            //try {
                
            //    ObjectMapper mapper = new ObjectMapper();
            //    JsonNode json = mapper.readTree(response);
            //    string[] resultValues = mapper.readValue(json.get("result"),
            //            string[].class);
            //    return resultValues[0];
            //} catch (Exception e) {
            //    throw new RuntimeException(string.Format(
            //            "Error parsing response '%s' : %s", response,
            //            e.getMessage()));
            //}
        }

        private static string parseResult(string response) {
            //try {
            //    ObjectMapper mapper = new ObjectMapper();
            //    JsonNode json = mapper.readTree(response);
            //    string resultValue = mapper.readValue(json.get("result"),
            //            string.class);
            //    return resultValue;
            //} catch (Exception e) {
            //    throw new Exception(e.getMessage());
            //}

            throw new NotImplementedException();
        }

        private static string parseError(string response) {
            //try {
            //    ObjectMapper mapper = new ObjectMapper();
            //    JsonNode json = mapper.readTree(response);
            //    string resultValue = mapper.readValue(json.get("error"),
            //            string.class);
            //    return resultValue;
            //} catch (Exception e) {
            //    return "";
            //}

            throw new NotImplementedException();
        }
    }
}
