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
    public class SearchProductTransactionUseCase : ISearchProductTransactionUseCase
    {
        private readonly IProductTransactionRepository _productTransactionRepository;

        public SearchProductTransactionUseCase(IProductTransactionRepository ProductTransactionRepository)
        {
            this._productTransactionRepository = ProductTransactionRepository;
        }

        public async Task<IEnumerable<ProductTransaction>> ExecuteAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
        {
            if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);
            return await _productTransactionRepository.GetProductTransactionAsync(productName, dateFrom, dateTo, transactionType);
        }
    }
}
