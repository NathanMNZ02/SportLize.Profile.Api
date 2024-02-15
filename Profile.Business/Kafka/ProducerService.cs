using Microsoft.Extensions.Options;
using SportLize.Profile.Api.Profile.Repository.Abstraction;
using SportLize.Profile.Api.Profile.Repository.Model;
using Utility.Kafka.Abstractions.Clients;
using Utility.Kafka.Services;

namespace SportLize.Profile.Api.Profile.Business.Kafka
{
    public class ProducerService : ProducerService<KafkaTopicsOutput>
    {
        public ProducerService(ILogger<ProducerService> logger, IProducerClient producerClient, IAdministatorClient adminClient, IOptions<KafkaTopicsOutput> optionsTopics, IOptions<KafkaProducerServiceOptions> optionsProducerService, IServiceScopeFactory serviceScopeFactory)
            : base(logger, producerClient, adminClient, optionsTopics, optionsProducerService, serviceScopeFactory) { /*BLANK*/ }

        protected override async Task OperationsAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = ServiceScopeFactory.CreateScope();
            IRepository repository = scope.ServiceProvider.GetRequiredService<IRepository>();


            Logger.LogInformation("Acquisizione dei TransactionalOutbox da elaborare...");
            IEnumerable<TransactionalOutbox> transactionalOutboxList = (await repository.GetAllTransactionalOutbox(cancellationToken)).OrderBy(x => x.Id);
            if (!transactionalOutboxList.Any())
            {
                Logger.LogInformation($"Non ci sono TransactionalOutbox da elaborare");
                return;
            }

            Logger.LogInformation("Ci sono {Count} TransactionalOutbox da elaborare", transactionalOutboxList.Count());

            foreach (TransactionalOutbox tran in transactionalOutboxList)
            {
                string groupMsg = $"del record {nameof(TransactionalOutbox)} con " +
                        $"{nameof(TransactionalOutbox.Id)} = {tran.Id}, " +
                        $"{nameof(TransactionalOutbox.Table)} = '{tran.Table}' e " +
                        $"{nameof(TransactionalOutbox.Message)} = '{tran.Message}'";

                Logger.LogInformation("Elaborazione {groupMsg}...", groupMsg);

                try
                {
                    Logger.LogInformation("Determinazione del topic...");
                    string topic = tran.Table switch
                    {
                        nameof(User) => KafkaTopics.User,
                        _ => throw new ArgumentOutOfRangeException($"La tabella {tran.Table} non è prevista come topic nel Producer")
                    };

                    Logger.LogInformation("Scrittura del messaggio Kafka sul topic '{topic}'...", topic);
                    await ProducerClient.ProduceAsync(topic, tran.Message, cancellationToken);

                    Logger.LogInformation("Eliminazione {groupMsg}...", groupMsg);
                    await repository.DeleteTransactionalOutbox(tran.Id, cancellationToken);

                    await repository.SaveChanges(cancellationToken);
                    Logger.LogInformation("Record eliminato");

                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Si è verificata un'eccezione nel metodo ProducerService.OperationsAsync durante l'elaborazione {groupMsg}: {ex}", groupMsg, ex);
                }
            }
        }
    }
}
