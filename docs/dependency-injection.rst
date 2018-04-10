Dependency Injection
====================

What is Dependency Injection
****************************
Dependency injection is basically providing the objects that an object needs (its dependencies) instead of having it construct them itself.  It's a very useful technique for testing, since it allows dependencies to be mocked or stubbed out.

Dependencies can be injected into objects by many means (such as constructor injection or setter injection).  One can even use specialized dependency injection frameworks (e.g. SimpleInjector, Autofac, StructureMap) to do that, but they certainly aren't required. 

The .net core framework comes with a built in DI container. We will use this DI container to illustrate how to configure and inject Threenine.Data.

Once you have added the Nuget Package to your project, you can edit your `Startup.cs`  and import `using Threenine.Data.DependencyInjection;`

::

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
            // Use the Threenine.Data Dependency Injection to set up the Unit of Work
            services.AddDbContext<SampleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SampleDB"))).AddUnitOfWork<SampleContext>();

            services.AddMvc();
        }

Check out an example using Threenine.Data `Sample MVC Application<https://github.com/threenine/Threenine.Data/blob/master/samples/SampleCoreMVCWebsite/Startup.cs>`





