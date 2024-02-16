using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Core.Utils
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        [Description("To Do")]
        ToDo = 1,
        [Description("In Progress")]
        InProgress,
        [Description("Done")]
        Done
    }
}
