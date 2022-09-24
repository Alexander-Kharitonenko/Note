using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;
using NSwag;
using NJsonSchema;

namespace Tools.Swagger
{
    public class ODataQueryOperationProcessor : IOperationProcessor
    {
        private readonly List<OpenApiParameter> OdataParams = new List<OpenApiParameter>() {
            new OpenApiParameter() {
                Name = "$top",
                Type = JsonObjectType.Integer,
                IsRequired= false,
                Kind = OpenApiParameterKind.Query
            },
             new OpenApiParameter() {
                Name = "$skip",
                Type = JsonObjectType.Integer,
                IsRequired= false,
                Kind = OpenApiParameterKind.Query
            },
              new OpenApiParameter() {
                Name = "$filter",
                Type = JsonObjectType.String,
                IsRequired= false,
                Kind = OpenApiParameterKind.Query
            },
               new OpenApiParameter() {
                Name = "$orderby",
                Type = JsonObjectType.String,
                IsRequired= false,
                Kind = OpenApiParameterKind.Query
            },
                new OpenApiParameter() {
                Name = "$count",
                Type = JsonObjectType.Boolean,
                IsRequired= false,
                Kind = OpenApiParameterKind.Query
            },

        };
        public bool Process(OperationProcessorContext context)
        {
            var parameters = context.MethodInfo.GetParameters();

            if (parameters.Any(el => el.ParameterType.IsAssignableTo(typeof(ODataQueryOptions)))) 
            {
               
                var operationParameters = context.OperationDescription.Operation.Parameters;
                foreach (var param in OdataParams) 
                {
                    operationParameters.Add(param);
                }
            }

            return true;
        }
    }
}
