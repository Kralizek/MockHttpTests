using System;
using System.Net.Http;
using AutoFixture;
using AutoFixture.Kernel;
using RichardSzalay.MockHttp;

namespace MockHttpTests
{
    public class HttpClientSpecimenBuilder : ISpecimenBuilder
    {
        public HttpClientSpecimenBuilder(IRequestSpecification requestSpecification)
        {
            RequestSpecification = requestSpecification ?? throw new ArgumentNullException(nameof(requestSpecification));
        }

        public HttpClientSpecimenBuilder() : this(new HttpClientRequestSpecification()) { }

        public IRequestSpecification RequestSpecification { get; }

        public object Create(object request, ISpecimenContext context)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!RequestSpecification.IsSatisfiedBy(request))
            {
                return new NoSpecimen();
            }

            var handler = context.Create<MockHttpMessageHandler>();

            return handler.ToHttpClient();
        }

        private class HttpClientRequestSpecification : IRequestSpecification
        {
            public bool IsSatisfiedBy(object request)
            {
                return request is Type type && type == typeof(HttpClient);
            }
        }
    }
}