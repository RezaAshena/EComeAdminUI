using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Elasticsearch.Net;
using Elasticsearch.Net.Aws;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComeAdminUI.Models
{
    public class DatabaseClient : IDatabaseClient
    {
        private readonly IConfiguration _config;
        public DatabaseClient(IConfiguration config)
        {
            _config = config;
        }

        public IElasticClient GetDeliveryStoreIndexElasticClient()
        {
            //throw new NotImplementedException();
            var url = _config["Database:ElasticSearchUrl"];
            var defaultIndex = _config["Database:DeliveryStoreElasticIndex"];
            var profileName = _config["Database:ProfileName"];

            var chain = new CredentialProfileStoreChain(_config["Database:ElasticKeysPath"]);

            chain.TryGetAWSCredentials(profileName, out AWSCredentials awsCredentials);
            var httpConnection = new AwsHttpConnection(awsCredentials, Amazon.RegionEndpoint.CACentral1);
            var pool = new SingleNodeConnectionPool(new Uri(url));
            var settings = new ConnectionSettings(pool, httpConnection).DefaultIndex(defaultIndex);
            var client = new ElasticClient(settings);
            return client;
        }
    }
}
