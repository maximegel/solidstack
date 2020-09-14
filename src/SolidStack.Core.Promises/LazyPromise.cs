using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolidStack.Core.Promises
{
    public class LazyPromise<TError, TSuccess> : Promise<TError, TSuccess>
    {
        public LazyPromise(Func<Task<Promise<TError, TSuccess>>> factory) =>
            Content = new[] {factory}.Select(async create =>
            {
                try
                {
                    return await create();
                }
                catch (Exception)
                {
                    return new DefaultRejectedPromise();
                }
            });

        private LazyPromise()
        {
        }

        private IEnumerable<Task<Promise<TError, TSuccess>>> Content { get; set; }

        internal LazyPromise<TError, TNewSuccess> Map<TNewSuccess>(
            Func<Promise<TError, TSuccess>, Task<Promise<TError, TNewSuccess>>> mapper) =>
            new LazyPromise<TError, TNewSuccess>
            {
                Content = Content.Select(async innerPromise => await mapper(await innerPromise))
            };

        internal Task<Promise<TError, TSuccess>> RunAsync() =>
            Content.Single();
    }
}