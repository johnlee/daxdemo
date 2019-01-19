using Amazon;
using Amazon.DAX;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace daxdemo.Data
{
    public class DynamoDbHandler : IDbHandler
    {
        private AWSCredentials _credentials;
        private RegionEndpoint _region;
        private string AccessId;
        private string SecretKey;
        private string HostName;
        private int Port;
        private string TableName;

        public DynamoDbHandler(IConfiguration configuration)
        {
            AccessId = configuration.GetValue<string>("AccessId");
            SecretKey = configuration.GetValue<string>("SecretKey");
            HostName = configuration.GetValue<string>("HostName");
            Port = configuration.GetValue<int>("Port");
            TableName = configuration.GetValue<string>("TableName");
            _region = RegionEndpoint.USWest2;
            _credentials = new BasicAWSCredentials(AccessId, SecretKey);
        }

        public async Task<bool> ConnectTest()
        {
            try
            {
                AmazonDynamoDBClient client = new AmazonDynamoDBClient(_credentials, _region);
                var result = await client.DescribeTableAsync(TableName);
                if (result.Table.TableName == TableName)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR " + e.Message);
                return false;
            }
        }

        public async Task<List<Widget>> Read()
        {
            List<Widget> widgets = new List<Widget>();
            try
            {
                ScanRequest scan = new ScanRequest(TableName);

                // DAX
                //DaxClientConfig daxConfig = new DaxClientConfig(HostName, Port);
                //daxConfig.AwsCredentials = _credentials;
                //daxConfig.RegionEndpoint = _region;
                //daxConfig.RequestTimeout = TimeSpan.FromSeconds(12);
                //daxConfig.ConnectTimeout = TimeSpan.FromSeconds(12);
                //ClusterDaxClient client = new ClusterDaxClient(daxConfig);

                // DYNAMODB
                AmazonDynamoDBClient client = new AmazonDynamoDBClient(_credentials, _region);

                var result = await client.ScanAsync(scan);

                foreach (var item in result.Items)
                {
                    widgets.Add(new Widget()
                    {
                        pk = item["pk"].S,
                        sk = item["sk"].S,
                        data = item["data"].S
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            return widgets;
        }

        public async Task<bool> Write(Widget widget)
        {
            try
            {
                AmazonDynamoDBClient client = new AmazonDynamoDBClient(_credentials, _region);
                var request = new PutItemRequest()
                {
                    TableName = TableName,
                    Item = new Dictionary<string, AttributeValue>()
                    {
                        { "pk", new AttributeValue{S = widget.pk } },
                        { "sk", new AttributeValue{S = widget.sk } },
                        { "data", new AttributeValue{S = widget.data } }
                    }
                };
                await client.PutItemAsync(request);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR " + e.Message);
                return false;
            }
        }
    }
}
