using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace TodoWebApi.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Testing()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5000/api/Todo/test");
            Assert.Pass();
        }
    }
}