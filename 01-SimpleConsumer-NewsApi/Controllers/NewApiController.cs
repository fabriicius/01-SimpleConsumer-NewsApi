using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System;

namespace _01_SimpleConsumer_NewsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NewApiController : ControllerBase
{

    public NewApiController()
    {

    }

    [HttpGet]
    public IActionResult Get()
    {
        var date = DateTime.Now.AddDays(-1);
        var newsApiClient = new NewsApiClient("dcf90877c0464f1e9c25aa4a9e5bbaf8");
        var response = newsApiClient.GetEverything(new EverythingRequest
        {
            Q = "bitcoin",
            SortBy = SortBys.Popularity,
            Language = Languages.EN,
            From = new DateTime(date.Year, date.Month, date.Day)
        });

        if (response.Status != Statuses.Ok)
            return BadRequest();

        if (response.TotalResults == 0)
            return NotFound();

        IList<object> resultArtigos = new List<object>();

        foreach (var artigos in response.Articles)
        {
            var obj = new
            {
                Autor = artigos.Author,
                Titulo = artigos.Title,
                Descircao = artigos.Description,
            };
            resultArtigos.Add(obj);
        }

        return Ok(resultArtigos);

    }
}

