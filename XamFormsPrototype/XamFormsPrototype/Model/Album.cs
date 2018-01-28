using Newtonsoft.Json;
using SQLite;
using XamFormsPrototype.Contracts;

namespace XamFormsPrototype.Model
{
    [Table("Albums")]
    public class Album : IEntity
    {
        [JsonProperty("id")]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
