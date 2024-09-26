using TISB.DataTransfer.JsonParser.Enums;

namespace TISB.DataTransfer.JsonParser {
    internal class AdJsonParse : IParseJson {
        public string Response { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Text { get; set; }

        public ResponseStatus GetResponseStatus() {
            if (string.IsNullOrEmpty(Response))
                return ResponseStatus.Empty;
            else if (Response == "ok")
                return ResponseStatus.Success;
            else
                return ResponseStatus.Failed;
        }
        public string GetErrorMessage() { return Response; }

        public ErrorCodes GetErrorCode() { return GetErrorCode(); }
    }
}
