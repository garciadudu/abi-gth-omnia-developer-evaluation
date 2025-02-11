using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
/// <summary>
/// Command for deleting a user
/// </summary>
public record DeleteProductCommand : IRequest<DeleteProductResponse>
{
    /// <summary>
    /// The unique identifier of the user to delete
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteUserCommand
    /// </summary>
    /// <param name="id">The ID of the user to delete</param>
    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}
