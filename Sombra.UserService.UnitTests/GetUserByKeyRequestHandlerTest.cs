﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.User;
using Sombra.UserService.DAL;

namespace Sombra.UserService.UnitTests
{
    [TestClass]
    public class GetUserByKeyRequestHandlerTest
    {
        [TestMethod]
        public async Task GetUserByKeyRequestHandler_Handle_Returns_User()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                GetUserByKeyResponse response;
                var request = new GetUserByKeyRequest
                {
                    UserKey = Guid.NewGuid()
                };

                using (var context = UserContext.GetInMemoryContext())
                {
                    context.Users.Add(new User
                    {
                        UserKey = request.UserKey,
                        FirstName = "john",
                        LastName = "doe"
                    });

                    context.SaveChanges();
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new GetUserByKeyRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    Assert.IsTrue(response.UserExists);
                    Assert.AreEqual(context.Users.Single().FirstName, response.User.FirstName);
                    Assert.AreEqual(context.Users.Single().LastName, response.User.LastName);
                }
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetUserByKeyRequestHandler_Handle_Returns_NotExists()
        {
            UserContext.OpenInMemoryConnection();

            try
            {
                GetUserByKeyResponse response;
                var request = new GetUserByKeyRequest();

                using (var context = UserContext.GetInMemoryContext())
                {
                    context.Users.Add(new User
                    {
                        UserKey = Guid.NewGuid(),
                        FirstName = "john",
                        LastName = "doe"
                    });

                    context.SaveChanges();
                }

                using (var context = UserContext.GetInMemoryContext())
                {
                    var handler = new GetUserByKeyRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                Assert.IsFalse(response.UserExists);
                Assert.IsNull(response.User);
            }
            finally
            {
                UserContext.CloseInMemoryConnection();
            }
        }
    }
}