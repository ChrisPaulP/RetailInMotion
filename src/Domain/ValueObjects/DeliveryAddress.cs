namespace RetailInMotion.Domain.ValueObjects
{

    public class DeliveryAddress : ValueObject
    {
        public string Street { get; private set; }

        public string City { get; private set; }

        public string Country { get; private set; }

        public string PostCode { get; private set; }

        private DeliveryAddress() { }

        public DeliveryAddress(string street, string city, string country, string postCode)
        {
            Street = street;
            City = city;
            Country = country;
            PostCode = postCode;
        }
        public static DeliveryAddress Create(string street, string city, string country, string postCode)
        {
            return new DeliveryAddress(street, city, country, postCode);
        }
        public static DeliveryAddress InvalidPostCode(string postCode)
        {
            var da = new DeliveryAddress { PostCode = postCode };

            if (postCode.Length > 10)
            {
                throw new InvalidPostCodeException(postCode);
            }
            return da;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return City;
            yield return Country;
            yield return PostCode;
        }
    }
}
