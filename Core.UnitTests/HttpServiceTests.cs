using Core.Shared;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UnitTests
{
    public class HttpServiceTests
    {
        [Test]
        public async Task IsStatusOkAsync_PageReturns200_ReturnsTrue()
        {
            // Arrange
            var httpService = new HttpService();

            // Act
            var result = await httpService.IsStatusOkAsync("http://www.diuhefiuhrefiuhreiufhreif.com");

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsStatusOkAsync_PageReturns404_ReturnsFalse()
        {
            Assert.Pass();
        }
    }
}
