using MongoDB.Driver;
using TestePloomes.Models;

namespace TestePloomes.Services
{
    public class ComunicacaoService
    {
        private readonly IMongoCollection<Comunicacao> _comunicacoes;
        private string ConnectionString;

        public ComunicacaoService(IClientesDatabaseSettings settings)
        {
            var client = new MongoClient(DatabaseService.getConnectionString());
            var database = client.GetDatabase(settings.DatabaseName);

            _comunicacoes = database.GetCollection<Comunicacao>(settings.ComunicacaoCollectionName);
        }

        public List<Comunicacao> Get() =>
            _comunicacoes.Find(comunicacao => true).ToList();

        public List<Comunicacao> GetComunicacoesTituloMaiorQueCinco() =>
            _comunicacoes.Find(comunicacao => comunicacao.Titulo.Length > 5).ToList();

        public List<Comunicacao> GetComunicacoesCliente(string idCliente) =>
            _comunicacoes.Find(comunicacao => comunicacao.IdCliente == idCliente).ToList();

        public List<Comunicacao> GetComunicacoesForma(string idCliente, string formaDeContato) =>
            _comunicacoes.Find(comunicacao => comunicacao.IdCliente == idCliente && comunicacao.FormaDeContato.Equals(formaDeContato)).ToList();

        public List<Comunicacao> GetComunicacoesTitulo(string titulo) =>
            _comunicacoes.Find<Comunicacao>(comunicacao => comunicacao.Titulo == titulo).ToList();

        public Comunicacao Get(string id) =>
            _comunicacoes.Find<Comunicacao>(comunicacao => comunicacao.Id == id).FirstOrDefault();

        public Comunicacao Create(Comunicacao com)
        {
            _comunicacoes.InsertOne(com);
            return com;
        }
    }
}
