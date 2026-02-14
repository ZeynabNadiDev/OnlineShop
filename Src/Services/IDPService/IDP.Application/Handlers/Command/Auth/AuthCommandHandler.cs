using IDP.Application.Commands.Auth;
using IDP.Application.Helper;
using IDP.Domain.IRepository.Query;
using IDP.Domain.IRepository.Command;
using MediatR;
using IDP.Domain.Entities;
using Shared.Domain.Interface.UOW;
using DotNetCore.CAP;
using EventMessages.Events;

namespace IDP.Application.Handlers.Command.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, bool>
    {
        private readonly IOtpRedisRepository _otpRedisRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndPoint _publishEndPoint;
        //private readonly ICapPublisher _capPublisher;

        internal AuthCommandHandler(IOtpRedisRepository otpRedisRepository,
            IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository,
            IUnitOfWork unitOfWork,IPublishEndPoint publishEndPoint)
        {
          _otpRedisRepository = otpRedisRepository;
            _userQueryRepository = userQueryRepository;
            _userCommandRepository = userCommandRepository;
            _unitOfWork = unitOfWork;

            _publishEndPoint = publishEndPoint;


        }
        public async Task <bool>  Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _userQueryRepository.GetByPhoneNumberAsync(request.MobileNumber);
            if (user == null)
            {
                user = new Domain.Entities.User
                {
                    PhoneNumber = request.MobileNumber,
                    IsPhoneNumberConfirmed = false,
                    IsProfileCompleted = false
                };

                await _userCommandRepository.AddAsync(user);
              await  _unitOfWork.SaveChangeAsync(cancellationToken);
            }
            await _otpRedisRepository.AddAsync(new Domain.DTO.Otp
            {
                MobileNumber = request.MobileNumber,
                OtpCode = OtpGenerator.Generate(),
                IsUse = false
            });
            //await _capPublisher.PublishAsync<AuthCommand>("otpevent", new OtpEvent
            //{
            //    MobileNumber = request.MobileNumber, OtpCode = OtpGenerator.Generate()

            //});
            
            return true;
        }
    }

    internal interface IPublishEndPoint
    {
    }
}
