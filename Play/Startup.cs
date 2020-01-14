using DatabaseUtils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Play
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //redis����
            var section = Configuration.GetSection("Redis:Default");
            string _connectionString = section.GetSection("Connection").Value;//�����ַ���
            string _instanceName = section.GetSection("InstanceName").Value; //ʵ������
            int _defaultDB = int.Parse(section.GetSection("DefaultDB").Value ?? "0"); //Ĭ�����ݿ�           
            services.AddSingleton(new RedisCommon(_connectionString, _instanceName, _defaultDB));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            //services.AddSwaggerGen(option =>
            //{
            //    option.SwaggerDoc("sparktodo", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "SparkTodo API",
            //        Description = "API for SparkTodo",
            //        Contact = new OpenApiContact() { Name = "WeihanLi", Email = "weihanli@outlook.com" }
            //    });

            //    // include document file
            //    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml"), true);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder =>builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                // c.RoutePrefix = string.Empty;
            });
        }
    }
}
