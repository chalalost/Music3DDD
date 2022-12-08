using AutoMapper;
using MediatR;
using Music3_Core.Extension.IRepositories;
using System.Threading.Tasks;
using System.Threading;
using System;
using Music3_Core.DomainModels;

namespace Music3_Api.Commands.Create.Slide
{
    public partial class CreateSlideCmdHandler : IRequestHandler<CreateSlideCmd, bool>
    {
        private readonly IRepositoryEF<SlideDomainModel> _repository;
        private readonly IMapper _mapper;

        public CreateSlideCmdHandler(IRepositoryEF<SlideDomainModel> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateSlideCmd request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<SlideDomainModel>(request);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
