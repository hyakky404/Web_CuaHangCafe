using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Web_CuaHangCafe.Helpers
{
    public static class ExtensionHelper
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            if (session == null)
            {
                // Xử lý tình huống phiên không tồn tại
                return default;
            }

            if (string.IsNullOrEmpty(value))
            {
                // Xử lý tình huống chuỗi JSON trống
                return default;
            }

            try
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
    }
}
