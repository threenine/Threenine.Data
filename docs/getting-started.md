# Getting Started

## What is Threenine.Data

A Generic Unit of Work (UOW) and Repository pattern implementation framework for Entity Framework Core,  to assist developers to implementing the [Generic Repository Pattern .net core](https://garywoodfine.com/generic-repository-pattern-net-core).


## Installation
The simplest method to install Threenine.Data into your solution is to make use of Nuget:

```jshelllanguage
 nuget Install-Package Threenine.Data
```

or via the Dotnet CLI
```jshelllanguage
   dotnet add package Threenine.Data
```
Check out the [Nuget package details](https://www.nuget.org/packages/Threenine.Data) for more details.

### Dependency Injection

ASP.NET Core supports the dependency injection (DI) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies.

### What is Dependency Injection

Dependency injection is basically a mechanism of providing objects that an object needs (its dependencies) instead of having it construct them itself.  It's a very useful technique for testing, since it allows dependencies to be mocked or stubbed out.

Dependencies can be injected into objects by many means (such as constructor injection or setter injection).  One can even use specialized dependency injection frameworks (e.g. SimpleInjector, Autofac, StructureMap) to do that, but they certainly aren't required. 

The .net core framework comes with a built in DI container. Threenine.Data is able to be used as an extension of the DI container and you can use this to configure and inject Threenine.Data and its Unit of Work interface as a dependency.

Dependency injection addresses a few issues in software development

- The use of an interface or base class to abstract the dependency implementation.
- Registration of the dependency in a service container. ASP.NET Core provides a built-in service container, `IServiceProvider`. Services are registered in the applications `Startup.ConfigureServices` method.
- Injection of the service into the constructor of the class where it's used. The framework takes on the responsibility of creating an instance of the dependency and disposing of it when it's no longer needed.


## How to use Dependency Injection

Once you have added the Nuget Package to your project, you can edit your `Startup.cs`  and import `using Threenine.Data.DependencyInjection;`

In the example we are just going to use a connection string that we have declared in our `appsettings.json` file which we have simply called `SampleDB`. 

We make use of any of the database providers supported by Entity Framework Core i.e. mySQL, Postgres SQL, Oracle, MS SQL etc.

We have made use of PostgreSQL in the example for ease of illustration. You would need to add your database context as normal, and once done you can make use of the `AddUnitOfWork` which will then configure the Unit of Work to be available via dependency injection throughout your application.

```c#
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
                options.UseNpgsql(Configuration.GetConnectionString("SampleDB")))
                .AddUnitOfWork<SampleContext>();

            services.AddMvc();
        }
```

Once the Dependency Injection has been configured. You can now simply make use of the Unit of Work to access your 
repositories via Dependency Injection.

```c#

 public class AddressService : IService<Address>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Address GetAddressDetail(int id)
        {
           return _unitOfWork.GetReadOnlyRepository<Address>().SingleOrDefault(x => x.Id == id);
           
        }
    }

```

All functionality can access via thr various types of repositories that are made available. You are to include your Generic Unit of Work and Repository pattern in other Generic methods.

```c#
  private async Task<SingleResponse<Response>> Create<T>(T entity) where T : class
        {
            try
            {
                var created = _unitOfWork.GetRepository<T>().Insert(entity);
                await _unitOfWork.CommitAsync();
                return new SingleResponse<Response>(_mapper.Map<Response>(created));
            }
            catch(DbUpdateException e)
            {
                return new SingleResponse<Response>(null, new List<KeyValuePair<string, string[]>>()
                {
                    new(ErrorKeyNames.Conflict, new []{ e.InnerException.Message})
                });
            }
        }
```




