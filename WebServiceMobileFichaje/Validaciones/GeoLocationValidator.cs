using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.Models;

namespace WebServiceMobileFichaje.Validaciones
{
    public class GeoLocationValidator : AbstractValidator<GeoLocationTimeSheet>
    {
        public GeoLocationValidator()
        {
            RuleFor(model => model.CoordenadaX).NotEmpty().WithMessage("La coordenada X es requerida"); 
            RuleFor(model => model.CoordenadaY).NotEmpty().WithMessage("La coordenada Y es requerida");
            When(x => x.CoordenadaX != 0 && x.CoordenadaY != 0, () => {
                RuleFor(model => model)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithName("location")
                .SetValidator(new ValidacionGeoLocationProximidad());
            });

        }
    }
}