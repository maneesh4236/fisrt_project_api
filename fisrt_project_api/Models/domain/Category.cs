namespace fisrt_project_api.Models.domain
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UrlHandle { get; set; }

        public  ICollection<BlogPost> Blogposts { get; set; }
    }
}
