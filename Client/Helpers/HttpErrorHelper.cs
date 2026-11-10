using System;
using System.Net;
using System.Net.Http;

namespace Client.Helpers;

public class HttpErrorHelper
{
    public static string GetMessage(HttpRequestException exception)
    {
        return exception.StatusCode switch
        {
            HttpStatusCode.BadRequest =>
                "Begäran innehåller ogiltiga uppgifter.",

            HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden =>
                "Åtkomst till databasen nekades.",

            HttpStatusCode.NotFound =>
                "Den begärda resursen kunde inte hittas.",

            HttpStatusCode.TooManyRequests =>
                "För många förfrågningar. Försök igen senare.",

            >= HttpStatusCode.InternalServerError =>
                "Ett serverfel inträffade. Försök igen senare.",

            _ =>
                "Det gick inte att kommunicera med servern."
        };
    }
}
