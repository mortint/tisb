using Newtonsoft.Json;
using TISB.DataTransfer.JsonParser.Enums;

namespace TISB.DataTransfer.JsonParser {
    internal interface IParseJson {
        [JsonProperty("response")] public string Response { get; set; }
        [JsonProperty("error_message")] public string ErrorMessage { get; set; }
        [JsonProperty("error_code")] public string ErrorCode { get; set; }

        public string GetErrorMessage();

        public ErrorCodes GetErrorCode();
    }
}
