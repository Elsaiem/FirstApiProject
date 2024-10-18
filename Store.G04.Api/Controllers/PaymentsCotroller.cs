using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Api.Errors;
using Store.G04.Core.ServicesContract;
using Stripe;
namespace Store.G04.Api.Controllers
{
  
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        [HttpPost("{busketId}")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentIntent(string busketId)
        {
            if (busketId is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));


            var busket = await  _paymentService.CreateOrUpdatePaymentIntentIdAsync(busketId);
            if (busket is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(busket);

        }

        const string endpointSecret = "whsec_a3094c9feb97ebd5a1144b82fe4cf75083f4fa43bf3b222cf652e53a8d360447";

        [HttpPost("webhook")]// https://localhost:7240//api/Payments/webhook
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                // Extract the Stripe signature from the request headers
                var stripeSignature = Request.Headers["Stripe-Signature"];

                // Validate the event with the Stripe signature and endpoint secret
                var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, endpointSecret);

                // Ensure the event is properly recognized as a PaymentIntent event
                if (stripeEvent.Data.Object is PaymentIntent paymentIntent)
                {
                    if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                    {
                        await _paymentService.UpdatePaymentIntentForSucceedOrFailed(paymentIntent.Id, false);
                    }
                    else if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                    {
                        await _paymentService.UpdatePaymentIntentForSucceedOrFailed(paymentIntent.Id, true);
                    }
                    else
                    {
                        Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                    }
                }
                else
                {
                    Console.WriteLine("Event type is not PaymentIntent");
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe exception: {ex.Message}");
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General exception: {ex.Message}");
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
        }


    }

}
