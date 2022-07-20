using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace SupermercadoVirtual.Helper
{
    public static class SesionHelper
    {
        public static void SetObjetoToJson(this ISession sesion, string clave, object valor) {
            sesion.SetString(clave, JsonConvert.SerializeObject(valor));
        }
        public static T GetJsonToObjeto<T>(this ISession sesion, string clave) {
            return sesion.GetString(clave) == null ? default(T) : JsonConvert.DeserializeObject<T>(sesion.GetString(clave));
        }
    }
}
