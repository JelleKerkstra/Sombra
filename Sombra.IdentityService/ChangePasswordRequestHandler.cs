using System.Threading.Tasks;
using Sombra.Core;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Infrastructure;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Sombra.IdentityService
{
    public class ChangePasswordRequestHandler : IAsyncRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
    {
        private readonly AuthenticationContext _context;
        public ChangePasswordRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest message)
        {
            
            Console.WriteLine("ChangePasswordRequest received");
            var response = new ChangePasswordResponse(false);

            var credential = await _context.Credentials.Select(b => b).Where(c => c.SecurityToken == message.SecurityToken && c.ExpirationDate > DateTime.Now).FirstOrDefaultAsync(); 
            if(credential != null){
                credential.Secret = message.Password;
                credential.SecurityToken = String.Empty;
                _context.SaveChanges();
                response.Success = true;
           }           

            return response;
        }
    }
}
