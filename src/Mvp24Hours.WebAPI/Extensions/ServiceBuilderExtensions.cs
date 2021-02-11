﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Mvp24Hours.Core.Contract.Data;
using Mvp24Hours.Core.Contract.Infrastructure.Contexts;
using Mvp24Hours.Core.Contract.Logic;
using Mvp24Hours.Core.Contract.Logic.DTO;
using Mvp24Hours.Core.Mappings;
using Mvp24Hours.Core.ValueObjects.Logic;
using Mvp24Hours.Infrastructure.Contexts;
using Mvp24Hours.Infrastructure.Data;
using Mvp24Hours.WebAPI.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace Mvp24Hours.WebAPI.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceBuilderExtensions
    {
        /// <summary>
        /// Adds essential services
        /// </summary>
        public static IServiceCollection AddMvp24HoursService(this IServiceCollection services)
        {
            #region [ HttpContext ]
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region [ Notification ]
            services.AddScoped<INotificationContext, NotificationContext>();
            services.AddMvc(options => options.Filters.Add<NotificationFilter>());
            #endregion

            #region [ Hateaos ]
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddScoped<IUrlHelper>(x => x.GetRequiredService<IUrlHelperFactory>()
                .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));
            #endregion

            return services;
        }

        /// <summary>
        /// Add database context services
        /// </summary>
        public static IServiceCollection AddMvp24HoursDbService(this IServiceCollection services, params Type[] TDbContexts)
        {
            foreach (var db in TDbContexts)
            {
                services.AddScoped(db.GetType(), db);
            }

            services.AddScoped<IUnitOfWork>(x => new UnitOfWork());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

        /// <summary>
        /// Add business services (result / criteria / paging)
        /// </summary>
        public static IServiceCollection AddMvp24HoursBsService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBusinessResult<>), typeof(BusinessResult<>));
            services.AddScoped(typeof(ILinkResult), typeof(LinkResult));
            services.AddScoped(typeof(IMessageResult), typeof(MessageResult));
            services.AddScoped(typeof(IPageResult), typeof(PageResult));
            services.AddScoped(typeof(IPagingCriteria), typeof(PagingCriteria));
            services.AddScoped(typeof(IPagingCriteriaExpression<>), typeof(PagingCriteriaExpression<>));
            services.AddScoped(typeof(IPagingResult<>), typeof(PagingResult<>));
            services.AddScoped(typeof(ISummaryResult), typeof(SummaryResult));

            return services;
        }

        /// <summary>
        /// Add mapping services
        /// </summary>
        public static IServiceCollection AddMvp24HoursMapService(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddMvp24HoursJsonSerializer(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd"
            };
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection Add24Mapping(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection Add24Documentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer API", Version = "v1" });

                //c.DocumentFilter<CustomSwaggerFilter>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection Add24Compreesion(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            return services;
        }
    }
}
