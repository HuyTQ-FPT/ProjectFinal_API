
using FinalAPI.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;

namespace FinalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Start
            builder.Services.AddSwaggerGen();
			builder.Services.AddControllers().AddOData(opt => opt.Count().Filter().Expand().Select().OrderBy());
			builder.Services.AddDbContext<FinalPRN231Context>(options =>
	            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            //{
            //    opt.RequireHttpsMetadata = false;
            //    opt.SaveToken = true;
            //    opt.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //        ValidAudience = builder.Configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            //    };
            //});
            //--------------------
            builder.Services.AddCors(act =>
            {
                act.AddPolicy("_MainPolicy", options =>
                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowAnyOrigin();
                });
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("_MainPolicy");
            app.MapControllers();

            //End

            app.Run();
        }
    }
}