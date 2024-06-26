using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using static Emp_Management_SF.Entity.EmployeeBasicDetails;

namespace Emp_Management_SF.ServiceFilter
{
    public class BuildEmployeeFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeFilterCriteria);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("object is null");
                return;
            }

            EmployeeFilterCriteria filterCriteria = (EmployeeFilterCriteria)param.Value;

            var StatusFilter = filterCriteria.Filters.Find(a => a.FieldName == "status");
            if (StatusFilter == null)
            {
                StatusFilter = new FilterCriteria();
                StatusFilter.FieldName = "status";
                StatusFilter.FieldValue = "Active";
                filterCriteria.Filters.Add(StatusFilter);

            }

            filterCriteria.Filters.RemoveAll(a => string.IsNullOrEmpty(a.FieldName));
            var result = await next();
        }
    }
}
