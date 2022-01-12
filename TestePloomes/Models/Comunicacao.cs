using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

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
        [MinLength(5)]
        public string Titulo { get; set; }
        public string Conteudo { get; set; }

        Comunicacao()
        {
            this.Id = "";
        }
    }
}
