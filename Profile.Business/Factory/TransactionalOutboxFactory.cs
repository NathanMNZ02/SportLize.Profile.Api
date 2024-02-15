using SportLize.Profile.Api.Profile.Repository.Model;
using SportLize.Profile.Api.Profile.Shared.Dto;
using System.Text.Json;
using Utility.Kafka.Constants;
using Utility.Kafka.Messages;

namespace SportLize.Profile.Api.Profile.Business.Factory
{
    public class TransactionalOutboxFactory
    {
        #region CREATE
        public static TransactionalOutbox CreateInsert(UserReadDto userReadDto) => Create(userReadDto, Operations.Insert);
        public static TransactionalOutbox CreateUpdate(UserReadDto userReadDto) => Create(userReadDto, Operations.Update);
        public static TransactionalOutbox CreateDelete(UserReadDto userReadDto) => Create(userReadDto, Operations.Delete);
        #endregion

        private static TransactionalOutbox Create(UserReadDto userReadDto, string operation) => Create(nameof(User), userReadDto, operation);
        private static TransactionalOutbox Create<TDTO>(string table, TDTO dto, string operation) where TDTO : class, new()
        {
            OperationMessage<TDTO> operationMessage = new OperationMessage<TDTO>()
            {
                Dto = dto,
                Operation = operation
            };

            operationMessage.CheckMessage();

            return new TransactionalOutbox()
            {
                Table = table,
                Message = JsonSerializer.Serialize(operationMessage)
            };
        }
    }
}
