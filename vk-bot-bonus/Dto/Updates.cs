using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace vk_bot_bonus.Dto;

[Serializable]
public class Updates
{
    /// <summary>
    /// Тип события
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Объект, инициировавший событие
    /// Структура объекта зависит от типа уведомления
    /// </summary>
    [JsonProperty("object")]
    public JObject Object { get; set; }

    /// <summary>
    /// ID сообщества, в котором произошло событие
    /// </summary>
    [JsonProperty("group_id")]
    public long GroupId { get; set; }
}