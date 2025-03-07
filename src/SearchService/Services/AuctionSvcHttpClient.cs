using System;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services;

public class AuctionSvcHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<List<Item>> GetItemsForSearchDb()
    {

        /*
            DB.Find<Item, string>()

            Queries the database for Item records.
            The second generic parameter <string> means the final projected result is a string.
            .Sort(x => x.Descending(x => x.UpdatedAt))

            Sorts the Item records in descending order based on the UpdatedAt field, meaning the most recently updated item comes first.
            .Project(x => x.UpdatedAt.ToString())

            Extracts only the UpdatedAt field and converts it to a string (likely a date-time string).
            .ExecuteFirstAsync();

            Executes the query asynchronously and retrieves only the first record (the most recently updated item's timestamp).
            The result is stored in lastUpdated.
        */


         var lastUpdated = await DB.Find<Item, string>()
            .Sort(x => x.Descending(x => x.UpdatedAt))
            .Project(x => x.UpdatedAt.ToString())
            .ExecuteFirstAsync();

            /*
                _httpClient.GetFromJsonAsync<List<Item>>()

                Sends an HTTP GET request to fetch a list of Item objects.
                The response is expected to be in JSON format, which will be deserialized into a List<Item>.
                (_config["AuctionServiceUrl"] + "/api/auctions?date=" + lastUpdated)

                Constructs the API URL dynamically using a base URL from _config["AuctionServiceUrl"].
                Appends ?date=<lastUpdated> as a query parameter, so the API only returns items updated after the retrieved timestamp.
            */

            return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"] + "/api/auctions?date=" + lastUpdated);
    }
}
