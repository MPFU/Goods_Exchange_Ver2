using AutoMapper;
using goods_server.Core.Interfaces;
using goods_server.Core.InterfacesRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoodsExchangeApplication2024DbContext _context;

        private readonly IAccountRepo _accountRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ICityRepo _cityRepo;
        private readonly ICommentRepo _commentRepo;
        private readonly IGenreRepo _genreRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderDetailRepo _orderDetailRepo;
        private readonly IProductRepo _productRepo;
        private readonly IRatingRepo _ratingRepo;
        private readonly IReplyCommentRepo _replyCommentRepo;
        private readonly IReportRepo _reportRepo;
        private readonly IRequestHistoryRepo _requestHistoryRepo;
        private readonly IRoleRepo _roleRepo;
        private readonly IMapper _mapper;

        public UnitOfWork(IMapper mapper)
        {
            _mapper = mapper;
            _context = new GoodsExchangeApplication2024DbContext();
            _accountRepo = new AccountRepository(_context);
            _categoryRepo = new CategoryRepository(_context);
            _cityRepo = new CityRepository(_context);   
            _commentRepo = new CommentRepository(_context);
            _genreRepo = new GenreRepository(_context);
            _orderDetailRepo = new OrderDetailRepository(_context);
            _orderRepo = new OrderRepository(_context);
            _productRepo = new ProductRepository(_context);
            _ratingRepo = new RatingRepository(_context);
            _replyCommentRepo = new ReplyCommentRepository(_context);
            _reportRepo = new ReportRepository(_context);
            _requestHistoryRepo = new RequestHistoryRepository(_context);
            _roleRepo = new RoleRepository(_context);
        }

        public IAccountRepo AccountRepo => _accountRepo;

        public ICategoryRepo CategoryRepo => _categoryRepo;

        public ICityRepo CityRepo => _cityRepo;

        public ICommentRepo CommentRepo => _commentRepo;

        public IGenreRepo GenreRepo => _genreRepo;

        public IOrderDetailRepo OrderDetailRepo => _orderDetailRepo;

        public IOrderRepo OrderRepo => _orderRepo;

        public IProductRepo ProductRepo => _productRepo;

        public IRatingRepo RatingRepo => _ratingRepo;

        public IReplyCommentRepo ReplyCommentRepo => _replyCommentRepo;

        public IReportRepo ReportRepo => _reportRepo;

        public IRequestHistoryRepo RequestHistoryRepo => _requestHistoryRepo;

        public IRoleRepo RoleRepo => _roleRepo;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
