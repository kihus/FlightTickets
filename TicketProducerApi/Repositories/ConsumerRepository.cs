using FlightTickets.ConsumerApi.Repositories.Interfaces;
using FlightTickets.Models.Entities;
using Infrastructure.Data.Mongo.Contexts;
using MongoDB.Driver;

namespace FlightTickets.ConsumerApi.Repositories
{
    public class ConsumerRepository(
        MongoDbContext mongoDatabase
        ) : IConsumerRepository
    {
        private readonly IMongoCollection<Ticket> _approvedTicketsCollection = mongoDatabase.ApprovedTickets;
        private readonly IMongoCollection<Ticket> _deniedTicketsCollection = mongoDatabase.DeniedTickets;
        public async Task SaveTicketApproved(Ticket ticket)
        {
            try
            {
                await _approvedTicketsCollection.InsertOneAsync(ticket);
            }
            catch (MongoException mongoEx)
            {
                throw new MongoException(mongoEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task SaveTicketDenied(Ticket ticket)
        {
            try
            {
                await _deniedTicketsCollection.InsertOneAsync(ticket);
            }
            catch (MongoException mongoEx)
            {
                throw new MongoException(mongoEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
