using Refit;
using Tuya.Exceptions;
using Tuya.Request;
using Tuya.Response;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tuya
{
    public class TuyaClient
    {
        protected string baseUrl = "https://openapi.tuyaeu.com";
        protected string signMethod = "HMAC-SHA256";
        protected string userAgent = "Tuya-dotNET/0.2.0";
        protected string language = "en";

        private readonly string clientId;
        private readonly string clientSecret;
        private readonly ITuyaCloudAPI api;

        private Credentials credentials;

        private HttpClient HttpClient
        {
            get
            {
                HttpClient httpClient = new HttpClient
                {
                    BaseAddress = new Uri(baseUrl)
                };

                httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
                httpClient.DefaultRequestHeaders.Add("client_id", clientId);
                httpClient.DefaultRequestHeaders.Add("sign_method", signMethod);
                httpClient.DefaultRequestHeaders.Add("lang", language);

                httpClient.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
                httpClient.DefaultRequestHeaders.Add("Keep-Alive", "timeout=600");

                return httpClient;
            }
        }

        public TuyaClient(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            api = RestService.For<ITuyaCloudAPI>(HttpClient);
        }

        public async Task<Credentials> Authorize()
        {
            RequestSignature sig = GenerateRequestSignature();
            Response.ApiResponse<Credentials> response = await api.GetAccessToken(
                sig.Signature,
                sig.Timestamp)
                .ConfigureAwait(false);

            if (!response.IsSuccess)
            {
                throw new ResponseException(response.ResponseMessage, response.ResponseCode);
            }

            credentials = response.Result;
            credentials.Timestamp = response.Timestamp;

            return credentials;
        }

        public async Task<Credentials> Reauthorize()
        {
            if (credentials == null || credentials?.RefreshToken.Length == 0)
            {
                return await Authorize().ConfigureAwait(false);
            }

            RequestSignature sig = GenerateRequestSignature();
            Response.ApiResponse<Credentials> response = await api.RefreshAccessToken(
                credentials.RefreshToken,
                sig.Signature,
                sig.Timestamp)
                .ConfigureAwait(false);

            if (!response.IsSuccess)
            {
                throw new ResponseException(response.ResponseMessage, response.ResponseCode);
            }

            credentials = response.Result;
            credentials.Timestamp = response.Timestamp;

            return credentials;
        }

        public async Task<DeviceInfo> GetDeviceInfo(string deviceId)
        {
            if (credentials == null || credentials?.AccessToken.Length == 0)
            {
                throw new UnauthorizedException("Unauthorized");
            }

            RequestSignature sig = GenerateRequestSignature(credentials.AccessToken, $"/v1.0/devices/{deviceId}");
            Response.ApiResponse<DeviceInfo> response = await api.GetDeviceInfo(
                sig.Signature,
                sig.Timestamp,
                credentials.AccessToken,
                deviceId)
                .ConfigureAwait(false);

            if (!response.IsSuccess)
            {
                throw new ResponseException(response.ResponseMessage, response.ResponseCode);
            }

            return response.Result;
        }

        public async Task<Attributes> GetDeviceStatus(string deviceId)
        {
            if (credentials == null || credentials?.AccessToken.Length == 0)
            {
                throw new UnauthorizedException("Unauthorized");
            }

            RequestSignature sig = GenerateRequestSignature(credentials.AccessToken, $"/v1.0/devices/{deviceId}/status");
            Response.ApiResponse<Attributes> response = await api.GetDeviceStatus(
                sig.Signature,
                sig.Timestamp,
                credentials.AccessToken,
                deviceId)
                .ConfigureAwait(false);

            if (!response.IsSuccess)
            {
                throw new ResponseException(response.ResponseMessage, response.ResponseCode);
            }

            return response.Result;
        }

        public async Task<DeviceSpecs> GetDeviceSpecifications(string deviceId)
        {
            if (credentials == null || credentials?.AccessToken.Length == 0)
            {
                throw new UnauthorizedException("Unauthorized");
            }

            RequestSignature sig = GenerateRequestSignature(credentials.AccessToken, $"/v1.0/devices/{deviceId}/specifications");
            Response.ApiResponse<DeviceSpecs> response = await api.GetDeviceSpecifications(
                sig.Signature,
                sig.Timestamp,
                credentials.AccessToken,
                deviceId)
                .ConfigureAwait(false);

            if (!response.IsSuccess)
            {
                throw new ResponseException(response.ResponseMessage, response.ResponseCode);
            }

            return response.Result;
        }

        public async Task<DeviceFunctions> GetDeviceFunctions(string deviceId)
        {
            if (credentials == null || credentials?.AccessToken.Length == 0)
            {
                throw new UnauthorizedException("Unauthorized");
            }

            RequestSignature sig = GenerateRequestSignature(credentials.AccessToken, $"/v1.0/devices/{deviceId}/functions");
            Response.ApiResponse<DeviceFunctions> response = await api.GetDeviceFunctions(
                sig.Signature,
                sig.Timestamp,
                credentials.AccessToken,
                deviceId)
                .ConfigureAwait(false);

            if (!response.IsSuccess)
            {
                throw new ResponseException(response.ResponseMessage, response.ResponseCode);
            }

            return response.Result;
        }

        public async Task<CommandResponse> SendCommands(string deviceId, Commands commands)
        {
            if (credentials == null || credentials?.AccessToken.Length == 0)
            {
                throw new UnauthorizedException("Unauthorized");
            }

            var jsonCommands = JsonConvert.SerializeObject(commands);

            RequestSignature sig = GenerateRequestSignature(
                credentials.AccessToken,
                "POST",
                $"/v1.0/devices/{deviceId}/commands", jsonCommands
                );

            CommandResponse response = await api.SendCommands(
                sig.Signature,
                sig.Timestamp,
                credentials.AccessToken,
                deviceId,
                jsonCommands)
                .ConfigureAwait(false);

            if (!response.IsSuccess)
            {
                throw new ResponseException(response.ResponseMessage, response.ResponseCode);
            }

            return response;
        }

        protected RequestSignature GenerateRequestSignature()
        {
            return GenerateRequestSignature(null, "GET", "/v1.0/token?grant_type=1", null);
        }

        protected RequestSignature GenerateRequestSignature(string accessToken, string signUrl)
        {
            return GenerateRequestSignature(accessToken, "GET", signUrl, null);
        }

        protected RequestSignature GenerateRequestSignature(string accessToken, string method, string signUrl, string body)
        {
            long timestamp = GetTimestamp();
            string contentHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            
            if (!string.IsNullOrEmpty(body))
            {
                contentHash = Encrypt(body);
            }

            string stringToSign = $"{method}\n{contentHash}\n\n{signUrl}";
            string str = clientId + (accessToken ?? "") + timestamp.ToString() + stringToSign;
            var signature = Encrypt(str, clientSecret);
            return new RequestSignature(signature, timestamp);
        }

        private string Encrypt(string message)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(message));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        private string Encrypt(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashmessage.Length; i++)
                {
                    builder.Append(hashmessage[i].ToString("x2"));
                }
                return builder.ToString().ToUpper();
            }
        }

        private long GetTimestamp()
        {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0))).TotalMilliseconds;
        }
    }
}