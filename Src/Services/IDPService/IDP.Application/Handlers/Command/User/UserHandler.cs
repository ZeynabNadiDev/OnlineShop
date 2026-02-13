using IDP.Application.Commands.User;
using IDP.Domain.IRepository.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Handlers.Command.User
{
    public class UserHandler : IRequestHandler<UserCommand, bool>
    {
        public async Task<bool> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            
            return true;
        }
    }
}
