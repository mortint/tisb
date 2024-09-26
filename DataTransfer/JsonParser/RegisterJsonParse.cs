using TISB.DataTransfer.JsonParser.Enums;

namespace TISB.DataTransfer.JsonParser {
    internal class RegisterJsonParse : IParseJson {
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
        public string GetErrorMessage() {
            if (!string.IsNullOrEmpty(ErrorCode)) {
                var errorType = GetErrorCode();

                switch (errorType) {
                    case ErrorCodes.BadRequest:
                        return "Отправлен неизвестный запрос";
                    case ErrorCodes.LoginOrPasswordEmpty:
                        return "Поле \"Логин\" или \"Пароль\" не может быть пустым";
                    case ErrorCodes.LoginOrPasswordLength:
                        return "Поле \"Логин\" или \"Пароль\" не может содержать менее 4 символов";
                    case ErrorCodes.DuplicateLogin:
                        return "Вы выбрали очень красивый логин, но, увы, он занят :(";
                    case ErrorCodes.HardDriveIdAlready:
                        return "Мне кажется, Вы вошли с другого устройства в Ваш аккаунт :(";
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
                    case "login_or_password_empty":
                        return ErrorCodes.LoginOrPasswordEmpty;
                    case "login_or_password_leaser_long":
                        return ErrorCodes.LoginOrPasswordLength;
                    case "duplicate_login":
                        return ErrorCodes.DuplicateLogin;
                    case "hard_driver_id_already":
                        return ErrorCodes.HardDriveIdAlready;
                    default:
                        return ErrorCodes.Unknown;
                }
            }

            return ErrorCodes.Unknown;
        }
    }
}
