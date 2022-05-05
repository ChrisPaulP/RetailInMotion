using FluentAssertions;
using NUnit.Framework;
using RetailInMotion.Domain.Exceptions;
using RetailInMotion.Domain.ValueObjects;

namespace RetailInMotion.Domain.UnitTests.ValueObjects
{
    public class DeliveryAddressTests
    {
        [Test]
        public void ShouldThrowUnsupportedColourExceptionGivenNotSupportedColourCode()
        {
            var postCode = "Fartoolongpostcode";
            FluentActions.Invoking(() => DeliveryAddress.InvalidPostCode(postCode))
                .Should().Throw<InvalidPostCodeException>();
        }
    }
}