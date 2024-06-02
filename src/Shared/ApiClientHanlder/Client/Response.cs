using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Shared.ApiClientHanlder;

public class Response<T>
{
	public T? Data { get; private set; }

	public ValidationProblemDetails? ValidationErrors { get; private set; }

	public ProblemDetails? ServerError { get; private set; }

	public int Status { get; private set; }

	public IEnumerable<HeaderData> ResponseHeaders { get; private set; }

	public bool ContainsError { get; private set; }

	public Response(ResponseData response, bool isXml = false, params int[]? otherSuccessStatuses)
	{
		Status = response.StatusCode;
		ResponseHeaders = response.ResponseHeaders;

		if (response.StatusCode >= StatusCodes.Status200OK && response.StatusCode < StatusCodes.Status300MultipleChoices || (otherSuccessStatuses?.Contains(response.StatusCode) ?? false))
		{
			if (isXml)
				Data = response.ConvertXmlContent<T>();
			else
			{
				var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, ReferenceHandler = ReferenceHandler.Preserve };
				Data = response.ConvertContent<T>(options);
			}
		}
		else
		{
			try
			{
				ContainsError = true;
				if (Status == StatusCodes.Status400BadRequest)
					ValidationErrors = response.ConvertContent<ValidationProblemDetails>();
				else
					ServerError = response.ConvertContent<ProblemDetails>();
			}
			catch (Exception ex)
			{
				throw new InvalidDataException($"Could not parse response for status: {response.StatusCode}, data: {response.Content}.", ex);
			}

			ContainsError = true;
		}
	}

	public Response(ResponseData response) : this(response, false, null) { }
}