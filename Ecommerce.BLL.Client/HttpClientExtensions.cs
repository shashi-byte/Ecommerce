using System.Net.Http.Json;
using System.Net;

namespace Ecommerce.BLL.Client
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetAsyncSafe<T>(this System.Net.Http.HttpClient httpClient, string? requestUri) where T : class
        {
            try
            {
                var result = await httpClient.GetAsync(requestUri);

                if (result.StatusCode == HttpStatusCode.NoContent)
                {
                    return null;
                }
                else if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadFromJsonAsync<T>();
                }

                var msg = await result.Content.ReadAsStringAsync();
                msg = msg.Split(Environment.NewLine)[0];
                throw new Exception($"Status code: '{ToSeparateWords(result.StatusCode.ToString())}'<br \\>{msg}");
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        private static string ToSeparateWords(string value)
        {
            if (value == null) { return null; }
            if (value.Length <= 1) { return value; }
            char[] inChars = value.ToCharArray();
            List<int> uCWithAnyLC = new List<int>();
            int i = 0;
            while (i < inChars.Length && char.IsUpper(inChars[i])) { ++i; }
            for (; i < inChars.Length; i++)
            {
                if (char.IsUpper(inChars[i]))
                {
                    uCWithAnyLC.Add(i);
                    if (++i < inChars.Length && char.IsUpper(inChars[i]))
                    {
                        while (++i < inChars.Length)
                        {
                            if (!char.IsUpper(inChars[i]))
                            {
                                uCWithAnyLC.Add(i - 1);
                                break;
                            }
                        }
                    }
                }
            }
            char[] outChars = new char[inChars.Length + uCWithAnyLC.Count];
            int lastIndex = 0;
            for (i = 0; i < uCWithAnyLC.Count; i++)
            {
                int currentIndex = uCWithAnyLC[i];
                Array.Copy(inChars, lastIndex, outChars, lastIndex + i, currentIndex - lastIndex);
                outChars[currentIndex + i] = ' ';
                lastIndex = currentIndex;
            }
            int lastPos = lastIndex + uCWithAnyLC.Count;
            Array.Copy(inChars, lastIndex, outChars, lastPos, outChars.Length - lastPos);
            return new string(outChars);
        }

    }
}