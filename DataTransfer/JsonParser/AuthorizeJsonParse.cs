using TISB.DataTransfer.JsonParser.Enums;

namespace TISB.DataTransfer.JsonParser {
    internal class AuthorizeJsonParse : IParseJson {
        public string Response { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }

        public ResponseStatus GetResponseStatus() {
            if (string.IsNullOrEmpty(Response))
                return ResponseStatus.Empty;
            else if (Response == "ok")
                return ResponseStatus.Success;
            else
                return ResponseStatus.Failed;
        }

        public ErrorCodes GetErrorCode() {
            if (!string.IsNullOrEmpty(ErrorCode)) {
                switch (ErrorCode) {
                    case "drive_matches":
                        return ErrorCodes.DriveMatches;
                    case "does_not_match":
                        return ErrorCodes.DoesNotMatch;
                    case "not_found":
                        return ErrorCodes.UserNotFound;
                    case "invalid_request":
                        return ErrorCodes.BadRequest;
                    case "is_access_invalid":
                        return ErrorCodes.IsAccessInvalid;
                    case "is_banned":
                        return ErrorCodes.IsBanned;
                }
            }

            return ErrorCodes.Unknown;
        }

        public string GetErrorMessage() {
            if (!string.IsNullOrEmpty(ErrorCode)) {
                var errorType = GetErrorCode();

                switch (errorType) {
                    case ErrorCodes.BadRequest:
                        return "Отправлен неизвестный запрос";
                    case ErrorCodes.UserNotFound:
                        return "Похоже, Вы ошиблись. Пользователь с таким ником еще не зарегистрирован :(";
                    case ErrorCodes.DriveMatches:
                        return "Неудачная попытка входа...";
                    case ErrorCodes.DoesNotMatch:
                        return "Попробуйте авторизоваться позже";
                    case ErrorCodes.IsAccessInvalid:
                        return "Вы еще не получили доступ. Свяжитесь с администраторами.";
                    case ErrorCodes.IsBanned:
                        return "Ваш аккаунт заблокирован за нарушение правил регламента\n\nКонтакты: @tretiakov8229";
                    default:
                        return $"Произошла ошибка: {ErrorMessage}";
                }
            }
            else {
                return $"Неизвестная ошибка: {ErrorMessage}";
            }
        }
    }
}
