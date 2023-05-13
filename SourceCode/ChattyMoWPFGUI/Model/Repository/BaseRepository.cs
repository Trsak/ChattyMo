using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ChattyMoWPFGUI.Model.Exception;

namespace ChattyMoWPFGUI.Model.Repository;

public class BaseRepository
{
    protected static async Task EnsureRequestIsSuccessful(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var details = await ParseBadRequestContent(response);
            throw new BadRequestException(details);
        }

        response.EnsureSuccessStatusCode();
    }

    private static async Task<string> ParseBadRequestContent(HttpResponseMessage httpResponseMessage)
    {
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var jsonContent = JsonNode.Parse(content);

        if (jsonContent != null && jsonContent.AsObject().ContainsKey("errors"))
            return jsonContent["errors"].AsObject().Aggregate("",
                (current1, validationError) => validationError.Value.AsArray()
                    .Aggregate(current1, (current, propertyError) => current + $"{propertyError} "));

        if (jsonContent != null && jsonContent.AsObject().ContainsKey("detail"))
            return jsonContent["detail"].ToString();

        return "Unexpected Error";
    }
}