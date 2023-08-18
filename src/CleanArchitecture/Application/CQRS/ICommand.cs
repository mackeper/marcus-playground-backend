﻿using SharedKernel.Result;

namespace Application.CQRS;

public interface ICommand : IRequest<Result>
{
}
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
