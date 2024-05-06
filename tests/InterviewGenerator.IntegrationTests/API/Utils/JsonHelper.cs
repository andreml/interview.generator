using Newtonsoft.Json;

namespace InterviewGenerator.IntegrationTests.API.Utils;

public static class JsonHelper
{
    public static async Task<T> LerDoJson<T>(HttpContent content)
    {
        var responseString = await content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString)!;
    }
}
