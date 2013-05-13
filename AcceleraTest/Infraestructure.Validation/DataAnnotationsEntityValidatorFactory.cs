using Infraestructure.Validation.Base;

namespace Infraestructure.Validation
{
    public class DataAnnotationsEntityValidatorFactory
    {
        public static IEntityValidator Create()
        {
            return new DataAnnotationsEntityValidator();
        }
    }
}
