using Core.Bus;
using Core.Bus.Messages;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Caixa.BackgroundServices
{
    public class UserRegistryIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public UserRegistryIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UserMessage, ResponseMessage>(async request =>
               await RegistryUser(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegistryUser(UserMessage message)
        {
            //var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);
            ValidationResult sucesso = new ValidationResult();

            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            //    sucesso = await mediator.EnviarComando(clienteCommand);
            //}

            return new ResponseMessage(sucesso);
        }
    }
}
