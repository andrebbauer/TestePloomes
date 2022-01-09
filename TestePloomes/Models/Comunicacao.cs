using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestePloomes.Models
{
    public class Comunicacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdCliente { get; set; }
        public string FormaDeContato { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }

        Comunicacao()
        {
            this.Id = "";
        }
    }
}
