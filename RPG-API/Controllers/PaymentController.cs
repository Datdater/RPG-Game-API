using BLL.DTO;
using BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RPG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentRequestDTO payment)
        {
            // Process payment
            try
            {
                string paymentLink = await paymentService.CreatePaymentLink(payment);
                return Ok(paymentLink);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> ExcuteAddGold([FromRoute]string id)
        {
            // Execute adding gold
            try
            {
                await paymentService.ExcuteAddGold(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
