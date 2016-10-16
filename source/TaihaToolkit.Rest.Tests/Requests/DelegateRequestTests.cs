using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Rest.Requests;

namespace Studiotaiha.Toolkit.Rest.Tests.Requests
{
	[TestClass]
	public class DelegateRequestTests
	{
		[TestMethod]
		public void ConstructorTest()
		{
			var methods = new HttpMethod[] {
				HttpMethod.Get,
				HttpMethod.Post,
				HttpMethod.Put,
				HttpMethod.Delete,
			};

			foreach (var method in methods) {
				var path = Guid.NewGuid().ToString();
				var acceptContentTypes = new[]{
					PreDefinedAcceptContentTypes.Json
				};
				var request = new DelegateRequest<object, object, object>(method, path, acceptContentTypes);

				Assert.AreEqual(method, request.Method);
				Assert.AreEqual(path, request.Path);
				Assert.IsTrue(Enumerable.SequenceEqual(acceptContentTypes, request.AcceptContentTypes));
			}
		}

		[TestMethod]
		public async Task HandlersTest()
		{
			var configureHeaderCalled = false;
			var configureParameterCalled = false;
			var parseFailureResultCalled = false;
			var parseSuccessResultCalled = false;
			var expectedParameter = new object();
			var expectedParameterBag = new StubParameterBag();
			var expectedHeaderBag = new StubHeaderBag();
			var expectedStatusCode = HttpStatusCode.OK;
			var expectedRequestResult = new StubRequestResult();
			var expectedSuccessResult = new object();
			var expectedFailureResult = new object();

			var request = new DelegateRequest<object, object, object>(HttpMethod.Post, string.Empty) {
				ConfigureHeaderAsyncHandler = (headerBag, parameter) => {
					Assert.AreEqual(expectedHeaderBag, headerBag);
					Assert.AreEqual(expectedParameter, parameter);
					configureHeaderCalled = true;

					return Task.FromResult(0);
				},
				ConfigureParameterAsyncHandler = (parameterBag, parameter) => {
					Assert.AreEqual(expectedParameterBag, parameterBag);
					Assert.AreEqual(expectedParameter, parameter);
					configureParameterCalled = true;

					return Task.FromResult(0);
				},
				ParseSuccessResultAsyncHandler = (statusCode, requestResult) => {
					Assert.AreEqual(expectedStatusCode, statusCode);
					Assert.AreEqual(expectedRequestResult, requestResult);
					parseSuccessResultCalled = true;

					return Task.FromResult<object>(expectedSuccessResult);
				},
				ParseFailureResultAsyncHandler = (statusCode, requestResult) => {
					Assert.AreEqual(expectedStatusCode, statusCode);
					Assert.AreEqual(expectedRequestResult, requestResult);
					parseFailureResultCalled = true;

					return Task.FromResult<object>(expectedFailureResult);
				},
			};

			await request.ConfigureHeaderAsync(expectedHeaderBag, expectedParameter);
			await request.ConfigureParameterAsync(expectedParameterBag, expectedParameter);
			var successResult = await request.ParseSuccessResultAsync(expectedStatusCode, expectedRequestResult);
			var failureResult = await request.ParseFailureResultAsync(expectedStatusCode, expectedRequestResult);

			Assert.IsTrue(configureHeaderCalled);
			Assert.IsTrue(configureParameterCalled);
			Assert.IsTrue(parseSuccessResultCalled);
			Assert.IsTrue(parseFailureResultCalled);

			Assert.AreEqual(expectedSuccessResult, successResult);
			Assert.AreEqual(expectedFailureResult, failureResult);
		}
	}
}
