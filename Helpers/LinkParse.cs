using System.Text.RegularExpressions;

namespace TISB.Helpers {
    public enum TypeLink {
        None,
        Chat,
        User
    }
    public class LinkParse {
        public static (TypeLink Type, string Id) Parse(string link) {
            var userMatch = Regex.Match(link, @"user=(.*)");
            var chatMatch = Regex.Match(link, @"chat=(.*)");

            if (userMatch.Success)
                return (TypeLink.User, userMatch.Groups[1].Value);
            else if (chatMatch.Success)
                return (TypeLink.Chat, chatMatch.Groups[1].Value);
            else
                return (TypeLink.None, string.Empty);
        }
    }
}
