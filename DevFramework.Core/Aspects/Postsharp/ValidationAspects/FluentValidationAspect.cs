﻿using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValitadion;
using FluentValidation;
using PostSharp.Aspects;
using System;
using System.Linq;


namespace DevFramework.Core.Aspects.Postsharp.ValidationAspects
{
    [Serializable]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        Type _validatorType;

        public FluentValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = args.Arguments.Where(t => t.GetType() == entityType).ToList();

            foreach (var entity in entities)
            {
                ValidatorTool.FluentValidate(validator, entity);
            }


        }
    }
}
