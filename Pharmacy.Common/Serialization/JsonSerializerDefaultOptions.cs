using System.Text.Json;

namespace Pharmacy.Common.Serialization
{
    public static class JsonSerializerDefaultOptions
    {
        public static readonly JsonSerializerOptions Defaults = new()
        {
            PropertyNameCaseInsensitive = true,
        };
    }
}
