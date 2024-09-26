using Newtonsoft.Json;
using TISB.DataTransfer.JsonParser.Enums;
using TISB.Utils.User;

namespace TISB.DataTransfer.JsonParser {
    internal class UserJsonParse : IParseJson {
        public string Response { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        [JsonProperty("user")] public UserInfo User { get; set; }

        public ResponseStatus GetResponseStatus() {
            if (string.IsNullOrEmpty(Response))
                return ResponseStatus.Empty;
            else if (Response == "ok")
                return ResponseStatus.Success;
            else
                return ResponseStatus.Failed;
        }

        public string GetErrorMessage() {
            if (!string.IsNullOrEmpty(ErrorCode)) {
                var errorType = GetErrorCode();

                switch (errorType) {
                    case ErrorCodes.BadRequest:
                        return "Отправлен неизвестный запрос";
                    case ErrorCodes.LicenseInvalid:
                        return "Лицензия истекла или недействительна";
                    case ErrorCodes.UserNotFound:
                        return "Пользователь не найден";
                    default:
                        return $"Произошла ошибка: {ErrorMessage}";
                }
            }
            else {
                return $"Неизвестная ошибка: {ErrorMessage}";
            }
        }

        public ErrorCodes GetErrorCode() {
            if (!string.IsNullOrEmpty(ErrorCode)) {
                switch (ErrorCode) {
                    case "bad_request":
                        return ErrorCodes.BadRequest;
                    case "not_found":
                        return ErrorCodes.UserNotFound;
                    case "invalid_license":
                        return ErrorCodes.LicenseInvalid;
                    default:
                        return ErrorCodes.Unknown;
                }
            }

            return ErrorCodes.Unknown;
        }
    }
}
