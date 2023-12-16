using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PlaneState
    {
        FAULTI,
        SUCCESS
    }
}
