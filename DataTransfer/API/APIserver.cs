using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TISB.Helpers.Client;

namespace TISB.DataTransfer.API {
    internal class APIserver {
        [NonSerialized] public static readonly string LinkTest = "http://murder/api/method/";
        [NonSerialized] public static readonly string Link = "https://murder.junf.ru/api/method/";
        public static string Request(string method, Dictionary<string, string> parameters) {
            var postData =
                string.Join("&", parameters.Select(x => $"{x.Key}={WebUtility.UrlEncode(x.Value)}"));

            return
                Network.POST(
                    $"{Link}{method}", postData);
        }
    }
}
