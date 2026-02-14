using IDP.Application.Commands.User;
using IDP.Domain.IRepository.Command;
using MediatR;
using Shared.Domain.Interface.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Handlers.Command.User
{
    public class UserHandler : IRequestHandler<UserCommand, bool>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserHandler(IUserCommandRepository userCommandRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userCommandRepository = userCommandRepository;
        }
        public async Task<bool> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            //var user=new User()
            
            return true;
        }
    }
}
