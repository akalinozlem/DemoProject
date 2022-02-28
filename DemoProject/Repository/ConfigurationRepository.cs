using DemoProject.IRepository;
using DemoProject.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DemoProject.Repository
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<Configuration> _configurationTable = null;

        public ConfigurationRepository()
        {
            _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            _database = _mongoClient.GetDatabase("BuildingDB");
            _configurationTable = _database.GetCollection<Configuration>("Configurations");
        }

        /// <summary>
        /// Delete a building informations.
        /// </summary>
        public bool Delete(string configurationId)
        {
            _configurationTable.DeleteOne(x => x.Id == configurationId);
            return true;
        }

        /// <summary>
        /// Get a building informations.
        /// </summary>
        public Configuration Get(string configurationId)
        {
            return _configurationTable.Find(x => x.Id == configurationId).FirstOrDefault();
        }

        /// <summary>
        /// Get all of the building informations.
        /// </summary>
        public List<Configuration> Gets()
        {
            return _configurationTable.Find(FilterDefinition<Configuration>.Empty).ToList();
        }

        /// <summary>
        /// Save the building informations.
        /// </summary>
        public Configuration Save(Configuration configuration)
        {
            var confObj = _configurationTable.Find(x => x.Id == configuration.Id).FirstOrDefault();
            if(confObj == null)
            {
                _configurationTable.InsertOne(configuration);
            }
            else
            {
                _configurationTable.ReplaceOne(X => X.Id == configuration.Id, configuration);
            }

            return configuration;
        }
    }
}
