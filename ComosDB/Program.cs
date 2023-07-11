using ComosDB;
using Microsoft.Azure.Cosmos;

Console.WriteLine("CosmosDB exercise\n");

// Run the examples asynchronously, wait for the results before proceeding
ProcessAsync().GetAwaiter().GetResult();

Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();

static async Task ProcessAsync()
{
    string endpoint = "PLACE-ENDPOINT";
    string key = "PLACE-STRING";
    CosmosClient client = new CosmosClient(endpoint, key);

    Console.WriteLine("Cosmos DB Client connection established.");

    // An object containing relevant information about the response
    Database database = await client.CreateDatabaseIfNotExistsAsync("3", 400);

    Console.WriteLine("Create Cosmos DB database if it does not exist, or fetch one.");
    Console.WriteLine("Created Database: {0}\n", database.Id);
    Console.WriteLine("Press enter to continue the sample application.");
    Console.ReadLine();

    DatabaseResponse readResponse = await database.ReadAsync();

    Console.WriteLine("Read the contents of the database fetched.");
    Console.WriteLine(readResponse);
    Console.WriteLine("Press enter to continue the sample application.");
    Console.ReadLine();

    ContainerProperties containerPropertiesToBuild = new ContainerProperties()
    {
        Id = "35",
        PartitionKeyPath = "/pk",
    };

    Container simpleContainer = await database.CreateContainerIfNotExistsAsync(containerPropertiesToBuild);//, ThroughputProperties.CreateAutoscaleThroughput(5000));

    Console.WriteLine("Create a container in the database fetched.");
    Console.WriteLine("Created Container: {0}\n", simpleContainer.Id);
    Console.WriteLine("Press enter to continue the sample application.");
    Console.ReadLine();

    Container container = database.GetContainer("35");
    ContainerProperties containerProperties = await container.ReadContainerAsync();

    Console.WriteLine("Read the contents of a container from the database fetched.");
    Console.Write(containerProperties.GetType());
    Console.WriteLine("Press enter to continue the sample application.");
    Console.ReadLine();

    QueryDefinition query = new QueryDefinition(
    "select * from c where c.Id = @IdInput ")
    .WithParameter("@IdInput", "3");

    FeedIterator<SalesOrder> resultSet = container.GetItemQueryIterator<SalesOrder>(
        query,
        requestOptions: new QueryRequestOptions()
        {
            PartitionKey = new PartitionKey("3"),
            MaxItemCount = 1
        });

    Console.WriteLine("Query the container of the Cosmos DB.");
    while (resultSet.HasMoreResults)
    {
        var resultQuery = await resultSet.ReadNextAsync();
        Console.WriteLine(resultQuery.ETag);
    }
    Console.WriteLine("Press enter to continue the sample application.");
    Console.ReadLine();

    await database.GetContainer("35").DeleteContainerAsync();
    Console.WriteLine("Deletes a container.");
    Console.WriteLine("Press enter to continue the sample application.");
    Console.ReadLine();

    await database.DeleteAsync();
}