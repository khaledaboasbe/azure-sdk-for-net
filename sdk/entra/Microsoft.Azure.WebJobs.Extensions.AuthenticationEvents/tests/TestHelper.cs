﻿//using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.AuthenticationEvents.TokenIssuanceStart;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Extensions.AuthenticationEvents.Tests
{
    /// <summary>Static Helper Methods for running tests.</summary>
    public static partial class TestHelper
    {
        private const string DefaultNamespace = "Microsoft.Azure.WebJobs.Extensions.AuthenticationEvents";
        private static readonly Assembly MainAssembly = Assembly.Load(DefaultNamespace);

        /// <summary>Enum for HTTP methods</summary>
        public enum HttpMethods
        {
            /// <summary>
            /// The post
            /// </summary>
            Post,
            /// <summary>
            /// The get
            /// </summary>
            Get
        }

        /// <summary>The validating JSON schema type.</summary>
        public enum TestSchemaType
        {
            /// <summary>Request Schema</summary>
            Request,

            /// <summary>Response Schema</summary>
            Response
        }

        /// <summary>
        /// This function will create action results based on incoming httpStatusCode
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        internal static TestAuthResponse GetContentForHttpStatus(HttpStatusCode httpStatusCode)
        {

            switch (httpStatusCode)
            {
                case HttpStatusCode.OK: return new TestAuthResponse(HttpStatusCode.OK, "{'hi':'bye'}");
                case HttpStatusCode.Unauthorized: return new TestAuthResponse(HttpStatusCode.Unauthorized);
                default: return new TestAuthResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// This function creates HttpRequestMessage  using the incoming params
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, string url, string body)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, url);
            requestMessage.Headers.Add("Authorization", "bearer 123123123123");
            requestMessage.Headers.Add("Accept", "*/*");
            requestMessage.Headers.Add("Connection", "keep-alive");
            requestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            if (!string.IsNullOrEmpty(body))
            {
                requestMessage.Content = new StringContent(body);
            }

            return requestMessage;
        }

        /// <summary>Sets up the boilerplate code for running end to end system tests.<br /><br />Sets the HTTP methods as post and a default function URL called OnTokenIssuanceStart</summary>
        /// <param name="action">Action to emulate the external function call.</param>
        /// <returns>A HttpResponseMessage containing the a result pertaining to the action expectations.</returns>
        public static async Task<HttpResponseMessage> BaseTest(Action<ActionParameters> action)
        {
            return await BaseTest(HttpMethods.Post, "http://test/mock?function=onTokenissuancestart", action);
        }

        /// <summary>
        /// Sets up the boilerplate code for running end to end system tests. Returning a valid EventResponseHandler in the action<br /><br />Sets the HTTP methods as post and a default function URL called OnTokenIssuanceStart
        /// </summary>
        /// <param name="action">Action to emulate the external function call.</param>
        /// <param name="testTypes">Defines the type of test</param>
        /// <returns>A HttpResponseMessage containing the a result pertaining to the action expectations.</returns>
        [Obsolete]
        public static async Task<HttpResponseMessage> EventResponseBaseTest(Action<AuthenticationEventResponseHandler> action, TestTypes testTypes)
        {
            return await EventResponseBaseTest(HttpMethods.Post, "http://test/mock?function=onTokenissuancestart", action, testTypes);
        }

        /// <summary>
        /// Sets up the boilerplate code for running end to end system tests. Returning a valid EventResponseHandler in the action<br /><br />Sets the HTTP methods as post and a default function URL called OnTokenIssuanceStart
        /// </summary>
        /// <param name="action">Action to emulate the external function call.</param>
        /// <returns>A HttpResponseMessage containing the a result pertaining to the action expectations.</returns>
        [Obsolete]
        public static async Task<HttpResponseMessage> EventResponseBaseTest(Action<AuthenticationEventResponseHandler> action)
        {
            return await EventResponseBaseTest(HttpMethods.Post, "http://test/mock?function=onTokenissuancestart", action);
        }

        /// <summary>
        /// Sets up the boilerplate code for running end to end system tests. Returning a valid EventResponseHandler in the action<br /><br />Sets the HTTP methods as post and a default function URL called OnTokenIssuanceStart
        /// </summary>
        /// <param name="httpMethods">Type of methods. i.e. Post/Get</param>
        /// <param name="url">The URL to use to create an inactive mock end point</param>
        /// <param name="action">Action to emulate the external function call.</param>
        /// <param name="testTypes">defines the type of test</param>
        /// <returns>A HttpResponseMessage containing the a result pertaining to the action expectations.</returns>
        ///
        [Obsolete]
        public static async Task<HttpResponseMessage> EventResponseBaseTest(HttpMethods httpMethods, string url, Action<AuthenticationEventResponseHandler> action, TestTypes testTypes)
        {
            return await (BaseTest(httpMethods, url, t =>
            {
                if (t.FunctionData.TriggerValue is HttpRequestMessage mockedRequest)
                {
                    AuthenticationEventResponseHandler eventsResponseHandler = (AuthenticationEventResponseHandler)mockedRequest.Properties[AuthenticationEventResponseHandler.EventResponseProperty];
                    eventsResponseHandler.Request = new TokenIssuanceStartRequest(t.RequestMessage)
                    {
                        Response = testTypes == TestTypes.ValidCloudEvent ? CreateTokenIssuanceStartResponse() : CreateIssuanceStartLegacyResponse(),
                        RequestStatus = RequestStatusType.Successful
                    };

                    action(eventsResponseHandler);
                }
            }));
        }

        /// <summary>
        /// Sets up the boilerplate code for running end to end system tests. Returning a valid EventResponseHandler in the action<br /><br />Sets the HTTP methods as post and a default function URL called OnTokenIssuanceStart
        /// </summary>
        /// <param name="httpMethods">Type of methods. i.e. Post/Get</param>
        /// <param name="url">The URL to use to create an inactive mock end point</param>
        /// <param name="action">Action to emulate the external function call.</param>
        /// <returns>A HttpResponseMessage containing the a result pertaining to the action expectations.</returns>
        [Obsolete]
        public static async Task<HttpResponseMessage> EventResponseBaseTest(HttpMethods httpMethods, string url, Action<AuthenticationEventResponseHandler> action)
        {
            return await (BaseTest(httpMethods, url, t =>
            {
                if (t.FunctionData.TriggerValue is HttpRequestMessage mockedRequest)
                {
                    AuthenticationEventResponseHandler eventsResponseHandler = (AuthenticationEventResponseHandler)mockedRequest.Properties[AuthenticationEventResponseHandler.EventResponseProperty];
                    eventsResponseHandler.Request = new TokenIssuanceStartRequest(t.RequestMessage)
                    {

                        Response = CreateIssuanceStartLegacyResponse(),
                        RequestStatus = RequestStatusType.Successful
                    };

                    action(eventsResponseHandler);
                }
            }));
        }


        /// <summary>Sets up the boilerplate code for running end to end system tests.</summary>
        /// <param name="httpMethods">Type of methods. i.e. Post/Get</param>
        /// <param name="url">The URL to use to create an inactive mock end point</param>
        /// <param name="action">Action to emulate the external function call.</param>
        /// <returns>A HttpResponseMessage containing the a result pertaining to the action expectations.</returns>
        public static async Task<HttpResponseMessage> BaseTest(HttpMethods httpMethods, string url, Action<ActionParameters> action)
        {
            HttpRequestMessage requestMessage = CreateHttpRequestMessage(httpMethods == HttpMethods.Post ? HttpMethod.Post : HttpMethod.Get, url);

            AuthenticationEventsTriggerAttribute attr = CreateAuthenticationEventTriggerAttribute("Tenant", "App");

            Mock<ITriggeredFunctionExecutor> mockObject = new Mock<ITriggeredFunctionExecutor>();

            AuthenticationEventConfigProvider eventsTriggerConfigProvider = new AuthenticationEventConfigProvider(new LoggerFactory());

            eventsTriggerConfigProvider.Listeners.Add("onTokenIssuanceStart", new AuthenticationEventListener(mockObject.Object, attr));

            mockObject.Setup(m => m.TryExecuteAsync(It.IsAny<TriggeredFunctionData>(), It.IsAny<CancellationToken>())).Callback<TriggeredFunctionData, CancellationToken>(
                (t, x) =>
                {
                    action(new ActionParameters()
                    {
                        FunctionData = t,
                        RequestMessage = requestMessage
                    });
                }).ReturnsAsync(new FunctionResult(true));

            return await eventsTriggerConfigProvider.ConvertAsync(requestMessage, new CancellationToken(false));
        }

        /// <summary>
        /// This function creates HttpRequestMessage  using the incoming params
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <returns>A newly created HttpRequestMessage</returns>
        public static HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, string url)
        {
            return CreateHttpRequestMessage(method, url, null);
        }

        /// <summary>
        /// This function creates AuthenticationEventTriggerAttribute using the incoming params
        /// </summary>
        /// <param name="versions">Available version</param>
        /// <param name="eventTypes">Available Event type</param>
        /// <returns>A newly create AuthenticationEventTriggerAttribute</returns>
        internal static AuthenticationEventsTriggerAttribute CreateAuthenticationEventTriggerAttribute(EventDefinition versions, EventType eventTypes)
        {
            return CreateAuthenticationEventTriggerAttribute(string.Empty, string.Empty);
        }

        /// <summary>
        /// This function creates AuthenticationEventTriggerAttribute using the incoming params
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="audienceAppId"></param>
        /// <returns>A newly create AuthenticationEventTriggerAttribute</returns>
        public static AuthenticationEventsTriggerAttribute CreateAuthenticationEventTriggerAttribute(string tenantId, string audienceAppId)
        {

            return new AuthenticationEventsTriggerAttribute()
            {
                TenantId = tenantId,
                AudienceAppId = audienceAppId
            };
        }

        /// <summary>Reads the content of an embedded resource.</summary>
        /// <param name="assembly">The assembly where the resource is embedded.</param>
        /// <param name="resource">The resource Identifier</param>
        /// <returns>The content of the resource as a string.</returns>
        /// <exception cref="System.MissingFieldException"></exception>
        public static string ReadResource(Assembly assembly, string resource)
        {
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                if (!assembly.GetManifestResourceNames().Any(x => x == resource))
                {
                    throw new MissingFieldException();
                }

                stream = assembly.GetManifestResourceStream(resource);
                reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
            }
        }

        /// <summary>Gets an attribute from an enumerator field</summary>
        /// <typeparam name="TAttribute">The Type of the attribute on the enumerator field.</typeparam>
        /// <param name="value">The enum that the field is on</param>
        /// <returns>The Attribute if found.</returns>
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        /// <summary>Creates the issuance start Legacy response.</summary>
        /// <returns>A newly created TokenIssuanceStartResponse for version preview_10_01_2021</returns>
        public static TokenIssuanceStartResponse CreateIssuanceStartLegacyResponse()
        {
            JObject jBody = JObject.Parse(ReadResource(MainAssembly, String.Join(".", DefaultNamespace, "Templates", "ActionableTemplate.json")));
            (jBody["type"] as JValue).Value = "onTokenIssuanceStartCustomExtension";
            (jBody["apiSchemaVersion"] as JValue).Value = "10-01-2021-preview";


            return new TokenIssuanceStartResponse()
            {
                Body = jBody.ToString()
            };
        }

        /// <summary>Creates the issuance start Legacy response.</summary>
        /// <returns>A newly created TokenIssuanceStartResponse for version preview_10_01_2021</returns>
        public static TokenIssuanceStartResponse CreateTokenIssuanceStartResponse()
        {

            JObject jBody = JObject.Parse(ReadResource(MainAssembly, String.Join(".", DefaultNamespace, "Templates", "CloudEventActionableTemplate.json")));
            (jBody["data"]["@odata.type"] as JValue).Value = "microsoft.graph.onTokenIssuanceStartResponseData";


            return new TokenIssuanceStartResponse()
            {
                Body = jBody.ToString()
            };
        }

        /// <summary>Deep probes json to confirm if two pieces of Json are identical if not Json then normal Ordinal String comparison.</summary>
        /// <param name="actual">The actual.</param>
        /// <param name="expected">The expected.</param>
        /// <returns>True if the actual and expected are identical.</returns>
        public static bool DoesPayloadMatch(string expected, string actual)
        {
            if (IsJson(expected))
            {
                var jExpected = JToken.Parse(expected);
                var jActual = JToken.Parse(actual);

                return JToken.DeepEquals(jActual, jExpected);
            }
            else
            {
                return actual.Equals(expected, StringComparison.Ordinal);
            }
        }
        /// <summary>Does the file payload match.</summary>
        /// <param name="expected">The expected payload.</param>
        /// <param name="path">The path to the file containing the payload</param>
        /// <returns>True if payloads match</returns>
        public static bool DoesFilePayloadMatch(string expected, string path)
        {
            return File.Exists(path) ? DoesPayloadMatch(expected, File.ReadAllText(path)) : false;
        }


        /// <summary>Determines whether the specified input is json.</summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is json; otherwise, <c>false</c>.</returns>
        internal static bool IsJson(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            input = input.Trim();
            return (input.StartsWith("{", StringComparison.OrdinalIgnoreCase) && input.EndsWith("}", StringComparison.OrdinalIgnoreCase))
                || (input.StartsWith("[", StringComparison.OrdinalIgnoreCase) && input.EndsWith("]", StringComparison.OrdinalIgnoreCase));
        }
    }
}
