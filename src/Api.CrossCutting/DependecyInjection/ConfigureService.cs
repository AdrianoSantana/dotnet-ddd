using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependecyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependeciesService(IServiceCollection serviceCollection)
        {
            // Transient --> Para cada operação que tiver 
            // uma injeção de dependência ele irá criar uma instância de user

            // Scopped --> Ele cria apenas uma única instância que fica vivo em um único ciclo de vida
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
        }
    }
}