using System;
using System.Net;
using System.Threading.Tasks;
using atmps.domain.appServices.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace atmps.web.Utils
{
    public sealed class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke the specified context.
        /// </summary>
        /// <returns>The invoke.</returns>
        /// <param name="context">Context.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AccountNumberIsNotValidException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest); }
            catch (BalanceOperationException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest); }
            catch (BankIdentificationNumberIsNotFoundException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound); }
            catch (BankInfoIsNotFoundException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound); }
            catch (BankIsNotOperatedInThisATMException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.ExpectationFailed); }
            catch (BankResponseError ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.BadGateway); }
            catch (CardNumberNotValidException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest); }
            catch (CardTypeNotFoundException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound); }
            catch (DepositOperationException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest); }
            catch (Exception ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError); }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            // Log exception here
            string result = JsonConvert.SerializeObject(new { message = exception.Message, time = DateTime.UtcNow });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}