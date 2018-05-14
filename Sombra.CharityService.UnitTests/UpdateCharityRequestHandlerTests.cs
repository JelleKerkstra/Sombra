﻿using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sombra.Messaging.Events;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityService.DAL;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class UpdateCharityRequestHandlerTests
    {
        [TestMethod]
        public async Task UpdateCharityRequest_Handle_Returns_Charity()
        {

            CharityContext.OpenInMemoryConnection();

            try
            {
                var busMock = new Mock<IBus>();
                busMock.Setup(m => m.PublishAsync(It.IsAny<UpdateCharityEvent>())).Returns(Task.FromResult(true));

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                }

                UpdateCharityResponse response;
                var newKey = Guid.NewGuid();
                var request = new UpdateCharityRequest()
                {
                    CharityKey = newKey,
                    NameCharity = "0",
                    NameOwner = "0",
                    EmailCharity = "0",
                    Category = Core.Enums.Category.None,
                    KVKNumber = 0,
                    IBAN = "0-IBAN",
                    CoverImage = "",
                    Slogan = "0"
                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();
                    context.Charities.Add(new CharityEntity
                    {
                        CharityKey = newKey,
                        NameCharity = "0",
                        NameOwner = "0",
                        EmailCharity = "testEmail",
                        Category = Core.Enums.Category.Dierenbescherming,
                        KVKNumber = 1,
                        IBAN = "1111-1111",
                        CoverImage = "x",
                        Slogan = "Test2"

                    });

                    context.SaveChanges();
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new UpdateCharityRequestHandler(context, Helper.GetMapper(), busMock.Object);
                    response = await handler.Handle(request);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.AreEqual(request.CharityKey, context.Charities.Single().CharityKey);
                    Assert.AreEqual(request.NameCharity, context.Charities.Single().NameCharity);
                    Assert.AreEqual(request.NameOwner, context.Charities.Single().NameOwner);
                    Assert.AreEqual(request.EmailCharity, context.Charities.Single().EmailCharity);
                    Assert.AreEqual(request.Category, context.Charities.Single().Category);
                    Assert.AreEqual(request.KVKNumber, context.Charities.Single().KVKNumber);
                    Assert.AreEqual(request.IBAN, context.Charities.Single().IBAN);
                    Assert.AreEqual(request.CoverImage, context.Charities.Single().CoverImage);
                    Assert.AreEqual(request.Slogan, context.Charities.Single().Slogan);
                    Assert.IsTrue(response.Success);
                }

                busMock.Verify(m => m.PublishAsync(It.Is<UpdateCharityEvent>(e => e.CharityKey == request.CharityKey && e.NameCharity == request.NameCharity)), Times.Once);
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }
    }
}