
using System.Text;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Repository;
using MagicVila_VillaAPi.Repository.IRepository;
using MagicVila_VillaAPi.Repository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MagicVila_VillaAPi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IVillaRepository, VillaRepository>();
            builder.Services.AddScoped<IRepositoryVillanum, RepositoryVillanum>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
            AddEntityFrameworkStores<ApplicationdbContext>().AddDefaultTokenProviders();
            builder.Services.AddResponseCaching();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            var Key = builder.Configuration.GetValue<string>("ApiSetting:SecretKey");
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Defult",
                    new CacheProfile()
                    {
                        Duration = 30
                    });
            } ).AddNewtonsoftJson();
           
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Magic Villa",
                    Description = "Api to Manage Villa"

                }); 
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Magic Villa",
                    Description = "Api to Manage Villa version 2"

                });
                // ????? ????? JWT Authorization
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.\nExample: \"Bearer abc123\""
                });

                // ????? Authorization ??? ???? ??? Endpoints
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                    new string[] { }
                 }
                });
              
            });
            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0); // الإصدار الافتراضي
                options.AssumeDefaultVersionWhenUnspecified = true; // إذا لم يتم تحديد الإصدار
                options.ReportApiVersions = true; // عرض الإصدارات المتاحة
            });

            builder.Services.AddDbContext<ApplicationdbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic_VillaV1");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Magic_VillaV2");
                });
            }
 
            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
