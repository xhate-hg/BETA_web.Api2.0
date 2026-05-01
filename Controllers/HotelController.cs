using Beta_web.Api.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly HotelService _service;

    public HotelController(HotelService service)
    {
        _service = service;
    }

    // ---------------- SEARCH ----------------
    [HttpGet("search")]
    public async Task<IActionResult> Search(string city, string checkIn, string checkOut)
    {
        var result = await _service.SearchHotels(city, checkIn, checkOut);
        return Ok(result);
    }

    // ---------------- DETAILS ----------------
    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(string id)
    {
        var result = await _service.GetDetails(id);
        return Ok(result);
    }

 
    //  FAVORITES - POST
 
    [HttpPost("favorite")]
    public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteRequest request)
    {
        var result = await _service.AddFavorite(request);
        return Ok(result);
    }

   
    // FAVORITES - PUT
   
    [HttpPut("favorite/{id}")]
    public async Task<IActionResult> UpdateFavorite(int id, [FromBody] UpdateFavoriteRequest request)
    {
        var result = await _service.UpdateFavorite(id, request);
        return Ok(result);
    }

    // FAVORITES - DELETE
   
    [HttpDelete("favorite/{id}")]
    public async Task<IActionResult> DeleteFavorite(int id)
    {
        var result = await _service.DeleteFavorite(id);
        return Ok(result);
    }

   
    //  GET USER FAVORITES
   
    [HttpGet("favorites/{userId}")]
    public async Task<IActionResult> GetFavorites(string userId)
    {
        var result = await _service.GetFavorites(userId);
        return Ok(result);
    }
}