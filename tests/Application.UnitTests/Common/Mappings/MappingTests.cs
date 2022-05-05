using AutoMapper;
using NUnit.Framework;
using RetailInMotion.Application.Common.Mappings;
using RetailInMotion.Application.Orders.Queries.GetOrder;
using RetailInMotion.Domain.Entities;
using System.Runtime.Serialization;

namespace RetailInMotion.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config =>
                config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        [TestCase(typeof(Order), typeof(OrderDto))]
        [TestCase(typeof(OrderItem), typeof(OrderItemDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type)!;

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}