using System.Net;
using System.Text.Json.Serialization;

namespace interview.generator.domain.Entidade.Common
{
    public class ResponseBase<T>
    {
        private HttpStatusCode _statusCode;
        private T? _data;
        private readonly List<string> _erros = new List<string>();

        [JsonIgnore]
        public int StatusCode => (int)_statusCode;

        [JsonPropertyName("data")]
        public T? Data => _data;

        [JsonPropertyName("erros")]
        public IEnumerable<string> erros => _erros;

        [JsonIgnore]
        public bool HasError => _erros.Any();

        public List<string> GetErrors() => _erros.ToList();

        public void SetStatusCode(HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
        }

        public void AddData(T data)
        {
            _data = data;
        }

        public void AddData(T data, HttpStatusCode statusCode)
        {
            _data = data;
            SetStatusCode(statusCode);
        }

        public void AddErro(string erro)
        {
            _erros.Add(erro); 
        }

        public void AddErrors(List<string> errors)
        {
            _erros.AddRange(errors);
        }
    }

    public class ResponseBase : ResponseBase<object>
    {
    }
}
