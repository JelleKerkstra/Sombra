using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityService.DAL;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class GetCharityByKeyRequestHandlerTests
    {
        [TestMethod]
        public async Task GetCharityByKeyRequest_Handle_Returns_Charity()
        {
            CharityContext.OpenInMemoryConnection();
            try
            {
                //Arrange
                var key = Guid.NewGuid();
                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();

                    var charity = new Charity
                    {
                        CharityKey = key,
                        Name = "testNameCharity",
                        OwnerUserName = "testNAmeOwner",
                        Email = "test@test.com",
                        Category = Core.Enums.Category.None,
                        KVKNumber = 0,
                        IBAN = "0-IBAN",
                        CoverImage = "",
                        Slogan = "Test"

                    };

                    context.Add(charity);
                    context.SaveChanges();

                }
                var request = new GetCharityByKeyRequest()
                {
                    CharityKey = key
                };

                GetCharityByKeyResponse response;

                //Act
                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new GetCharityByKeyRequestHandler(context, Helper.GetMapper());
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityContext.GetInMemoryContext())
                {
                    // TODO Fix unit test problem
                    Assert.AreEqual(response.CharityKey, request.CharityKey);
                    Assert.AreEqual(response.Name, context.Charities.Single().Name);
                    Assert.AreEqual(response.OwnerUserName, context.Charities.Single().OwnerUserName);
                    Assert.AreEqual(response.Email, context.Charities.Single().Email);
                    Assert.AreEqual(response.Category, context.Charities.Single().Category);
                    Assert.AreEqual(response.KVKNumber, context.Charities.Single().KVKNumber);
                    Assert.AreEqual(response.IBAN, context.Charities.Single().IBAN);
                    Assert.AreEqual(response.CoverImage, context.Charities.Single().CoverImage);
                    Assert.AreEqual(response.Slogan, context.Charities.Single().Slogan);
                    Assert.IsTrue(response.Success);
                }
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityByKeyRequest_Handle_Returns_Null()
        {
            CharityContext.OpenInMemoryConnection();
            try
            {
                //Arrange

                using (var context = CharityContext.GetInMemoryContext())
                {
                    context.Database.EnsureCreated();

                    var charity = new Charity
                    {
                        CharityKey = Guid.NewGuid(),
                        Name = "testNameCharity",
                        OwnerUserName = "testNAmeOwner",
                        Email = "test@test.com",
                        Category = Core.Enums.Category.None,
                        KVKNumber = 0,
                        IBAN = "0-IBAN",
                        CoverImage = "",
                        Slogan = "Test"

                    };

                    context.Add(charity);
                    context.SaveChanges();

                }
                var request = new GetCharityByKeyRequest();

                GetCharityByKeyResponse response;

                //Act
                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new GetCharityByKeyRequestHandler(context, Helper.GetMapper());
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.IsFalse(response.Success);
                }
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }
    }
}