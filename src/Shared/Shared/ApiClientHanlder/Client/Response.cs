﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.ApiClientHanlder;

public class Response<T>
{
    public T? Data { get; private set; }

    public ValidationProblemDetails? ValidationErrors { get; private set; }

    public ProblemDetails? ServerError { get; private set; }

    public int Status { get; private set; }

    public IEnumerable<HeaderData> ResponseHeaders { get; private set; }

    public bool ContainsError { get; private set; }

    public Response(ResponseData response, params int[]? otherSuccessStatuses)
    {
        Status = response.StatusCode;
        ResponseHeaders = response.ResponseHeaders;

        if (response.StatusCode >= StatusCodes.Status200OK && response.StatusCode < StatusCodes.Status300MultipleChoices || (otherSuccessStatuses?.Contains(response.StatusCode) ?? false))
        {
            Data = response.ConvertContent<T>();
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

    public Response(ResponseData response) : this(response, null) { }
}