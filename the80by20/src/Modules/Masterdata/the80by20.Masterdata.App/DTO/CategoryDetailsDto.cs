using System.ComponentModel.DataAnnotations;

namespace the80by20.Modules.Masterdata.App.DTO
{
    // INFO there due to crud level logic and simple use cases inheritance is used, however watch out for inheritance, 
    // often better way is not to inherit information (properties) or behavior (methods) and just copy (event if it seems like duplication and repetition),
    // if the code is used by different application use cases invoked by actor because (this similar at the beginning)
    // code will extend in different directions due to separate requirements
    //
    // according to Single Responsibility Principle code should be separated (put in different types / components)
    // based on different use cases in which actor (interacts) (send queries / commands) to the application
    public class CategoryDetailsDto : CategoryDto
    {
        // INFO 
        // Alternative way is to use fluent-validation nuget package
        [StringLength(1000)]
        public string Description { get; set; }
    }
}
