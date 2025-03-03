//=====================================================================================
// Developed by Kallebe Lins (https://github.com/kallebelins)
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Mvp24Hours.Core.Enums.Infrastructure;
using Mvp24Hours.Helpers;
using Mvp24Hours.Infrastructure.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mvp24Hours.Extensions
{
    /// <summary>
    /// Contains functions for web requests
    /// </summary>
    public static class HttpClientExtensions
    {
        const string METHOD_GET = "GET";
        const string METHOD_POST = "POST";
        const string METHOD_PATCH = "PATCH";
        const string METHOD_PUT = "PUT";
        const string METHOD_DELETE = "DELETE";

        /// <summary>
        /// 
        /// </summary>
        public static Encoding EncodingRequest { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 
        /// </summary>
        public static string ToQueryString(params object[] objs)
        {
            if (objs == null)
            {
                return string.Empty;
            }

            var result = new List<string>();
            foreach (var obj in objs)
            {
                var props = obj
                    .GetType()
                    .GetProperties()
                    .Where(p => p.GetValue(obj, null) != null);

                foreach (var p in props)
                {
                    var value = p.GetValue(obj, null);
                    if (value is ICollection enumerable)
                    {
                        result.AddRange(from object v in enumerable select string.Format("{0}={1}", p.Name, HttpUtility.UrlEncode(v.ToString())));
                    }
                    else
                    {
                        result.Add(string.Format("{0}={1}", p.Name, HttpUtility.UrlEncode(value.ToString())));
                    }
                }
            }

            return string.Join("&", result.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<string> HttpPostAsync(this HttpClient client, string url = "", string data = "", Dictionary<string, string> headers = null)
        {
            return await HttpSendAsync(client, url, headers, METHOD_POST, data);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<T> HttpPostAsync<T>(this HttpClient client, string url = "", string data = "", Dictionary<string, string> headers = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            var result = await HttpSendAsync(client, url, headers, METHOD_POST, data);
            if (!result.HasValue())
            {
                return default;
            }
            return result.ToDeserialize<T>(jsonSerializerSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<string> HttpGetAsync(this HttpClient client, string url = "", Dictionary<string, string> headers = null)
        {
            return await HttpSendAsync(client, url, headers, METHOD_GET, null);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<T> HttpGetAsync<T>(this HttpClient client, string url = "", Dictionary<string, string> headers = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            var result = await HttpSendAsync(client, url, headers, METHOD_GET, null);
            if (!result.HasValue())
            {
                return default;
            }
            return result.ToDeserialize<T>(jsonSerializerSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<string> HttpPutAsync(this HttpClient client, string url = "", string data = "", Dictionary<string, string> headers = null)
        {
            return await HttpSendAsync(client, url, headers, METHOD_PUT, data);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<T> HttpPutAsync<T>(this HttpClient client, string url = "", string data = "", Dictionary<string, string> headers = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            var result = await HttpSendAsync(client, url, headers, METHOD_PUT, data);
            if (!result.HasValue())
            {
                return default;
            }
            return result.ToDeserialize<T>(jsonSerializerSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<string> HttpPatchAsync(this HttpClient client, string url = "", string data = "", Dictionary<string, string> headers = null)
        {
            return await HttpSendAsync(client, url, headers, METHOD_PATCH, data);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<T> HttpPatchAsync<T>(this HttpClient client, string url = "", string data = "", Dictionary<string, string> headers = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            var result = await HttpSendAsync(client, url, headers, METHOD_PATCH, data);
            if (!result.HasValue())
            {
                return default;
            }
            return result.ToDeserialize<T>(jsonSerializerSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<string> HttpDeleteAsync(this HttpClient client, string url = "", Dictionary<string, string> headers = null)
        {
            return await HttpSendAsync(client, url, headers, METHOD_DELETE, null);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<T> HttpDeleteAsync<T>(this HttpClient client, string url = "", Dictionary<string, string> headers = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            var result = await HttpSendAsync(client, url, headers, METHOD_DELETE, null);
            if (!result.HasValue())
            {
                return default;
            }
            return result.ToDeserialize<T>(jsonSerializerSettings);
        }

        public static async Task<string> HttpSendAsync(this HttpClient client, string url, Dictionary<string, string> headers, string method, string data)
        {
            TelemetryHelper.Execute(TelemetryLevel.Verbose, "infra-httpclient-start");
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

                if (EncodingRequest == null)
                    EncodingRequest = Encoding.UTF8;

                string urlRequest = $"{client.BaseAddress}{url}";

                if (url.StartsWith("//") || url.Contains(":/") || url.Contains(":\\"))
                    urlRequest = url;

                using var request = new HttpRequestMessage(new HttpMethod(method), new Uri(urlRequest));

                string mediaType = string.Empty;

                if (headers.AnyOrNotNull())
                {
                    foreach (var keyValue in headers)
                    {
                        if (!keyValue.Key.HasValue() || !keyValue.Value.HasValue())
                            continue;
                        if (keyValue.Key == "Content-Type")
                        {
                            mediaType = keyValue.Value.Split(';').ElementAtOrDefault(0);
                        }
                        request.Headers.TryAddWithoutValidation(keyValue.Key, keyValue.Value);
                    }
                }

                if (!mediaType.HasValue())
                {
                    mediaType = "application/json";
                }

                if (!headers.ContainsKeySafe("Content-Type"))
                {
                    request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType));
                    request.Headers.TryAddWithoutValidation("Content-Type", $"{mediaType}; charset={EncodingRequest.BodyName.ToLower()}");
                }

                if (method == "POST" || method == "PUT" || method == "PATCH")
                {
                    request.Headers.TryAddWithoutValidation("Content-Length", EncodingRequest.GetBytes(data ?? string.Empty).Length.ToString());
                    request.Content = new StringContent(data, EncodingRequest);
                    request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType);
                }

                TelemetryHelper.Execute(TelemetryLevel.Verbose, "infra-httpclient-sendasync", $"url:{url}|method:{method}");

                var response = await client.SendAsync(request);
                if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new HttpNotFoundException();
                }

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                TelemetryHelper.Execute(TelemetryLevel.Error, "infra-httpclient-failure", ex);
                throw;
            }
            finally
            {
                TelemetryHelper.Execute(TelemetryLevel.Verbose, "infra-httpclient-end");
            }
        }
    }
}
