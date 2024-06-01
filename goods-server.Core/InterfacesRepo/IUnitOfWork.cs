using goods_server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Core.InterfacesRepo
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepo AccountRepo { get; }
        ICategoryRepo CategoryRepo { get; }
        ICityRepo CityRepo { get; }
        ICommentRepo CommentRepo { get; }
        IGenreRepo GenreRepo { get; }
        IOrderDetailRepo OrderDetailRepo { get; }
        IOrderRepo OrderRepo { get; }
        IProductRepo ProductRepo { get; }
        IRatingRepo RatingRepo { get; }
        IReplyCommentRepo ReplyCommentRepo { get; }
        IReportRepo ReportRepo { get; }
        IRequestHistoryRepo RequestHistoryRepo { get; }
        IRoleRepo RoleRepo { get; }

        Task<int> SaveAsync();
    }
}
