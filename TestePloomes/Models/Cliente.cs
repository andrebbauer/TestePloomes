using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestePloomes.Models
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string NomeCompleto { get; set; }
        public string Nickname { get; set; }
        public bool Habilitado { get; set; }
        public Contato[] Contatos { get; set; }

        Cliente()
        {
            this.Id = "";
            this.NomeCompleto = "";
            this.Nickname = "";
            this.Habilitado = true;
        }
    }
}
