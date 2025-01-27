using Common;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;

namespace PayPalApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PayPalService _payPalService;

        public PaymentsController(PayPalService payPalService)
        {
            _payPalService = payPalService;
        }

        [HttpPost("create-payment")]
        public IActionResult CreatePayment()
        {
            try
            {
                var apiContext = _payPalService.GetAPIContext();

                // Create a payment object
                var payment = new Payment
                {
                    intent = "sale",
                    payer = new Payer { payment_method = "paypal" },
                    transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            total = "10.00",  // Payment amount
                            currency = "USD"
                        },
                        description = "Payment for product/service."
                    }
                },
                    redirect_urls = new RedirectUrls
                    {
                        return_url = "https://yourdomain.com/api/payments/success",
                        cancel_url = "https://yourdomain.com/api/payments/cancel"
                    }
                };

                // Create the payment
                var createdPayment = payment.Create(apiContext);

                // Extract approval URL for redirecting user to PayPal
                var approvalUrl = createdPayment.links.FirstOrDefault(
                    link => link.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;

                return Ok(new { ApprovalUrl = approvalUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("success")]
        public IActionResult PaymentSuccess(string paymentId, string PayerID)
        {
            try
            {
                var apiContext = _payPalService.GetAPIContext();

                // Execute the payment
                var payment = new Payment { id = paymentId };
                var paymentExecution = new PaymentExecution { payer_id = PayerID };

                var executedPayment = payment.Execute(apiContext, paymentExecution);

                return Ok(new { Message = "Payment Successful!", Details = executedPayment });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("cancel")]
        public IActionResult PaymentCancel()
        {
            return Ok(new { Message = "Payment Cancelled!" });
        }
    }
}
