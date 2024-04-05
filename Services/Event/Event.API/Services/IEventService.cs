using System.Linq.Expressions;
using Event.API.Dtos;
using Event.API.Entities;
using SharedLib.Dtos;

namespace Event.API.Services
{
    public interface IEventService
    {
        Task<AppResponse<List<GetEventReponse>>> GetAllAsync();
        Task<AppResponse<GetEventReponse>> GetByIdAsync(Guid id);
        Task<AppResponse<CreatedEventResponse>> CreateAsync(CreateEventRequest request);
        Task<AppResponse<UpdatedEventResponse>> UpdateAsync(UpdateEventRequest request, Guid id);
        Task<AppResponse<NoContentResponse>> DeleteAsync(Guid id);
    }
}