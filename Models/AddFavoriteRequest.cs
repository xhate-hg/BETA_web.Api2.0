namespace Beta_web.Api.Models
{
    public class FavoriteHotel
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public string HotelId { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public string Notes { get; set; }
    }

    public class AddFavoriteRequest
    {
        public string UserId { get; set; }
        public string HotelId { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
    }

    public class UpdateFavoriteRequest
    {
        public string Notes { get; set; }
        public double? Rating { get; set; }
    }
}