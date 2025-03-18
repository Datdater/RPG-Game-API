using BLL.DTO;
using BLL.Interface;
using DAL.Entities;
using DAL.IUnitOfWork.Interface;
using DAL.Migrations;
using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class PaymentService(IUnitOfWork unitOfWork, PayOS payOS, IConfiguration configuration) : IPaymentService
    {
        public async Task<string> CreatePaymentLink(PaymentRequestDTO model)
        {
            var account = unitOfWork.AccountRepository.GetByUsername(model.Username);
            if (account == null) throw new Exception("Not found username");

            long orderCode = long.Parse(DateTimeOffset.Now.ToString("mmssffffff"));
            ItemData item = new ItemData(model.Username, 1, (int)model.Money);
            List<ItemData> items = new List<ItemData>();
            long expried = DateTimeOffset.Now.ToUnixTimeSeconds() + 15*60;
            items.Add(item);
            PaymentData paymentData = new PaymentData(orderCode, (int)model.Money, $"username{model.Username}", items, configuration["PayOS:CancelUrl"], configuration["PayOS:ReturnUrl"], null, null, null, null, null, expried);
            

            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);

            return createPayment.checkoutUrl;
        }

        public async Task ExcuteAddGold(string id)
        {
            var payment = unitOfWork.PaymentRepository.GetByPaymentId(long.Parse(id));
            if (payment != null) return;
            var checkPayment = await payOS.getPaymentLinkInformation(long.Parse(id));
            if (checkPayment.status == "PAID")
            {
                var input = checkPayment.transactions[0].description;
                string username = "";
                Match match = Regex.Match(input, @"username(\w+)");
                if (match.Success)
                {
                    username = match.Groups[1].Value;
                    var account = unitOfWork.AccountRepository.GetByUsername(username);
                    if (account != null)
                    {
                        var paymentSave = new Payment
                        {
                            Id = long.Parse(id),
                            IsPaid = true,
                            Amount = checkPayment.transactions[0].amount,
                            AccountId = account.Id
                        };
                        await unitOfWork.PaymentRepository.AddAsync(paymentSave);
                        account.Gold += checkPayment.transactions[0].amount / 100;
                        await unitOfWork.AccountRepository.UpdateAsync(account);
                        await unitOfWork.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Username not found in description");
                    }
                } else
                {
                    throw new Exception("Username not found in description");
                }

            } else
            {
                throw new Exception("Payment not paid");
            }
        }
    }
}
