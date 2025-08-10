
using System.Text.Json;

/*
 * 
 * 
 * https://www.mockaroo.com/
 * 
 * 
 */
namespace Application.Services.Helpers
{
    public static class Deserialize
    {
        public static List<T>? Json<T>(string json)
        {
            return JsonSerializer.Deserialize<List<T>>(json);
        }
    }
}