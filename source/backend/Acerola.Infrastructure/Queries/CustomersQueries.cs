﻿namespace Acerola.Infrastructure.Queries
{
    using Acerola.Application.Queries;
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Acerola.Infrastructure.DataAccess;
    using Acerola.Application.DTO;
    using Acerola.Domain.Customers;
    using Acerola.Application.Mappers;

    public class CustomersQueries : ICustomersQueries
    {
        private readonly AccountBalanceContext mongoDB;
        private readonly ICustomersMapper mapper;

        public CustomersQueries(AccountBalanceContext mongoDB, ICustomersMapper mapper)
        {
            this.mongoDB = mongoDB;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CustomerData>> GetAll()
        {
            IEnumerable<Customer> data = await this.mongoDB.Customers
                .Find(e => true)
                .ToListAsync();

            List<CustomerData> result = new List<CustomerData>();

            foreach (Customer item in data)
            {
                CustomerData customerVM = this.mapper.Map(item);

                result.Add(customerVM);
            }

            return result;
        }

        public async Task<CustomerData> GetCustomer(Guid id)
        {
            Customer data = await this.mongoDB.Customers
                .Find(Builders<Customer>.Filter.Eq("_id", id))
                .SingleOrDefaultAsync();

            if (data == null)
                throw new CustomerNotFoundException($"The account {id} does not exists or is not processed yet.");

            CustomerData customerVM = this.mapper.Map(data);

            return customerVM;
        }
    }
}
