using Auth;
using IDP.Application.Queries.Auth;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Query;
using MediatR;
using Shared.Domain.Interface.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Handlers.Query.Auth
{
    public class AuthHandler : IRequestHandler<AuthQuery, JsonWebToken>
    {
        private readonly IOtpRedisQueryRepository _otpQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHandler _jwtHandler;

        public AuthHandler(
            IOtpRedisQueryRepository otpQueryRepository,
            IUserCommandRepository userCommandRepository,
            IUserQueryRepository userQueryRepository,
            IUnitOfWork unitOfWork,
            IJwtHandler jwtHandler)
        {
            _otpQueryRepository = otpQueryRepository;
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
        }
        public async Task<JsonWebToken> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
            // 1️⃣ Check OTP in Redis
            var isValid = await _otpQueryRepository
                .IsValidAsync(request.MobileNumber, request.OtpCode);

            if (!isValid)
                throw new UnauthorizedAccessException("OTP is invalid");

            // 2️⃣ Update User in SQL
            var user = await _userQueryRepository
                .GetByPhoneNumberAsync(request.MobileNumber);

            user.IsPhoneNumberConfirmed = true;
            user.PhoneConfirmedAt = DateTime.UtcNow;
            
             _userCommandRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            // 3️⃣ Create JWT ✅
            return _jwtHandler.Create(user.ID); // 
        }

        
    }
}
