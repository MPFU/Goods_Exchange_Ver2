using Azure.Storage.Blobs;
using goods_server.Core.InterfacesRepo;
using goods_server.Infrastructure.AutoMaper;
using goods_server.Infrastructure.Repositories;
using goods_server.Service.InterfaceService;
using goods_server.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IReplyCommentService, ReplyCommentService>();
            services.AddScoped<IRequestHistoyService, RequestHistoryService>();
            services.AddScoped<IAzureBlobStorage,AzureBlobService>();
            services.AddScoped<IVNPay,VNPayService>();

            services.AddScoped(x => new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage")));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }
    }
}
