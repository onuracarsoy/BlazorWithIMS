﻿using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Reports
{
    public class SearchInventoryTransactionsUseCase : ISearchInventoryTransactionsUseCase
    {
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;

        public SearchInventoryTransactionsUseCase(IInventoryTransactionRepository inventoryTransactionRepository)
        {
            this._inventoryTransactionRepository = inventoryTransactionRepository;
        }

        public async Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
        {
            if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);
            return await _inventoryTransactionRepository.GetInventoryTransactionAsync(inventoryName, dateFrom, dateTo, transactionType);
        }
    }
}
