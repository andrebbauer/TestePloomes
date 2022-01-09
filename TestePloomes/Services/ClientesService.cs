using MongoDB.Driver;
using TestePloomes.Models;

namespace TestePloomes.Services
{
    public class ClientesService
    {
        private readonly IMongoCollection<Cliente> _clientes;

        public ClientesService(IClientesDatabaseSettings settings)
        {
            var client = new MongoClient(DatabaseService.getConnectionString());
            var database = client.GetDatabase(settings.DatabaseName);

            _clientes = database.GetCollection<Cliente>(settings.ClientesCollectionName);
        }

        public List<Cliente> Get() =>
            _clientes.Find(cliente => true).ToList();

        public List<Cliente> GetHabilitados() =>
            _clientes.Find(cliente => cliente.Habilitado == true).ToList();

        public List<Cliente> GetDesabilitados() =>
            _clientes.Find(cliente => cliente.Habilitado == false).ToList();

        public List<Cliente> GetPorFormaDeContato(string formaDeContato) =>
            _clientes.Find(cliente => cliente.Habilitado == true && cliente.Contatos.Any(contato => contato.FormaDeContato.Equals(formaDeContato))).ToList();

        public List<Contato> GetContatos(string id) =>
            this.Get(id).Contatos.ToList();

        public Cliente Get(string id) =>
            _clientes.Find<Cliente>(cliente => cliente.Id == id).FirstOrDefault();

        public Cliente Create(Cliente cliente)
        {
            _clientes.InsertOne(cliente);
            return cliente;
        }

        public void Update(string id, Cliente clienteIn) =>
            _clientes.ReplaceOne(cliente => cliente.Id == id, clienteIn);

        public void UpdateHabilitar(string id)
        {
            var filter = Builders<Cliente>.Filter.Eq("Id", id);
            var update = Builders<Cliente>.Update.Set("Habilitado", true);
            _clientes.UpdateOne(filter, update);
        }

        public void UpdateDesabilitar(string id)
        {
            var filter = Builders<Cliente>.Filter.Eq("Id", id);
            var update = Builders<Cliente>.Update.Set("Habilitado", false);
            _clientes.UpdateOne(filter, update);
        }

        public void Remove(Cliente clienteIn) =>
            _clientes.DeleteOne(cliente => cliente.Id == clienteIn.Id);

        public void Remove(string id) =>
            _clientes.DeleteOne(cliente => cliente.Id == id);
    }
}
