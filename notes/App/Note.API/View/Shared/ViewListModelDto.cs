namespace Note.API.View.Shared
{
    public class ViewListModelDto<TModel> where TModel : class
    {
        public IQueryable<TModel> data { get; set; }
        public int? Count { get; set; }
    }
}
