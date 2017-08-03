using System.Threading.Tasks;
using BlogCore.AccessControl.Domain;
using MediatR;

namespace BlogCore.AccessControl.UseCases.UpdateUserProfileSetting
{
    public class UpdateUserProfileSettingInteractor 
        : IAsyncRequestHandler<UpdateUserProfileSettingRequest, UpdateUserProfileSettingResponse>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserProfileSettingInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UpdateUserProfileSettingResponse> Handle(UpdateUserProfileSettingRequest request)
        {
            await _userRepository.UpdateUserProfile(
                request.UserId,
                request.GivenName,
                request.FamilyName,
                request.Bio,
                request.Company,
                request.Location);

            return new UpdateUserProfileSettingResponse();
        }
    }
}