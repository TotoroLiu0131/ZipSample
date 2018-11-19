using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class DaoTests
    {
        [TestMethod]
        public void project_test()
        {
            var expected = new OrderDto
            {
                TotalAmount = 100,
                Date = "20181117",
                Id = 1111
            };

            var insertData = new Order
            {
                Amount = 100,
                CreateDate = new DateTime(2018, 11, 17),
                OrderId = 1111
            };

            var actual = Project(insertData, order => new OrderDto
            {
                Id = order.OrderId,
                TotalAmount = order.Amount,
                Date = order.CreateDate.ToString("yyyyMMdd")
            });

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.TotalAmount, actual.TotalAmount);
        }

        [TestMethod]
        public void project_test1()
        {
            var expected = new OrderDto
            {
                TotalAmount = 100,
                Date = "20181117",
                Id = 1111
            };

            var insertData = new Order
            {
                Amount = 100,
                CreateDate = new DateTime(2018, 11, 17),
                OrderId = 1111
            };

            var actual = Project(insertData, new Mapper());

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.TotalAmount, actual.TotalAmount);
        }

        [TestMethod]
        public void project_test2()
        {
            var expected = new OrderDto
            {
                TotalAmount = 100,
                Date = "20181117",
                Id = 1111,
                Name = "Joy",
                Key = 10,
            };

            var insertData = new Order
            {
                Amount = 100,
                CreateDate = new DateTime(2018, 11, 17),
                OrderId = 1111,
                Name = "Joy",
                Key = 10,
            };

            var actual = Project(insertData);

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Key, actual.Key);
        }

        private OrderDto Project(Order source)
        {
            var sourceType = source.GetType();
            var sourcePropertiesName = sourceType.GetProperties().Select(x => x.Name);

            var result = new OrderDto();
            var resultType = result.GetType();

            foreach (var prop in resultType.GetProperties())
            {
                if (sourcePropertiesName.Contains(prop.Name))
                {
                    prop.SetValue(result, sourceType.GetProperty(prop.Name).GetValue(source));
                }
            }

            return result;
        }

        private TResult Project<TSource, TResult>(TSource source, IMapper<TSource, TResult> mapper)
        {
            return mapper.Project(source);
        }

        private TResult Project<TSource, TResult>(TSource source, Func<TSource, TResult> orderDto)
        {
            return orderDto(source);
        }

        public class Mapper : IMapper<Order, OrderDto>
        {
            public OrderDto Project(Order order)
            {
                return new OrderDto()
                {
                    Id = order.OrderId,
                    TotalAmount = order.Amount,
                    Date = order.CreateDate.ToString("yyyyMMdd")
                };
            }
        }

        public interface IMapper<TSource, TResult>
        {
            TResult Project(TSource source);
        }
    }
}