using Dapper;
using MediatR;
using Music3_Api.Paging;
using Music3_Core.DomainModels;
using Music3_Core.Extension.IRepositories;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;
using Music3_Core.Extension;

namespace Music3_Api.Commands.Paging.Slide
{
    public class SlidePaginatedCmdHandler : IRequestHandler<SlidePaginatedCmd, IPaginatedList<SlideDomainModel>>
    {
        private readonly IDapperExtension _repository;
        private readonly IPaginatedList<SlideDomainModel> _list;

        public SlidePaginatedCmdHandler(IDapperExtension repository, IPaginatedList<SlideDomainModel> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<SlideDomainModel>> Handle(SlidePaginatedCmd request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim() ?? "";
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM ( ");

            sbCount.Append(" select Id ");
            sbCount.Append(" from Slides where ");

            StringBuilder sb = new StringBuilder();

            sb.Append(" select *");
            sb.Append(" from Slides where ");
            
            if (!string.IsNullOrEmpty(request.KeySearch))
            {
                sb.Append("  (Name like @key or Description like @key) and ");
                sbCount.Append("  (Name like @key or Description like @key) and ");
            }
            sb.Append("  Status = 0  ");
            sbCount.Append("  Status = 0  ");
            //
            sbCount.Append(" ) t   ");
            sb.Append(" order by Id OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@skip", request.Skip);
            parameter.Add("@take", request.Take);
            Console.WriteLine(sb.ToString());
            _list.Result = await _repository.GetAllAync<SlideDomainModel>(sb.ToString(), parameter, CommandType.Text);
            _list.TotalCount = await _repository.GetAyncFirst<int>(ValidatorString.GetSqlCount(sb.ToString(), SqlEnd: "order"), parameter, CommandType.Text);
            return _list;
        }
    }
}
