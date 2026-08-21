using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace LibraryManagmentSystem.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = ex switch
            {
                DbUpdateException dbEx when dbEx.InnerException != null &&
                    dbEx.InnerException.Message.Contains("FOREIGN KEY")
                    => (HttpStatusCode.BadRequest, "Invalid reference — the related record (e.g. AuthorId, BookId, MemberId) does not exist."),

                DbUpdateException dbEx when dbEx.InnerException != null &&
                    dbEx.InnerException.Message.Contains("UNIQUE") || dbEx.InnerException?.Message.Contains("duplicate") == true
                    => (HttpStatusCode.Conflict, "A record with the same unique value (e.g. email) already exists."),

                DbUpdateException
                    => (HttpStatusCode.BadRequest, "An error occurred while saving changes to the database."),

                KeyNotFoundException
                    => (HttpStatusCode.NotFound, ex.Message),

                UnauthorizedAccessException
                    => (HttpStatusCode.Unauthorized, "You are not authorized to perform this action."),

                ArgumentException
                    => (HttpStatusCode.BadRequest, ex.Message),

                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred. Please try again later.")
            };

            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                status = (int)statusCode,
                error = message
            });

            return context.Response.WriteAsync(result);
        }
    }
}