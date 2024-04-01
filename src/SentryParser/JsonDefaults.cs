using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SentryParser
{
    public static class JsonDefaults
    {
        public static readonly JsonSerializerOptions JSON_DEFAULTS = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
