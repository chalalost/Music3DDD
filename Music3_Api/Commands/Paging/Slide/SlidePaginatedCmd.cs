using MediatR;
using Music3_Api.Paging;
using Music3_Core.DomainModels;
using Music3_Core.ViewModels;

namespace Music3_Api.Commands.Paging.Slide
{
    public class SlidePaginatedCmd : BaseSearchModel, IRequest<IPaginatedList<SlideDomainModel>>
    {
    }
}
