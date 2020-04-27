// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System.Collections.Generic;
using System.Threading.Tasks;
using Silverback.Messaging.Messages;

namespace Silverback.Messaging.Publishing
{
    /// <summary>
    ///     <para>
    ///         Publishes the messages implementing <see cref="ICommand" />.
    ///     </para>
    ///     <para>
    ///         An <see cref="ICommand" /> is  can optionally return a value.
    ///     </para>
    /// </summary>
    public interface ICommandPublisher
    {
        void Execute(ICommand commandMessage);

        Task ExecuteAsync(ICommand commandMessage);

        void Execute(IEnumerable<ICommand> commandMessages);

        Task ExecuteAsync(IEnumerable<ICommand> commandMessages);

        TResult Execute<TResult>(ICommand<TResult> commandMessage);

        Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> commandMessage);

        IReadOnlyCollection<TResult> Execute<TResult>(IEnumerable<ICommand<TResult>> commandMessages);

        Task<IReadOnlyCollection<TResult>> ExecuteAsync<TResult>(IEnumerable<ICommand<TResult>> commandMessages);
    }
}
