using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace MockHttpTests
{
    public class Tests
    {
        [Test, CustomAutoData]
        public async Task Basic_test(Uri testUri, string result)
        {
            var handler = new MockHttpMessageHandler();
            
            var sut = new TestService(handler.ToHttpClient());            
            
            handler.When(HttpMethod.Get, testUri.ToString()).Respond(HttpStatusCode.OK, new StringContent(result));
           
            var actual = await sut.GetString(testUri);

            Assert.That(actual, Is.EqualTo(result));
        }

        [Test, CustomAutoData]
        public async Task Test_with_custom_autodata([Frozen] MockHttpMessageHandler handler, TestService sut, Uri testUri, string result)
        {
            handler.When(HttpMethod.Get, testUri.ToString()).Respond(HttpStatusCode.OK, new StringContent(result));

            var actual = await sut.GetString(testUri);

            Assert.That(actual, Is.EqualTo(result));
        }
    }
}