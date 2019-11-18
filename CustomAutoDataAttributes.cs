using System;
using System.Net.Http;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using RichardSzalay.MockHttp;

namespace MockHttpTests
{
    public static class FixtureHelper
    {
        public static IFixture CreateFixture()
        {
            var fixture = new Fixture();

            fixture.Customize(new AutoMoqCustomization
            {
                GenerateDelegates = true,
                ConfigureMembers = true
            });

            fixture.Customize<HttpClient>(o => o.FromFactory((MockHttpMessageHandler handler) => handler.ToHttpClient()));

            return fixture;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute() : base (FixtureHelper.CreateFixture) { }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomInlineAutoDataAttribute : InlineAutoDataAttribute
    {
        public CustomInlineAutoDataAttribute(params object[] arguments) : base(FixtureHelper.CreateFixture, arguments) { }
    }
}