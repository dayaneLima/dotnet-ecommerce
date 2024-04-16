using FluentValidation.Results;

namespace Pedidos.Domain.Errors;

public class EntityError
{
    public bool IsValid { get; private set; }

    public ICollection<ErrorModel> Descriptions { get; set; }

    public EntityError(ValidationResult validationResult)
    {
        Descriptions = new List<ErrorModel>();
        IsValid = validationResult.IsValid;
        
        if (!validationResult.IsValid)
            foreach (var error in validationResult.Errors)            
                AddError(error.ErrorMessage, error.PropertyName);        
    }

    public void AddError(string description, string propertyName)
    {
        Descriptions.Add(new ErrorModel {PropertyName = propertyName, Description = description});
    }
}