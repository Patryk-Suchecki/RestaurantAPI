using System.Linq.Expressions;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RestaurantAPI.entity;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RestaurantAPI.Services
{
    public interface ITransactionService
    {
        PagedResult<TransactionDto> GetAll(TransactionQuery query);
        int Create(CreateTransactionDto dto);
        void Remove(int transactionId);
    }

    public class TransactionService : ITransactionService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public TransactionService(RestaurantDbContext dbContext, IMapper mapper, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
        }


        public PagedResult<TransactionDto> GetAll(TransactionQuery query)
        {
            int userId = int.Parse(_userContextService.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var baseQuery = _dbContext
                .Transactions
                .Include(r => r.Adress)
                .Include(u => u.User)
                    .Where(t => t.UserId == userId);

                baseQuery = (query.sortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(t => t.Date) : baseQuery.OrderByDescending(t => t.Date));

            var transactions = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var transactionsDtos = _mapper.Map<List<TransactionDto>>(transactions);

            var result = new PagedResult<TransactionDto>(transactionsDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }
        public int Create(CreateTransactionDto dto)
        {
            var transaction = _mapper.Map<Transaction>(dto);
            transaction.UserId = (int)_userContextService.GetUserId;
            transaction.Date = DateTime.UtcNow;
            var user = _dbContext.Users
            .FirstOrDefault(u => u.Id == transaction.UserId);
            if (user != null) 
            {
                user.NumberOfOrders++;
            }
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return transaction.Id;
        }
        public void Remove(int transactionId)
        {
            var transaction = _dbContext.Transactions.FirstOrDefault(r => r.Id == transactionId);

            if (transaction is null) throw new NotFoundException("Transaction not found");

            _dbContext.Transactions.Remove(transaction);
            _dbContext.SaveChanges();
        }
    }
}
