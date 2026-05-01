namespace Beta_web.Api.Models
{
    public class Hotel1
    {
        public class Hotel
        {
           public string Id { get; set; }
           public string Title { get; set; }
           public string Price { get; set; }
           public double Rating { get; set; }
        }
        public class Rootobject
            {
                public bool status { get; set; }
                public string message { get; set; }
                public long timestamp { get; set; }
                public Data data { get; set; }
            }

            public class Data
            {
                public string sortDisclaimer { get; set; }
                public Datum[] data { get; set; }
            }

            public class Datum
            {
                public string id { get; set; }
                public string title { get; set; }
                public string primaryInfo { get; set; }
                public string secondaryInfo { get; set; }
                public Badge badge { get; set; }
                public Bubblerating bubbleRating { get; set; }
                public bool isSponsored { get; set; }
                public bool accentedLabel { get; set; }
                public string provider { get; set; }
                public object priceForDisplay { get; set; }
                public object strikethroughPrice { get; set; }
                public object priceDetails { get; set; }
                public object priceSummary { get; set; }
                public Cardphoto[] cardPhotos { get; set; }
            }

            public class Badge
            {
                public string size { get; set; }
                public string type { get; set; }
                public string year { get; set; }
            }

            public class Bubblerating
            {
                public string count { get; set; }
                public float rating { get; set; }
            }

            public class Cardphoto
            {
                public string __typename { get; set; }
                public Sizes sizes { get; set; }
            }

            public class Sizes
            {
                public string __typename { get; set; }
                public int maxHeight { get; set; }
                public int maxWidth { get; set; }
                public string urlTemplate { get; set; }
            }

            public class HotelDto
            {
                public string Id { get; set; }
                public string Name { get; set; }
                public string Price { get; set; }
                public List<string> PhotoUrls { get; set; }

            }
       
    }

}

