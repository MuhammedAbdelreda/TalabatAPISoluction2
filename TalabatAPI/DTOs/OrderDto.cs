using Talabat;

namespace TalabatAPI
{
    public class OrderDto
    {
        public string Id {  get; set; }

        public int DeliveryMethod {  get; set; }

        public AddressDto ShipToAddress { get; set; }


    }
}
