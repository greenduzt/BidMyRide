using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]   
    public async Task<ActionResult<List<Item>>> SearchItems(string searchTerm)
    {
        var query = DB.Find<Item>();

        query.Sort(x => x.Ascending(a => a.Make));

        if(!string.IsNullOrEmpty(searchTerm))
        {
            query.Match(Search.Full,searchTerm).SortByTextScore();
        }


        // an asynchronous operation that executes the database query
        var result = await query.ExecuteAsync();

        return result;
    }
}