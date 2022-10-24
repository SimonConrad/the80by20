using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Masterdata.App.Exceptions
{
    public class CannotDeleteCategoryException : CustomException
    {
        public Guid Id { get; }

        public CannotDeleteCategoryException(Guid id) : base($"Category with ID: '{id}' cannot be deleted.")
        {
            Id = id;
        }
    }
}
