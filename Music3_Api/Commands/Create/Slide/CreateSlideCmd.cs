using MediatR;
using Music3_Core.DomainModels;
using System.Runtime.Serialization;

namespace Music3_Api.Commands.Create.Slide
{
    public partial class CreateSlideCmd: IRequest<bool>
    {
        [DataMember]
        public SlideDomainModel SlideItems { get; set; }
    }
}
