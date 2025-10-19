namespace ApiConsumerProduct.Controllers.v1;

using ApiConsumerProduct.Domain.ConsumerProducts.Features;
using ApiConsumerProduct.Domain.ConsumerProducts.Dtos;
using ApiConsumerProduct.Resources;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using Asp.Versioning;
using MediatR;

[ApiController]
[Route("api/v{v:apiVersion}/consumerproducts")]
[ApiVersion("1.0")]
public sealed class ConsumerProductsController(IMediator mediator): ControllerBase
{    

    /// <summary>
    /// Creates a new ConsumerProduct record.
    /// </summary>
    [HttpPost(Name = "AddConsumerProduct")]
    public async Task<ActionResult<ConsumerProductDto>> AddConsumerProduct([FromBody]ConsumerProductForCreationDto consumerProductForCreation)
    {
        var command = new AddConsumerProduct.Command(consumerProductForCreation);
        var commandResponse = await mediator.Send(command);

        return CreatedAtRoute("GetConsumerProduct",
            new { consumerProductId = commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single ConsumerProduct by ID.
    /// </summary>
    [HttpGet("{consumerProductId:guid}", Name = "GetConsumerProduct")]
    public async Task<ActionResult<ConsumerProductDto>> GetConsumerProduct(Guid consumerProductId)
    {
        var query = new GetConsumerProduct.Query(consumerProductId);
        var queryResponse = await mediator.Send(query);
        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all ConsumerProducts.
    /// </summary>
    [HttpGet(Name = "GetConsumerProducts")]
    public async Task<IActionResult> GetConsumerProducts([FromQuery] ConsumerProductParametersDto consumerProductParametersDto)
    {
        var query = new GetConsumerProductList.Query(consumerProductParametersDto);
        var queryResponse = await mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Append("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }


    /// <summary>
    /// Deletes an existing ConsumerProduct record.
    /// </summary>
    [HttpDelete("{consumerProductId:guid}", Name = "DeleteConsumerProduct")]
    public async Task<ActionResult> DeleteConsumerProduct(Guid consumerProductId)
    {
        var command = new DeleteConsumerProduct.Command(consumerProductId);
        await mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    /// Updates an entire existing ConsumerProduct.
    /// </summary>
    [HttpPut("{consumerProductId:guid}", Name = "UpdateConsumerProduct")]
    public async Task<IActionResult> UpdateConsumerProduct(Guid consumerProductId, ConsumerProductForUpdateDto consumerProduct)
    {
        var command = new UpdateConsumerProduct.Command(consumerProductId, consumerProduct);
        await mediator.Send(command);
        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
