using System;
using System.Collections.Generic;
using System.Linq;

namespace RDSolutions.Repository.Model;

public class Pageable<T> where T : class
{
    public Pageable(int page, int size, decimal total, IEnumerable<T> content)
    {
        if (page < 1)
            throw new ArgumentException($"{nameof(page)} could not be less than 1.");

        if (size < 1)
            throw new ArgumentException($"{nameof(size)} could not be less than 1.");

        Page = page;
        Size = size;
        Total = total;
        Pages = Math.Ceiling(total / size);
        Content = content
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
    }

    /// <summary>
    /// Actual page
    /// </summary>
    public int Page { get; private set; }

    /// <summary>
    /// Requested size
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Content of records
    /// </summary>
    public IList<T> Content { get; private set; }

    /// <summary>
    /// Total of existent records
    /// </summary>
    public decimal Total { get; private set; }

    /// <summary>
    /// Total of pages
    /// </summary>
    public decimal Pages { get; private set; }
}
