using System.Net;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ChattyMoUWPGUI.Model.Exception;

namespace ChattyMoUWPGUI.Model.Respository;

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
        {
            var errorMessage = "";

            foreach (var validationError in jsonContent["errors"].AsObject())
            foreach (var propertyError in validationError.Value.AsArray())
                errorMessage += $"{propertyError}";

            return errorMessage;
        }

        if (jsonContent != null && jsonContent.AsObject().ContainsKey("detail"))
            return jsonContent["detail"].ToString();

        return "Unexpected Error";
    }
}