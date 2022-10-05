using FluxoDeCaixa.Caixa.Domain.Entities;
using FluxoDeCaixa.Caixa.Domain.Repositories;
using FluxoDeCaixa.Caixa.Infra.Data.Repositories;
using AutoMapper;
using Core.Bus;
using Core.Bus.Messages;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluxoDeCaixa.Caixa.BackgroundServices
{
    public class UserRegistryIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public UserRegistryIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider, IMapper mapper)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UserMessage, ResponseMessage>(async request =>
               await UserRegistry(request));

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

        private async Task<ResponseMessage> UserRegistry(UserMessage message)
        {
            ValidationResult result = new ValidationResult();

            if (!message.IsValid())
                result = message.ValidationResult;
            else
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                    var user = await userRepository.GetUserByCpf(message.Cpf);

                    if (user != null)
                        result.Errors.Add(new ValidationFailure("Cpf", "Já existe um usuário cadastrado com o CPF informado"));
                    else
                    {
                        userRepository.Insert(_mapper.Map<User>(message));

                        bool success = await userRepository.UnitOfWork.Commit();

                        if (!success)
                            result.Errors.Add(new ValidationFailure(string.Empty, "Ocorreu um erro ao tentar salvar os dados na base"));
                    }
                }
            }

            return new ResponseMessage(result);            
        }
    }
}
