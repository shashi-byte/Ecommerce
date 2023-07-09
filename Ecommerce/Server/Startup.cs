using Microsoft.AspNetCore.Server.IISIntegration;

namespace Ecommerce.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<DAL.IDataRepository>(provider => new DAL.DataRepository(Configuration.GetConnectionString("Database")));

            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    // Configure the context to use Microsoft SQL Server.
            //    options.UseSqlServer(Configuration.GetConnectionString("Database"));

            //    // Register the entity sets needed by OpenIddict.
            //    // Note: use the generic overload if you need
            //    // to replace the default OpenIddict entities.
            //});
            services.AddCors();
            //services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(options =>
            //    {
            //        options.SaveToken = true;
            //        options.RequireHttpsMetadata = false;
            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidAudience = Configuration["JWT:ValidAudience"],
            //            ValidIssuer = Configuration["JWT:ValidIssuer"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
            //        };
            //    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                //endpoints.MapBlazorHub(options => { options.Transports = HttpTransportType.LongPolling; });
                //  endpoints.MapBlazorHub();
                //endpoints.MapFallbackToPage("/_Host");
                endpoints.MapFallbackToFile("index.html");

            });
        }
    }
}
