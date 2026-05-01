using Beta_web.Api.Models;
using static Beta_web.Api.Models.Hotel1;

public class HotelService
{
    private readonly HttpClient _client;
    private readonly string _apiKey;
    private readonly string _host;

   
    private static List<FavoriteHotel> _favorites = new();

    public HotelService(HttpClient client, IConfiguration config)
    {
        _client = client;
        _apiKey = config["RapidApi:Key"];
        _host = config["RapidApi:Host"];
    }

    // ---------------- SEARCH ----------------
    public async Task<List<Hotel>> SearchHotels(string city, string checkIn, string checkOut)
    {
        string geoId = await GetLocation(city);
        if (string.IsNullOrEmpty(geoId))
            return new List<Hotel>();

        var hotels = await GetHotels(geoId, checkIn, checkOut);

        return hotels
            .Where(x => x.Rating >= 4)
            .OrderByDescending(x => x.Rating)
            .ToList();
    }

    // ---------------- DETAILS ----------------
    public async Task<object> GetDetails(string id)
    {
        string url =
        $"https://tripadvisor16.p.rapidapi.com/api/v1/hotels/getHotelDetails?hotelId={id}";

        PrepareHeaders();

        var response = await _client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        return json;
    }

   
    public async Task<FavoriteHotel> AddFavorite(AddFavoriteRequest request)
    {
        var favorite = new FavoriteHotel
        {
            Id = _favorites.Count + 1,
            UserId = request.UserId,
            HotelId = request.HotelId,
            Title = request.Title,
            Rating = request.Rating,
            Notes = ""
        };

        _favorites.Add(favorite);
        return favorite;
    }

  
    public async Task<object> UpdateFavorite(int id, UpdateFavoriteRequest request)
    {
        var fav = _favorites.FirstOrDefault(x => x.Id == id);

        if (fav == null)
            return new { error = "Not found" };

        if (request.Notes != null)
            fav.Notes = request.Notes;

        if (request.Rating.HasValue)
            fav.Rating = request.Rating.Value;

        return new { message = "Updated", fav };
    }

  
    public async Task<object> DeleteFavorite(int id)
    {
        var fav = _favorites.FirstOrDefault(x => x.Id == id);

        if (fav == null)
            return new { error = "Not found" };

        _favorites.Remove(fav);

        return new { message = "Deleted", id };
    }

   
    public async Task<List<FavoriteHotel>> GetFavorites(string userId)
    {
        return _favorites
            .Where(x => x.UserId == userId)
            .ToList();
    }

    //  PRIVATE
    private async Task<string> GetLocation(string city)
    {
        string url =
        $"https://tripadvisor16.p.rapidapi.com/api/v1/hotels/searchLocation?query={city}";

        PrepareHeaders();

        var response = await _client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        var doc = System.Text.Json.JsonDocument.Parse(json);

        foreach (var item in doc.RootElement.GetProperty("data").EnumerateArray())
        {
            if (item.TryGetProperty("geoId", out var geo))
                return geo.ToString();
        }

        return "";
    }

    private async Task<List<Hotel>> GetHotels(string geoId, string checkIn, string checkOut)
    {
        List<Hotel> hotels = new();

        string url =
        $"https://tripadvisor16.p.rapidapi.com/api/v1/hotels/searchHotels?geoId={geoId}&checkIn={checkIn}&checkOut={checkOut}&pageNumber=1&currencyCode=USD";

        PrepareHeaders();

        var response = await _client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        var doc = System.Text.Json.JsonDocument.Parse(json);

        var hotelArray = doc.RootElement
            .GetProperty("data")
            .GetProperty("data");

        foreach (var item in hotelArray.EnumerateArray())
        {
            hotels.Add(new Hotel
            {
                Id = item.GetProperty("id").ToString(),
                Title = item.GetProperty("title").ToString(),
                Price = item.TryGetProperty("priceForDisplay", out var p) ? p.ToString() : "",
                Rating = item.TryGetProperty("bubbleRating", out var b) &&
                         b.TryGetProperty("rating", out var r)
                         ? r.GetDouble()
                         : 0
            });
        }

        return hotels;
    }

    private void PrepareHeaders()
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("x-rapidapi-key", _apiKey);
        _client.DefaultRequestHeaders.Add("x-rapidapi-host", _host);
    }
}